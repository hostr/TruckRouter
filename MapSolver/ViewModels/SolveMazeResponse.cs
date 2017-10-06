using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapSolver.ViewModels
{
    public class SolveMazeResponse
    {
        public int Steps { get; set; }
        public string[] Solution { get; set; }
    }
}
