using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyExchangeServiceWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeServiceWCF.Tests
{
    [TestClass()]
    public class Service1Tests
    {
        [TestMethod()]
        public void TestConvertCurrencyTest()
        {
            //Assert();
        }

        [TestMethod()]
        public void TestGetCurrencyCodes()
        {
            //string[] x = GetCurrencyCodes();
           // Assert.ThrowsException(GetCurrencyCodes());
            Assert.AreEqual("ALL","ALL");
        }
        [TestMethod()]
        public void TestGetActualConversionRate()
        {
            //string[] x = GetCurrencyCodes();
            // Assert.ThrowsException(GetCurrencyCodes());
            Assert.AreEqual("", "ALL");
        }
    }
}