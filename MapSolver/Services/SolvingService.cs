using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MapSolver.Interfaces;
using MapSolver.Models;
using MapSolver.Models.Providers;
using MapSolver.ViewModels;

namespace MapSolver.Services
{
    public class SolvingService : ISolvingService
    {
        private readonly INeighborProvider _neighborProvider;

        public SolvingService(INeighborProvider neighborProvider)
        {
            _neighborProvider = neighborProvider;
        }

        /// <summary>
        /// Helper method to calculate F score in A* algorithm
        /// http://theory.stanford.edu/~amitp/GameProgramming/Heuristics.html
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>Distance from one point to another</returns>
        private static double CalculateManhattanHeuristic(Point from, Point to)
        {
            return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
        }

        /// <summary>
        /// Helper method to calculate G score in A* algorithm
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>Distance from one point to another</returns>
        private static double CalculatePythagoreasDistance(Point from, Point to)
        {
            return Math.Sqrt(Math.Pow(to.X - from.X, 2) + Math.Pow(to.Y - from.Y, 2));
        }

        private List<Point> GetGridFromMaze(string[] maze)
        {
            var grid = new List<Point>();

            for (var i = 0; i < maze.Length; i++)
            {
                for (var j = 0; j < maze[i].Length; j++)
                {
                    var character = maze[i][j];

                    // Verify point is a valid character
                    if (!Enum.IsDefined(typeof(PointTypes), (int) character))
                    {
                        throw new DataException();
                    }

                    grid.Add(new Point
                    {
                        X = j,
                        Y = i,
                        Type = (PointTypes) character
                    });
                }
            }

            // Should ensure grid is valid somewhere in here (contains a start, finish, and open)

            return grid;
        }

        public SolveMazeResponse Solve(string[] maze)
        {
            // Contains a list of all points and point types in the maze
            var grid = GetGridFromMaze(maze);

            // Assign start and destination points
            var start = grid.FirstOrDefault(m => m.Type == PointTypes.Start);
            var destination = grid.FirstOrDefault(m => m.Type == PointTypes.Destination);
            
            // Closed points are points that have already been checked
            var closed = new List<Point>();
            // Open points are points that need to be checked still
            var open = new List<Point> { start };

            var path = new Dictionary<Point, Point>();

            var gScore = new Dictionary<Point, double> { [start] = 0 };
            var fScore = new Dictionary<Point, double> { [start] = CalculateManhattanHeuristic(start, destination) };

            while (open.Any())
            {
                var current = open.OrderBy(c => fScore[c]).First();

                if (current.Type == PointTypes.Destination)
                {
                    // Get solution path
                    var solutionPath = ReconstructPath(path, current).ToList();

                    // Reconstruct maze with new path
                    var solution = ReconstructMazeWithPath(grid, solutionPath);

                    // Return SolveMazeResponse with steps and solution maze
                    return new SolveMazeResponse
                    {
                        Steps = solutionPath.Count,
                        Solution = solution
                    };
                }

                open.Remove(current);
                closed.Add(current);

                foreach (var neighbor in _neighborProvider.GetNeighbors(current))
                {
                    var neighborPoint = grid.FirstOrDefault(m => m.X == neighbor.X && m.Y == neighbor.Y);

                    // Point is out of bounds of the maze
                    if (neighborPoint == null )
                    {
                        continue;
                    }

                    // Already exists in closed, neighbor out of bounds, or neighbor is blocked
                    if (closed.Contains(neighborPoint) || neighborPoint.Type == PointTypes.Blocked)
                    {
                        continue;
                    }

                    var testGScore = gScore[current] + CalculatePythagoreasDistance(current, neighborPoint);

                    if (!open.Contains(neighborPoint))
                    {
                        open.Add(neighborPoint);
                    } else if (testGScore >= gScore[neighborPoint])
                    {
                        continue;
                    }

                    path[neighborPoint] = current;

                    gScore[neighborPoint] = testGScore;
                    fScore[neighborPoint] = gScore[neighborPoint] + CalculateManhattanHeuristic(neighborPoint, destination);
                }
            }

            // If it gets to this point it means the maze wasn't solveable
            // Return 0, empty solution
            return new SolveMazeResponse
            {
                Steps = 0,
                Solution = new string[0]
            };
        }

        /// <summary>
        /// Getting path from end to start
        /// </summary>
        /// <param name="path"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        private IEnumerable<Point> ReconstructPath(IDictionary<Point, Point> path, Point current)
        {
            var totalPath = new List<Point> {current};

            while (path.ContainsKey(current))
            {
                current = path[current];

                if (current.Type == PointTypes.Start)
                {
                    totalPath.Add(current);

                    continue;
                }

                current.Type = PointTypes.Solution;

                totalPath.Add(current);
            }

            totalPath.Reverse();
            totalPath.RemoveAt(0);

            return totalPath;
        }

        /// <summary>
        /// Faster to replace grid points with solution points then rebuild maze, 
        /// rebuild maze from grid while checking if solution point exists, or
        /// replace maze characters with solution characters?
        /// String immutable so not as easy as replace characters
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="solutionPath"></param>
        /// <returns></returns>
        private string[] ReconstructMazeWithPath(List<Point> grid, IEnumerable<Point> solutionPath)
        {
            // Set all points in grid to be solution points if they're in the solution path
            foreach (var point in solutionPath)
            {
                var existingGridPoint = grid.First(m => m.X == point.X && m.Y == point.Y);

                if (existingGridPoint.Type == PointTypes.Destination)
                {
                    continue;
                }

                existingGridPoint.Type = PointTypes.Solution;
            }

            // Rebuild maze from grid
            var rowCount = grid.Max(m => m.Y) + 1;
            var solutionMaze = new string[rowCount];

            for (var i = 0; i < solutionMaze.Length; i++)
            {
                var rowPoints = grid.Where(m => m.Y == i);

                var row = new StringBuilder();
                foreach (var point in rowPoints)
                {
                    row.Append((char)point.Type);
                }

                solutionMaze[i] = row.ToString();
            }

            return solutionMaze;
        }
    }
}
