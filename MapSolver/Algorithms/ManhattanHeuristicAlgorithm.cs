﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapSolver.Models;

namespace MapSolver.Algorithms
{
    public class ManhattanHeuristicAlgorithm : IHeuristicAlgorithm
    {
        /// <summary>
        /// Helper method to calculate H score in A* algorithm.
        /// Sum of absolute values of differences between from and to.
        /// http://theory.stanford.edu/~amitp/GameProgramming/Heuristics.html
        /// </summary>
        /// <param name="from">Pont to calculate distance from</param>
        /// <param name="to">Point to calculate distance to</param>
        /// <returns>Distance from one point to another</returns>
        public double Calculate(Point from, Point to)
        {
            return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
        }
    }
}
