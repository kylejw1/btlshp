using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Battleship.Tests
{
    [TestClass()]
    public class ShipTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ShipNonColinearCtorTest()
        {
            var ship = new Ship(new Point(0, 0), new Point(2, 2));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShipTooShortCtorTest()
        {
            var ship = new Ship(new Point(0, 0), new Point(0, 1));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShipTooLongCtorTest()
        {
            var ship = new Ship(new Point(0, 0), new Point(5, 0));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShipSinglePointCtorTest()
        {
            var ship = new Ship(new Point(0, 0), new Point(0, 0));
        }

        [TestMethod()]
        public void VerticalShipCtorTest()
        {
            var ship = new Ship(new Point(5, 5), new Point(5, 7));
            Assert.IsNotNull(ship);
        }

        [TestMethod()]
        public void HorizontalShipCtorTest()
        {
            var ship = new Ship(new Point(5, 5), new Point(7, 5));
            Assert.IsNotNull(ship);
        }

        [TestMethod()]
        public void NegativeCoordsCtorTest()
        {
            var ship = new Ship(new Point(-1, -1), new Point(1, -1));
            Assert.IsNotNull(ship);
        }

    }
}