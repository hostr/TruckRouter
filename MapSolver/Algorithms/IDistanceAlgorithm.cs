using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapSolver.Models;

namespace MapSolver.Algorithms
{
    public interface IDistanceAlgorithm
    {
        double Calculate(Point from, Point to);
    }
}
