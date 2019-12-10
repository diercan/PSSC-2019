using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Models
{
    public class LoginStudentViewModel
    {
        [Required]
        public string StudentAccount { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /*
        [Display(Name="Remember Me")]
        public bool RememberMe { get; set; }*/
    }
}
