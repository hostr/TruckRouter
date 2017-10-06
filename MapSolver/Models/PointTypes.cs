using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapSolver.Models
{
    public enum PointTypes
    {
        Open = '.',
        Blocked = '#',
        Start = 'A',
        Destination = 'B',
        Solution = '@'
    }
}
