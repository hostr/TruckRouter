using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapSolver.Models;

namespace MapSolver.Providers
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

        /// <summary>
        /// Get the neighbors above, below, and to the sides of a point
        /// </summary>
        /// <param name="point">Point to get neighbors from</param>
        /// <returns>List of neighbors</returns>
        public IEnumerable<Point> GetNeighbors(Point point)
        {
            var neighbors = new List<Point>();

            for (var i = 0; i < moves.GetLength(0); i++)
            {
                var neighborX = point.X + moves[i, 0];
                var neighborY = point.Y + moves[i, 1];

                neighbors.Add(new Point(neighborX, neighborY));
            }

            return neighbors;
        }
    }
}
