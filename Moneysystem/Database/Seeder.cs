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
        /// <summary>
        /// Denna metoden matar in standardvärden i databasen ifall databasen precis har skapats.
        /// </summary>
        public static void Seed()
        {
            using (var db = new Database())
            {
                //if (db.Users.Count() == 0)
                {
                    //db.Users.AddRange(new List<User>
                    //{
                    //    new User {Name = "admin1", Password = "admin1234", IsAdmin = true },
                    //    new User {Name = "user1", Password = "user1234", IsAdmin = false }
                    //});
                    db.SaveChanges();
                }
            }
        }
    }
}
