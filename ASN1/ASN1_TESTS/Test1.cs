using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyApp; // reference your app namespace

namespace MyApp.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Add_TwoNumbers_ReturnsSum()
        {
            var calc = new Calculator();
            int result = calc.Add(2, 3);
            Assert.AreEqual(5, result);
        }
    }
}
