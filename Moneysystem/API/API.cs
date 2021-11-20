using System;
using System.Collections.Generic;
using System.Linq;
using Moneysystem.Utilities;
using Moneysystem.Models;

namespace Moneysystem.API
{
    public class API
    {
        public List<Account> accounts = new();
        public int idCounter = 3;
        public List<Account> Accounts { get{return accounts;} set{accounts = value;}}

        public API()
        {
            accounts.Add(new Admin{
                ID = 1,
                Name="admin1",
                Password="admin1234",
                IsAdmin = true,
                Role = "Administrator",
                Salary = Roles.SetBasicSalary("Administrator")
                });
            accounts.Add(new User{
                ID = 2,
                Name="user1",
                Password="user1234",
                IsAdmin = false,
                Role = "User",
                Salary = Roles.SetBasicSalary("User")
                });
            
        }
        /// <summary>
        /// Registers a new user if username isn't not taken.
        /// </summary>
        public bool Register(string username, string password, string passwordVerify,
        string role, int salary = 0) 
        {
            if (!PasswordChecker.CheckPassword(password)
                || !PasswordChecker.CheckPassword(passwordVerify)
                || !UsernameChecker.CheckUsername(username)
                || !password.Equals(passwordVerify)
                || !Roles.ValidateRole(role))
            {
                return false;
            }
            foreach(var item in Accounts)
            {
                if(item.Name.Equals(username))
                {
                    return false;
                }
            }
            if(salary == 0)
            {
                salary = Roles.SetBasicSalary(role);
            }
            Accounts.Add(new User{
                ID = this.idCounter++,
                Name = username,
                Password = password,
                Salary = salary,
                Role = role
            });
            return true;
        }
        /// <summary>
        /// Logs in the user, adds 15 minutes to their sessiontimer.
        /// </summary>
        public bool Login(string username, string password)
        {
            bool login = false;
            if(username.Length < 3 || password.Length < 2)
            {
                return login;
            }
            foreach(var user in Accounts)
            {
                if (user.Name == username && user.Password == password)
                {
                    login = true;
                }
            }
            return login;
        }

        public Account GetUser(string username, string password)
        {
            Account getUser = new();
            foreach(var user in Accounts)
            {
                if (user.Name == username && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }

        public Models.Account GetUser(int id)
        {
            foreach(var item in Accounts){
                if (id == item.ID)
                {
                    return item;
                }
            }
            return new Models.Account();           
        }

        public int ViewSalary(int ID) 
        {
            int salary = 0;
            foreach(var user in Accounts)
            {
                if (ID == user.ID)
                {
                    salary = user.Salary;
                }
            }
            return salary;
        }

        public string ViewRole(int ID)
        {
            string role = "";
            foreach(var user in Accounts)
            {
                if (ID == user.ID)
                {
                    role = user.Role;
                }
            }
            return role;
        }

        public int CreateID(List<Models.Account> accounts)
        {
            int highest = 0;
            foreach(var user in accounts)
            {
                if(user.ID > highest)
                {
                    highest = user.ID;
                }
            }
            return highest + 1;
        }

        public bool RemoveUser(string username, string password)
        {
            foreach(var item in Accounts)
            {
                if(item.Name.Equals(username) && item.Password.Equals(password) && item.IsAdmin == false)
                {
                    Accounts.Remove(item);
                    return true;
                }
            }
            return false;
        }

        public List<Account> ListAllUsers()
        {
            return Accounts;
        }
    }
}