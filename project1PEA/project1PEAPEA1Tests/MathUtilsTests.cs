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
    public class MathUtilsTests
    {
        [TestMethod()]
        public void GetMinimumElementIndexTest()
        {
            
            var result = MathUtils.GetMinimumElementIndex(new List<double>() {0.0, 2.3, 1.2, 0.0}, false);
            Assert.AreEqual(0,result);

        }
    }
}