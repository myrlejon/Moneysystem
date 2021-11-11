using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moneysystem.Models;

namespace Moneysystem.Database
{
    public class Seeder
    {
        public static void Seed()
        {
            using (var db = new Database())
            {
                if (db.Users.Count() == 0)
                {
                    db.Users.AddRange(new List<Users>
                    {
                       new Users {Name = "admin1", Password = "admin1234", IsAdmin = true , Salary = 500, Role = "Administrator"},
                       new Users {Name = "user1", Password = "user1234", IsAdmin = false , Salary = 100, Role = "User"}
                    });
                    db.SaveChanges();
                }
            }
        }
    }
}
