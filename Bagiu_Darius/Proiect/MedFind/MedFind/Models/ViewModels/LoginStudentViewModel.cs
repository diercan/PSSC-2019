using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Models
{
    public class LoginStudentViewModel
    {
        [Display(Name = "Account")]
        [Required(ErrorMessage = "Account is required!")]
        [StringLength(100, ErrorMessage = "Must be between 5 and 100 characters", MinimumLength = 5)]
        public string StudentAccount { get; set; }

        [Required(ErrorMessage = "Password is required!"), DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Must be between 5 and 100 characters", MinimumLength = 5)]
        public string Password { get; set; }
        /*
        [Display(Name="Remember Me")]
        public bool RememberMe { get; set; }*/
    }
}
