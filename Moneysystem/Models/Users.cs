﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Moneysystem.Models
{
    public class Users
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; } = "user1234";
        public DateTime SessionTimer { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public bool IsAdmin { get; set; } = false;
    }
}
