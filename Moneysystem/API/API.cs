using System;
using System.Collections.Generic;
using System.Linq;
using Moneysystem.Utilities;

namespace Moneysystem.API
{
    public class API
    {
        /// <summary>
        /// Registers a new user if username isn't not taken.
        /// </summary>
        public bool Register(string username, string password, string passwordVerify,
        string role) // TODO: Lägga till obligatorisk salary och role vid registrering?
        {
            bool create = false;
            if (!PasswordChecker.CheckPassword(password)
                || !PasswordChecker.CheckPassword(passwordVerify)
                || !password.Equals(passwordVerify)
                || !Roles.ValidateRole(role))
            {
                return create;
            }
            return create;
        }
        /// <summary>
        /// Logs in the user, adds 15 minutes to their sessiontimer.
        /// </summary>
        public bool Login(string username, string password, List<Models.Account> accounts)
        {
            bool login = false;
            foreach(var user in accounts)
            {
                if (user.Name == username && user.Password == password)
                {
                    login = true;
                }
            }
            return login;
        }

        public Models.Account GetUser(string username, string password, List<Models.Account> accounts)
        {
            Models.Account getUser = new();
            foreach(var user in accounts)
            {
                if (user.Name == username && user.Password == password)
                {
                    getUser = user;
                }
            }
            return getUser;
        }

        // public Models.Account GetUser(string username)
        // {
            
        // }


        // public bool Logout(int ID)
        // {
            
        // }

        public int ViewSalary(int ID, List<Models.Account> accounts) 
        {
            int salary = 0;
            foreach(var user in accounts)
            {
                if (ID == user.ID)
                {
                    salary = user.Salary;
                }
            }
            return salary;
        }

        public string ViewRole(int ID, List<Models.Account> accounts)
        {
            string role = "";
            foreach(var user in accounts)
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

        // public bool RemoveUser(int ID, List<Models.Account> accounts)
        // {
        //     user

        //     accounts.Remove();
        //     bool userDelete = false;
        // }

        // public bool CreateUser(string username, string password, string passwordVerify, int salary, string role)
        // {
        //     bool create = false;
        //     if (!PasswordChecker.CheckPassword(password)
        //         || !PasswordChecker.CheckPassword(passwordVerify)
        //         || !password.Equals(passwordVerify)
        //         || !Roles.ValidateRole(role))
        //     {
        //         return create;
        //     }
        //     using (var db = new Database.Database())
        //     {
        //         var user = db.Users.FirstOrDefault(u => u.Name == username);

        //         if (user == null)
        //         {
        //             db.Users.Add(new Models.Account
        //             {
        //                 Name = username,
        //                 Password = password,
        //                 Salary = salary,
        //                 Role = role,
        //             });
        //             db.SaveChanges();
        //             create = true;
        //         }
        //         return create;
        //     }
        // }

        // public List<Models.Account> ListAllUsers()
        // {
        //     List<Models.Account> users = new();
        //     using (var db = new Database.Database())
        //     {
        //         foreach (Models.Account user in db.Users)
        //         {
        //             users.Add(user);
        //         }
        //     }
        //     return users;
        // }
    }
}