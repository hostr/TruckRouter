using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapSolver.Interfaces;
using MapSolver.ViewModels;

namespace MapSolver.Services
{
    public class SolvingService : ISolvingService
    {
        public SolveMazeResponse Solve(string[] maze)
        {
            throw new NotImplementedException();
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
