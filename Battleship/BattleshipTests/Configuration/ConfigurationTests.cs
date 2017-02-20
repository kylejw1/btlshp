using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleship.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Config.Tests
{
    [TestClass()]
    public class ConfigurationTests
    {
        [TestMethod()]
        public void ConfigurationTest()
        {
            var config = new Configuration("1", "2");
            Assert.AreEqual(config.Player1Name, "1");
            Assert.AreEqual(config.Player2Name, "2");
        }

        [TestMethod()]
        public void BlankPlayerNameTest()
        {
            var config = new Configuration(null, "");
            Assert.AreEqual(config.Player1Name, "Player1");
            Assert.AreEqual(config.Player2Name, "Player2");
        }
    }
}