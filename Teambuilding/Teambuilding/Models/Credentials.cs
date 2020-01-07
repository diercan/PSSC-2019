using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Teambuilding.Models
{
    public class Credentials
    {
        [Required]
        public string userName { get; set; }

        [Required]
        public string password { get; set; }
        
        public Credentials(string userName, string password) 
        {
            this.userName = userName;
            this.password = password;
        }

        public Credentials() { }
    }
}
