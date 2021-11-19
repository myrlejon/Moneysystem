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
            using (var db = new Database.Database())
            {
                var user = db.Users.FirstOrDefault(u => u.Name == username);
                if (user == null)
                {
                    db.Users.Add(new Models.User
                    {
                        Name = username,
                        Password = password,
                        Role = role,
                        Salary = Roles.SetBasicSalary(role)
                    });
                    db.SaveChanges();
                    create = true;
                }
                return create;
            }
        }
        /// <summary>
        /// Logs in the user, adds 15 minutes to their sessiontimer.
        /// </summary>
        public int Login(string username, string password)
        {
            int ID = 0;
            using (var db = new Database.Database())
            {
                var user = db.Users.FirstOrDefault(u => u.Name == username);

                if (user != null && username == user.Name && password == user.Password && user.IsActive)
                {
                    user.SessionTimer = DateTime.Now;
                    DateTime newTime = user.SessionTimer.AddMinutes(15);
                    user.SessionTimer = newTime;
                    ID = user.ID;
                }
                db.SaveChanges();
            }
            return ID;
        }

        public Models.Account GetUser(int ID)
        {
            using (var db = new Database.Database())
            {
                var user = db.Users.FirstOrDefault(u => u.ID == ID);
                return user;
            }
        }

        public Models.Account GetUser(string username)
        {
            using (var db = new Database.Database())
            {
                var user = db.Users.FirstOrDefault(u => u.Name == username);
                return user;
            }
        }

        /// <summary>
        /// Logs out the user and resets their session timer.
        /// </summary>
        public bool Logout(int ID)
        {
            using (var db = new Database.Database())
            {
                bool logout = false;
                var user = db.Users.FirstOrDefault(u => u.ID == ID);
                if (user != null)
                {
                    logout = true;
                    user.SessionTimer = DateTime.Now;
                }
                db.SaveChanges();
                return logout;
            }
        }

        public int ViewSalary(int ID) // TODO: I User menyn, stoppa in användarens ID i metoden automatiskt, och för Admin menyn så får man söka på valfri ID.
        {
            using (var db = new Database.Database())
            {
                int salary = 0;
                var user = db.Users.FirstOrDefault(u => u.ID == ID);
                if (user != null)
                {
                    salary = user.Salary;
                }
                return salary;
            }
        }

        public string ViewRole(int ID)
        {
            using (var db = new Database.Database())
            {
                string role = "";
                var user = db.Users.FirstOrDefault(u => u.ID == ID);
                if (user != null)
                {
                    role = user.Role;
                }
                return role;
            }
        }

        public bool RemoveUser(int ID, string username, string password)
        {
            using (var db = new Database.Database())
            {
                bool userDelete = false;
                var user = db.Users.FirstOrDefault(u => u.ID == ID);

                if (user != null && user.Name == username && user.Password == password)
                {
                    userDelete = true;
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                return userDelete;
            }
        }

        /// <summary>
        /// Admin kan inte ta bort sig själv.
        /// </summary>
        public bool RemoveUserAdmin(int adminID, int ID)
        {
            using (var db = new Database.Database())
            {
                bool userDelete = false;
                var user = db.Users.FirstOrDefault(u => u.ID == ID);
                var admin = db.Users.FirstOrDefault(u => u.ID == adminID);

                if (user != null && user.ID != admin.ID)
                {
                    userDelete = true;
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                return userDelete;
            }
        }

        public bool CreateUser(string username, string password, string passwordVerify, string role , int salary = 0)
        {
            bool create = false;
            if (!PasswordChecker.CheckPassword(password)
                || !PasswordChecker.CheckPassword(passwordVerify)
                || !UsernameChecker.CheckUsername(username)
                || !password.Equals(passwordVerify)
                || !Roles.ValidateRole(role))
            {
                return create;
            }
            using (var db = new Database.Database())
            {
                var user = db.Users.FirstOrDefault(u => u.Name == username);

                if (user == null)
                {
                    if(salary == 0) 
                    {
                        salary = Roles.SetBasicSalary(role); // sets salary to the basic for the role
                    }
                    db.Users.Add(new Models.Account
                    {
                        Name = username,
                        Password = password,
                        Salary = salary,
                        Role = role,
                    });
                    db.SaveChanges();
                    create = true;
                }
                return create;
            }
        }

        public List<Models.Account> ListAllUsers()
        {
            List<Models.Account> users = new();
            using (var db = new Database.Database())
            {
                foreach (Models.Account user in db.Users)
                {
                    users.Add(user);
                }
            }
            return users;
        }
    }
}