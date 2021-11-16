using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestSuite
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPassword()
        {
            Assert.IsTrue(Moneysystem.Utilities.PasswordChecker.CheckPassword("t123abdkq"));
        }
    }
}
