using System;
using System.Collections.Generic;
using System.Linq;

namespace Moneysystem.API
{
    public class API
    {
        /// <summary>
        /// Registers a new user if username isn't not taken.
        /// </summary>
        public bool Register(string username, string password, string passwordVerify) // TODO: Lägga till obligatorisk salary och role vid registrering?
        {
            using (var db = new Database.Database())
            {
                bool create = false;
                var user = db.Users.FirstOrDefault(u => u.Name == username);
                if (user == null && String.IsNullOrEmpty(password) == true)
                {
                    db.Users.Add(new User
                    {
                        Name = username
                    });
                    db.SaveChanges();
                    create = true;
                }
                else if (user == null && password == passwordVerify)
                {
                    db.Users.Add(new User
                    {
                        Name = username,
                        Password = password,
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
    }
}