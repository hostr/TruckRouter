using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapSolver.Models
{
    public enum PointTypes
    {
        Unknown = 0,
        Open = '.',
        Blocked = '#',
        Start = 'A',
        Destination = 'B',
        Solution = '@'
    }
}
