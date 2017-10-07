﻿using System;
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

            while (open.Any())
            {
                var current = open.OrderBy(c => fScore[c]).First();

                if (current.Type == PointTypes.Destination)
                {
                    // Get solution path
                    var solutionPath = ReconstructPath(path, current).ToList();

                    // Reconstruct maze with new path
                    var solution = ReconstructMazeWithPath(maze, solutionPath);

                    // Return SolveMazeResponse with steps and solution maze
                    return new SolveMazeResponse
                    {
                        Steps = solutionPath.Count,
                        Solution = solution
                    };
                }

                open.Remove(current);
                closed.Add(current);
            }


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
                existingGridPoint.Type = PointTypes.Solution;
            }

            // Rebuild maze from grid
            var solutionMaze = new string[grid.Max(m => m.Y)];
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
