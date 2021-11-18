using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneysystem.Utilities;
using System;
using System.Text.RegularExpressions;

namespace TestSuite
{
    [TestClass]
    public class UnitTest1
    {
        [DataRow("")]
        [DataRow("t123abdkq")]
        [DataRow("t123abdkq!")]
        [DataTestMethod]
        public void TestPassword(string value)
        {
            Regex regexItem = new Regex("^[a-zA-Z0-9]+$");
            bool result = regexItem.IsMatch(value);
            if (result)
            {
                Assert.IsTrue(PasswordChecker.CheckPassword(value));
            }
            else
            {
                Assert.IsFalse(PasswordChecker.CheckPassword(value));
            }
        }
    }
}
