using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}
