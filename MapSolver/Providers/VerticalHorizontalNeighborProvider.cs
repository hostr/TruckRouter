using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapSolver.Models.Providers
{
    public class VerticalHorizontalNeighborProvider : INeighborProvider
    {
        // { X, Y }
        private static readonly int[,] moves = {
            { 0, 1 },   // Down
            { 1, 0 },   // Right
            { 0, -1 },  // Up
            { -1, 0 },   // Left
        };

        public IEnumerable<Point> GetNeighbors(Point point)
        {
            var neighbors = new List<Point>();

            for (var i = 0; i < moves.GetLength(0); i++)
            {
                var xMove = moves[i, 0];
                var yMove = moves[i, 1];

                neighbors.Add(new Point
                {
                    X = point.X + moves[i, 0],
                    Y = point.Y + moves[i, 1]
                });
            }

            return neighbors;
        }
    }
}
