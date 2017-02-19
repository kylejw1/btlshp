using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Tests
{
    [TestClass()]
    public class ResolverTests
    {
        [TestMethod()]
        public void RegisterResolveObjectTest()
        {
            // Due to static class, initialize string resolve to null
            Resolver.RegisterObject(typeof(string), null);
            Assert.AreEqual(null, Resolver.Resolve<string>());

            var resolved = "not null";
            Resolver.RegisterObject(typeof(string), resolved);
            Assert.AreEqual(resolved, Resolver.Resolve<string>());
        }

        [TestMethod()]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void ResolveNonExistantDependencyTest()
        {
            var resolved = Resolver.Resolve<Int64>();
        }
    }
}