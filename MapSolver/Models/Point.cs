using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapSolver.Models
{
    public class Point
    {
        public int X { get; }
        public int Y { get; }
        public PointTypes Type { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Point a, Point b) => a.X == b.X && a.Y == b.Y;

        public static bool operator !=(Point a, Point b) => !(a == b);

        protected bool Equals(Point other)
        {
            return X == other.X && Y == other.Y && Type == other.Type;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                return hashCode;
            }
        }
    }
}
