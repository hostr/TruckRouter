using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapSolver.Models;
using MapSolver.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MapSolver.Tests.Providers
{
    [TestClass]
    public class VerticalHorizontalNeighborProviderTests
    {
        [TestMethod]
        public void GetNeighbors_WhenCalled_ReturnsExpectedValues()
        {
            var provider = new VerticalHorizontalNeighborProvider();

            var point = new Point {X = 1, Y = 1};

            var result = provider.GetNeighbors(point);

            var expected = new[]
            {
                new Point {X = 1, Y = 2},
                new Point {X = 1, Y = 2},
                new Point {X = 1, Y = 2},
                new Point {X = 1, Y = 2}
            };
        }
    }
}
