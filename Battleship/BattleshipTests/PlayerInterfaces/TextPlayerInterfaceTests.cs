using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleship.PlayerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Battleship.PlayerInterface.Tests
{
    [TestClass()]
    public class TextPlayerInterfaceTests
    {
        StreamWriter outputFromInterface = new StreamWriter(new MemoryStream());

        [TestCleanup]
        void Cleanup()
        {
            outputFromInterface.Close();
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TextPlayerInterfaceCtorNullInputsTest()
        {
            var i = new TextPlayerInterface(null, Console.Out);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TextPlayerInterfaceCtorNullInputsTest2()
        {
            var i = new TextPlayerInterface(Console.In, null);
        }

        [TestMethod()]
        public void GetFiringCoordinateTest()
        {
            using (var sr = new StringReader("D6"))
            {
                var i = new TextPlayerInterface(sr, outputFromInterface);
                Assert.AreEqual(new Point(3, 5), i.GetFiringCoordinate(new Player(null)));
            }
        }

        [TestMethod()]
        public void GetFiringCoordinateLowecaseTest()
        {
            using (var sr = new StringReader("d6"))
            {
                var i = new TextPlayerInterface(sr, outputFromInterface);
                Assert.AreEqual(new Point(3, 5), i.GetFiringCoordinate(new Player(null)));
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void GetFiringCoordinateBlankInputTest()
        {
            using (var sr = new StringReader(""))
            {
                var i = new TextPlayerInterface(sr, outputFromInterface);
                Assert.AreEqual(new Point(5, 3), i.GetFiringCoordinate(new Player(null)));
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void GetFiringCoordinateBadInputTest()
        {
            using (var sr = new StringReader("66D"))
            {
                var i = new TextPlayerInterface(sr, outputFromInterface);
                Assert.AreEqual(new Point(5, 3), i.GetFiringCoordinate(new Player(null)));
            }
        }

        [TestMethod()]
        public void GetFiringCoordinateBadThenGoodTest()
        {
            using (var sr = new StringReader("" + Environment.NewLine + "E5"))
            {
                var i = new TextPlayerInterface(sr, outputFromInterface);
                Assert.AreEqual(new Point(4, 4), i.GetFiringCoordinate(new Player(null)));
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void GetPlayerShipBlankTest()
        {
            using (var sr = new StringReader(""))
            {
                var i = new TextPlayerInterface(sr, outputFromInterface);
                Assert.AreEqual(new Point(5, 3), i.GetPlayerShip(new Player(null)));
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void GetPlayerShipOneCoordTest()
        {
            using (var sr = new StringReader("A5"))
            {
                var i = new TextPlayerInterface(sr, outputFromInterface);
                Assert.AreEqual(new Point(5, 3), i.GetPlayerShip(new Player(null)));
            }
        }

        [TestMethod()]
        public void GetPlayerShipGoodHorizontalCoordTest()
        {
            using (var sr = new StringReader("A5 A7"))
            {
                var i = new TextPlayerInterface(sr, outputFromInterface);
                Assert.IsNotNull(i.GetPlayerShip(new Player(null)));
            }
        }

        [TestMethod()]
        public void GetPlayerShipGoodLowercaseCoordTest()
        {
            using (var sr = new StringReader("a5 A7"))
            {
                var i = new TextPlayerInterface(sr, outputFromInterface);
                Assert.IsNotNull(i.GetPlayerShip(new Player(null)));
            }
        }

        [TestMethod()]
        public void GetPlayerShipGoodVerticalCoordTest()
        {
            using (var sr = new StringReader("A5 C5"))
            {
                var i = new TextPlayerInterface(sr, outputFromInterface);
                Assert.IsNotNull(i.GetPlayerShip(new Player(null)));
            }
        }

        [TestMethod()]
        public void GetPlayerShipBadThenGoodCoordTest()
        {
            using (var sr = new StringReader("A5 6" + Environment.NewLine + "A5 A7"))
            {
                var i = new TextPlayerInterface(sr, outputFromInterface);
                Assert.IsNotNull(i.GetPlayerShip(new Player(null)));
            }
        }

    }
}