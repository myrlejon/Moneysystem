using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneysystem.API;
using Moneysystem.Database;
using Moneysystem.Utilities;
using Moneysystem.Models;
using System;
using System.Text.RegularExpressions;

namespace TestSuite
{
    public class IntegrationTests
    {
        API api = new API();

        [TestInitialize]
        public void TestInit()
        {
            Seeder.Seed();
        }

        [TestMethod]
        public void UserTest()
        {
            UnitTests unitTests = new();
            Account user = new()
            {
                Name = "Gurra",
                Password = "superHemligt3",
                Role = "Minesweeper"
            };

            unitTests.TestRegisterUser(user.Name, user.Password, user.Password, user.Role);
            unitTests.TestLogin(user.Name, user.Password);
            unitTests.TestGetUserByName(user.Name);
            var testUser = api.GetUser(user.Name);
            unitTests.TestLogout(testUser.ID);
            Assert.IsInstanceOfType(api.GetUser(testUser.ID), typeof(Account));
            api.RemoveUser(testUser.ID, testUser.Name, testUser.Password);
        }
    }
}