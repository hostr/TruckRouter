using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapSolver.Models;

namespace MapSolver.Algorithms
{
    /// <summary>
    /// Used to calculate the H score in A* algorithm
    /// Possible algorithms are Manhattan, Diagonal, Euclidian, etc
    /// </summary>
    public interface IHeuristicAlgorithm
    {
        double Calculate(Point from, Point to);
    }
}
