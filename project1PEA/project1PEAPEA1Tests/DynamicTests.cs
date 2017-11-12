using Microsoft.VisualStudio.TestTools.UnitTesting;
using project1PEA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1PEA.Tests
{
    [TestClass()]
    public class DynamicTests
    {
        [TestMethod()]
        public void GenerateListTest()
        {
            var resultSet = new List<int>();
            var dynamic = new Dynamic();
            var result = dynamic.GenerateList(new[] {1, 2, 3}, 2);
            var expected = new List<int>() {1, 2};
            Assert.Equals(expected, result);
        }
    }
}