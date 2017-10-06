using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapSolver.ViewModels;

namespace MapSolver.Interfaces
{
    public interface ISolvingService
    {
        SolveMazeResponse Solve(string[] maze);
        SolveMazeResponse SolveSample(string[] maze);
    }
}
