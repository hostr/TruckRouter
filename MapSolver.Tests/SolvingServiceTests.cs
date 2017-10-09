using System;
using System.Collections.Generic;
using System.Linq;
using MapSolver.Algorithms;
using MapSolver.Interfaces;
using MapSolver.Providers;
using MapSolver.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MapSolver.Tests
{
    [TestClass]
    public class SolvingServiceTests
    {
        private readonly ISolvingService _service;

        public SolvingServiceTests()
        {
            _service = new SolvingService(
                new VerticalHorizontalNeighborProvider(), 
                new ManhattanHeuristicAlgorithm(),
                new PythagoreasTheoremAlgorithm());
        }

        [TestMethod]
        public void Solve_WithUnsolveableMaze_ReturnExpectedResult()
        {
            var mazeToSolve = new[]
            {
                "##########",
                "#A...#...#",
                "##########",
                "#.#.##.#.#",
                "#.#....#B#",
                "#.#.##.#.#",
                "#....#...#",
                "##########"
            };

            var result = _service.Solve(mazeToSolve);

            // Unsolveable mazes will return 0 steps and empty solution
            Assert.IsTrue(result.Steps == 0);
            Assert.IsTrue(result.Solution.Length == 0);
        }

        [TestMethod]
        public void Solve_WithSolveableMaze_ReturnExpectedResult()
        {
            var mazeToSolve = new[]
            {
                "##########",
                "#A...#...#",
                "#.#.##.#.#",
                "#.#.##.#.#",
                "#.#....#B#",
                "#.#.##.#.#",
                "#....#...#",
                "##########"
            };

            var result = _service.Solve(mazeToSolve);

            var expected = new[]
            {
                "##########",
                "#A@@.#...#",
                "#.#@##.#.#",
                "#.#@##.#.#",
                "#.#@@@@#B#",
                "#.#.##@#@#",
                "#....#@@@#",
                "##########"
            };

            Assert.IsTrue(result.Steps == 14);
            Assert.IsTrue(result.Solution.SequenceEqual(expected));
        }
    }
}
