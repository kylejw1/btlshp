using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Battleship.Misc;

namespace Battleship.Tests
{
    [TestClass()]
    public class GeometryTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ColinearAboutAxesSinglePointTest()
        {
            Geometry.ColinearAboutAxes(new List<Point>() { new Point(0,0) });
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ColinearAboutAxesNullTest()
        {
            Geometry.ColinearAboutAxes(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ColinearAboutAxesEmptyTest()
        {
            Geometry.ColinearAboutAxes(null);
        }

        [TestMethod()]
        public void ColinearAboutAxesHorizontalTest()
        {
            var col = Geometry.ColinearAboutAxes(new List<Point>() { new Point(0, 0), new Point(5,0) });
            Assert.IsTrue(col);
        }

        [TestMethod()]
        public void ColinearAboutAxesVerticalTest()
        {
            var col = Geometry.ColinearAboutAxes(new List<Point>() { new Point(0, 0), new Point(0, 5) });
            Assert.IsTrue(col);
        }

        [TestMethod()]
        public void ColinearAboutAxesNegativeTest()
        {
            var col = Geometry.ColinearAboutAxes(new List<Point>() { new Point(-10, 0), new Point(0, 0) });
            Assert.IsTrue(col);
        }

        [TestMethod()]
        public void ColinearAboutAxesMultipointTest()
        {
            var col = Geometry.ColinearAboutAxes(new List<Point>() { new Point(-10, 0), new Point(0, 0), new Point(10,0) });
            Assert.IsTrue(col);
        }

        [TestMethod()]
        public void ColinearAboutAxesNotColinearTest()
        {
            var col = Geometry.ColinearAboutAxes(new List<Point>() { new Point(-10, 0), new Point(0, 0), new Point(0, 10) });
            Assert.IsFalse(col);
        }
    }
}