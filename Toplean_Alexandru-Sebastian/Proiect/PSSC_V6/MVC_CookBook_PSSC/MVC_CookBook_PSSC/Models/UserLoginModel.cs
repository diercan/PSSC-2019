using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CookBook_PSSC.Models
{
    public class UserLoginModel
    {
        public UserLoginModel()
        {

        }
        
        [Key]
        public int ID { get; set; }

        [Required]
        public String strUsername { get; set; }
        [Required]
        public String strPassword { get; set; }
    }
}
