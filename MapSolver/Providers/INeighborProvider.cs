using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MapSolver.Models.Providers
{
    public interface INeighborProvider
    {
        IEnumerable<Point> GetNeighbors(Point point);
    }
}
