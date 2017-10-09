using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapSolver.Algorithms;
using MapSolver.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MapSolver.Tests.Algorithms
{
    [TestClass]
    public class PythagoreasTheoremAlgorithmTests
    {
        [TestMethod]
        public void Calculate_StraightLine_ReturnsExpectedValue()
        {
            var algorithm = new PythagoreasTheoremAlgorithm();

            var from = new Point(0, 0);
            var to = new Point(0, 10);

            var result = algorithm.Calculate(from, to);

            Assert.IsTrue(result == 10);
        }

        [TestMethod]
        public void Calculate_DiagonalLine_ReturnsExpectedValue()
        {
            var algorithm = new PythagoreasTheoremAlgorithm();

            var from = new Point(0, 0);
            var to = new Point(3, 4);

            var result = algorithm.Calculate(from, to);

            Assert.IsTrue(result == 5);
        }
    }
}
