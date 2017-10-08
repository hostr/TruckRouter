using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MapSolver.Models;

namespace MapSolver.Providers
{
    public interface INeighborProvider
    {
        IEnumerable<Point> GetNeighbors(Point point);
    }
}
