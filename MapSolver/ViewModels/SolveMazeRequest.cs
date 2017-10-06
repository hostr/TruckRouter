using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MapSolver.ViewModels
{
    public class SolveMazeRequest
    {
        public string[] Maze { get; set; }
    }
}
