//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Battleship.Config;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;

//namespace Battleship.Config.Tests
//{
//    [TestClass()]
//    public class StreamConfiguratonProviderTests
//    {
//        [TestMethod()]
//        public void StreamConfiguratonProviderTest()
//        {
//            using (var stream = new MemoryStream())
//            {
//                var scp = new StreamConfiguratonProvider(stream, stream);
//                Assert.IsNotNull(scp);
//            }
//        }

//        [TestMethod()]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void StreamConfiguratonProviderNullArgumentTest()
//        {
//            using (var stream = new MemoryStream())
//            {
//                var scp = new StreamConfiguratonProvider(null, null);
//            }
//        }

//        [TestMethod()]
//        public void GetConfigurationTest()
//        {
//            var inputData = "p1" + Environment.NewLine + "p2" + Environment.NewLine;
//            using (var input = new MemoryStream(Encoding.UTF8.GetBytes(inputData)))
//            using (var output = new StreamReader(new MemoryStream()))
//            {
//                var scp = new StreamConfiguratonProvider(input, output.BaseStream);
//                Assert.IsNotNull(scp);
//                var config = scp.GetConfiguration();
//                Assert.IsNotNull(config);
//                Assert.AreEqual("p1", config.Player1Name);
//                Assert.AreEqual("p2", config.Player2Name);
//            }
//        }
//    }
//}