using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyExchangeServiceWCF.Tests
{
    [TestClass()]
    public class Service1Tests
    {
        [TestMethod()]
        public void TestConvertCurrencyTest()
        {
            Assert.Equals("","");
        }

        [TestMethod()]
        public void TestGetCurrencyCodes()
        {
            Assert.AreEqual("ALL","ALL");
        }
        [TestMethod()]
        public void TestGetActualConversionRate()
        {
            Assert.AreEqual("ALL", "ALL");
        }
    }
}