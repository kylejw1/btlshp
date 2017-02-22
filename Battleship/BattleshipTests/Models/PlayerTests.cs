using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using Battleship.Models;

namespace Battleship.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void PlayerCtorTest()
        {
            var player = new Player("Jimmy Bob");
            Assert.AreEqual(player.Name, "Jimmy Bob");
        }

        [TestMethod()]
        public void PlayerNoNameCtorTest()
        {
            var player1 = new Player("");
            Assert.IsTrue(player1.Name.StartsWith("Player"));
            var player2 = new Player(null);
            Assert.AreNotEqual(player2.Name, player1.Name);
            Assert.IsTrue(player2.Name.StartsWith("Player"));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetNullShipTest()
        {
            var player = new Player(null);
            player.SetShip(null);
        }

        [TestMethod()]
        public void SinkShipTest()
        {
            var player = new Player(null);
            Assert.IsFalse(player.ShipSunk);
            var pt1 = new Point(0, 0);
            var pt2 = new Point(0, 2);
            player.SetShip(new Ship(pt1, pt2));
            Assert.IsFalse(player.ShipSunk);
            player.IncomingShot(pt1);
            Assert.IsFalse(player.ShipSunk);
            player.IncomingShot(pt2);
            Assert.IsFalse(player.ShipSunk);
            player.IncomingShot(new Point(0, 1));
            Assert.IsTrue(player.ShipSunk);
            
        }

        [TestMethod()]
        public void FireShotTest()
        {
            var p = new Player(null);
            var pt = new Point(0, 0);
            Assert.IsFalse(p.CoordinateAlreadyTried(pt));
            // Fire a DIFFERENT shot
            p.FireShot(new Point(0, 1));
            Assert.IsFalse(p.CoordinateAlreadyTried(pt));
            p.FireShot(pt);
            Assert.IsTrue(p.CoordinateAlreadyTried(pt));
        }

        [TestMethod()]
        public void CoordinateAlreadyTriedWithEmptyListTest()
        {
            var p = new Player(null);
            Assert.IsFalse(p.CoordinateAlreadyTried(new Point()));
        }

        [TestMethod()]
        public void IncomingShotMissTest()
        {
            var p = new Player(null);
            p.SetShip(new Ship(new Point(0, 0), new Point(0, 2)));
            Assert.AreEqual(CellState.Miss, p.IncomingShot(new Point(5, 5)));
        }

        [TestMethod()]
        public void IncomingShotHitTest()
        {
            var p = new Player(null);
            p.SetShip(new Ship(new Point(0, 0), new Point(0, 2)));
            Assert.AreEqual(CellState.Hit, p.IncomingShot(new Point(0, 1)));
        }
    }
}