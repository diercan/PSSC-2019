using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PSSC.Models
{
    public class User
    {
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public User()
        {

        }
        public User(string u, string p)
        {
            Username = u;
            Password = p;
        }
    }
}
