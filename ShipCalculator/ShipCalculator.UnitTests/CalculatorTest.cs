using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator_BLL.Repository;

namespace ShipCalculator.UnitTests
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void CalculateStops_CorrectValue()
        {
            var repo = new Calculator();
            var MGLB = "40";
            var distance = 90000000;
            var consumables = "6 years";
            
            var result = repo.CalculateStops(MGLB, consumables , distance);

            Assert.IsNotNull(result);
            Assert.AreNotEqual("unknown", result);
            Assert.AreEqual(result, "43");
            Assert.IsInstanceOfType(result, typeof(string));

        }

        [TestMethod]
        public void CalculateStops_returnUnknownbyMGLB()
        {
            var repo = new Calculator();
            var result = repo.CalculateStops("unknown", null, 0);

            Assert.IsNotNull(result);
            Assert.AreEqual("unknown", result);
        }

        [TestMethod]
        public void CalculateStops_returnUnknownbyconsumables()
        {
            var repo = new Calculator();
            var result = repo.CalculateStops(null, "unknown", 0);

            Assert.IsNotNull(result);
            Assert.AreEqual("unknown", result);
        }

    }
}
