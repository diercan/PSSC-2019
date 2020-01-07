using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LegumeDeBelint.Models
{
    public class RegisterViewModel
    {


        [Required]
        [EmailAddress]
        [Display(Name = "Adresă de email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Parola trebuie să conțină minim 6 caractere!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parolă")]

        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirmarea parolei")]
        [Compare("Password", ErrorMessage = "Cele două parole nu coincid!")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nr. telefon")]
        public string Phone { get; set; }
    }


    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Adresă de email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parolă")]
        public string Password { get; set; }

        [Display(Name = "Ținne-mă minte")]
        public bool RememberMe { get; set; }
    }
}