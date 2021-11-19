using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneysystem.API;
using Moneysystem.Database;
using Moneysystem.Utilities;
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
            Seeder.Seed();
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
        [DataRow("snurreSprätt", "daffyDuck23", "daffyDuck23", "Minesweeper")] // should pass
        [DataRow("grayWolf", "roadRunner23", "roadRunner25", "Production")] // should fail
        [DataTestMethod]
        public void TestRegisterUser(string username, string password, string passwordVerify, string role)
        {
            if (api.GetUser(username) is not null) // username already exists
            {
                bool result = api.Register(username, password, passwordVerify, role);
                Assert.IsFalse(result);
            }
            else if (!password.Equals(passwordVerify)) // if passwords dont match
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
        [DataRow("admin1", "admin1234")]
        [DataTestMethod]
        public void TestLogin(string username, string password)
        {
            int expected = api.Login(username, password);
            Assert.IsTrue(1 == expected);
        }
        [DataRow(1)]
        [DataTestMethod]
        public void TestGetUserByID(int id)
        {
            Assert.IsNotNull(api.GetUser(id));
        }
        [DataRow("admin1")]
        [DataTestMethod]
        public void TestGetUserByName(string username)
        {
            Assert.IsNotNull(api.GetUser(username));
        }
        [DataRow(1)]
        [DataTestMethod]
        public void TestLogout(int userID)
        {
            api.Login("admin1", "admin1234");
            Assert.IsTrue(api.Logout(userID));
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
            var user = api.GetUser("snurreSprätt");
            Assert.IsTrue(api.RemoveUser(user.ID, user.Name, user.Password));
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
            Assert.IsTrue(api.CreateUser("snurre2", "snurreBurre5", "snurreBurre5", "Minesweeper", 10));
        }

        [TestMethod]
        public void TestRemoveUserAdmin()
        {
            int adminId = api.Login("admin1", "admin1234");
            var user = api.GetUser("snurre2");
            Assert.IsTrue(api.RemoveUserAdmin(adminId, user.ID));

        }
    }
}
