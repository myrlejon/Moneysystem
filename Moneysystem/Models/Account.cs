using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Moneysystem.Models;

namespace Moneysystem.Models
{
    public class Account
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; } = "user1234";
        public bool IsAdmin { get; set; } = false;
        public int Salary { get; set; }
        public string Role { get; set; } = "User";
    }
}
