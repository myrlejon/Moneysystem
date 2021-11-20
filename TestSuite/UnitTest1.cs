using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneysystem.API;
using Moneysystem.Utilities;
using Moneysystem.Models;
using System;
using System.Text.RegularExpressions;

namespace TestSuite
{
    [TestClass]
    public class UnitTests
    {
        API api = new API();

        [TestInitialize]
        public void TestInit()
        {
            API api = new API();
        }

        [DataRow("")] // should fail
        [DataRow("t123abdkq")] // should pass
        [DataRow("t123abdkq!")] // should fail
        [DataTestMethod]
        public void TestPassword(string value)
        {
            Regex regexItem = new Regex("^[a-zA-Z0-9]+$"); // allowed chars: a-z, A-Z & 0-9
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

        [DataRow("admin1", "admin1234", "admin1234", "Administrator")] // should fail, existing user
        [DataRow("gurra231234", "daffyDuck23", "daffyDuck23", "Minesweeper")] // should pass
        [DataRow("grayWolf1", "roadRunner23", "roadRunner25", "Production")] // should fail
        [DataTestMethod]
        public void TestRegisterUser(string username, string password, string passwordVerify, string role)
        {
            if(api.GetUser(username, password) is null)
            {
                if (!password.Equals(passwordVerify)) // if passwords dont match
                {
                    bool result = api.Register(username, password, passwordVerify, role);
                    Assert.IsFalse(result);
                }
                else
                {
                    bool result = api.Register(username, password, passwordVerify, role);
                    Assert.IsTrue(result);
                }

            }
            else // user already exists
            {
                bool result = api.Register(username, password, passwordVerify, role);
                Assert.IsFalse(result);
            }
        }
        [DataRow("admin1", "admin1234")]
        [DataTestMethod]
        public void TestLogin(string username, string password)
        {
            bool expected = api.Login(username, password);
            Assert.IsTrue(expected);
        }
        [DataRow(1)]
        [DataTestMethod]
        public void TestGetUserByID(int id)
        {
            Assert.IsNotNull(api.GetUser(id));
        }
        [DataRow("admin1", "admin1234")]
        [DataTestMethod]
        public void TestGetUserByName(string username, string password)
        {
            Assert.IsNotNull(api.GetUser(username, password));
        }


        [TestMethod]
        public void TestViewSalary()
        {
            api.Login("admin1", "admin1234");
            Assert.IsTrue(api.ViewSalary(1) == 500);
        }
        [TestMethod]
        public void TestViewRole()
        {
            api.Login("admin1", "admin1234");
            Assert.IsTrue(api.ViewRole(1) == "Administrator");
        }
        [TestMethod]
        public void TestRemoveUser()
        {
            api.Register("snurreSprätt2", "daffyDuck23", "daffyDuck23", "Minesweeper", 10);
            var user = api.GetUser("snurreSprätt2", "daffyDuck23");
            Assert.IsTrue(api.RemoveUser(user.Name, user.Password));
        }
        [TestMethod]
        public void TestListAllUsers()
        {
            var list = api.ListAllUsers();
            Assert.IsTrue(list is not null);
        }
        [TestMethod]
        public void TestCreateUser()
        {
            Assert.IsTrue(api.Register("snurre2", "snurreBurre5", "snurreBurre5", "Minesweeper",10));
        }
    }
}
