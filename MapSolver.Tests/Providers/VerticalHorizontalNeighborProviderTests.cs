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

            var point = new Point(1, 1);

            var result = provider.GetNeighbors(point);

            var expected = new[]
            {
                new Point(1, 2),
                new Point(2, 1),
                new Point(1, 0),
                new Point(0, 1)
            };

            Assert.IsTrue(result.SequenceEqual(expected));
        }
    }
}
