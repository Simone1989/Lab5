using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        
        public User(string name, string email, bool isAdmin)
        {
            this.Name = name;
            this.Email = email;
            this.IsAdmin = isAdmin;
        }
    }
}
