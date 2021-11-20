using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneysystem.API;
using Moneysystem.Utilities;
using Moneysystem.Models;
using System;
using System.Text.RegularExpressions;

namespace TestSuite
{
    [TestClass]
    public class IntegrationTests
    {
        API api = new API();

        [TestInitialize]
        public void TestInit()
        {
            API api = new API();
        }

        [TestMethod]
        public void UserTest()
        {
            UnitTests unitTests = new();
            Account user = new()
            {
                Name = "Gurra1",
                Password = "superHemligt3",
                Role = "Minesweeper"
            };

            unitTests.TestRegisterUser(user.Name, user.Password, user.Password, user.Role);
            unitTests.TestLogin(user.Name, user.Password);
            api.Register(user.Name, user.Password, user.Password, user.Role);
            var testUser = api.GetUser(user.Name, user.Password);
            Assert.IsInstanceOfType(api.GetUser(testUser.ID), typeof(Account));
            api.RemoveUser(testUser.Name, testUser.Password);
            Assert.IsFalse(api.Login(testUser.Name, testUser.Password));
        }
    }
}