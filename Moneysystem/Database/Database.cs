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
        public DbSet<Users> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($@"Server=.\SQLExpress;Database={DatabaseName}; Trusted_Connection = true;");
        }
    }
}
