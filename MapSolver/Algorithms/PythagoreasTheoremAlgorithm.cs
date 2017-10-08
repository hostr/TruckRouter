using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapSolver.Models;

namespace MapSolver.Algorithms
{
    /// <summary>
    /// Helper method to calculate G score in A* algorithm
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns>Distance from one point to another</returns>
    public class PythagoreasTheoremAlgorithm : IDistanceAlgorithm
    {
        public double Calculate(Point from, Point to)
        {
            return Math.Sqrt(Math.Pow(to.X - from.X, 2) + Math.Pow(to.Y - from.Y, 2));
        }
    }
}
