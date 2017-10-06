using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MapSolver.Interfaces;
using MapSolver.Models;
using MapSolver.ViewModels;

namespace MapSolver.Services
{
    public class SolvingService : ISolvingService
    {
        /// <summary>
        /// http://theory.stanford.edu/~amitp/GameProgramming/Heuristics.html
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private double CalculateManhattanHeuristic(Point from, Point to)
        {
            return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
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

            var start = grid.FirstOrDefault(m => m.Type == PointTypes.Start);
            var end = grid.FirstOrDefault(m => m.Type == PointTypes.Start);
            
            var closed = new List<Point>();
            var open = new List<Point> { start };

            var path = new Dictionary<Point, Point>();

            var gScore = new Dictionary<Point, double> { [start] = 0 };
            var fScore = new Dictionary<Point, double> { [start] = CalculateManhattanHeuristic(start, end) };




            return new SolveMazeResponse();
        }

        public SolveMazeResponse SolveSample(string[] maze)
        {
               

            return new SolveMazeResponse
            {
                Steps = 14,
                Solution = new[]
                {
                    "##########",
                    "#A@@.#...#",
                    "#.#@##.#.#",
                    "#.#@##.#.#",
                    "#.#@@@@#B#",
                    "#.#.##@#@#",
                    "#....#@@@#",
                    "##########"
                }
            };
        }
    }
}
