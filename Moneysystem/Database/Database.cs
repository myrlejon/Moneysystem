using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moneysystem.Models;

namespace Moneysystem.Database
{
    public class Database : DbContext
    {
        private const string DatabaseName = "Moneysystem";
        //public DbSet<User> Users { get; set; }
        //public DbSet<Book> Books { get; set; }
        //public DbSet<BookCategory> BookCategory { get; set; }
        //public DbSet<SoldBooks> SoldBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($@"Server=.\SQLExpress;Database={DatabaseName}; Trusted_Connection = true;");
        }
    }
}
