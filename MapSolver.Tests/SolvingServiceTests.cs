using System.Linq;
using MapSolver.Interfaces;
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
            _service = new SolvingService();
        }


        [TestMethod]
        public void SolveUnsolveableMaze()
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
            Assert.Equals(result.Steps, 0);
            Assert.Equals(result.Solution.Length, 0);
        }

        [TestMethod]
        public void SolveSolveableMaze()
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
            
            Assert.Equals(result.Steps, 14);
            Assert.Equals(result.Solution, new []
            {
                "##########",
                "#A@@.#...#",
                "#.#@##.#.#",
                "#.#@##.#.#",
                "#.#@@@@#B#",
                "#.#.##@#@#",
                "#....#@@@#",
                "##########"
            });
        }
    }
}
