using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Models.ViewModels
{
    public class LoginMedicViewModel
    {
        
        [Display(Name = "User name")]
        [Required(ErrorMessage = "Username is required!")]
        [StringLength(100, ErrorMessage = "Must be between 5 and 100 characters", MinimumLength = 5)]
        public string MedicAccount { get; set; }

        
        [Required(ErrorMessage = "Password is required!"), DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Must be between 5 and 100 characters", MinimumLength = 5)]
        public string Password { get; set; }
    }
}
