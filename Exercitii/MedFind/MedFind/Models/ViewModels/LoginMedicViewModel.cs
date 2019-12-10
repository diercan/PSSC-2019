using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Models.ViewModels
{
    public class LoginMedicViewModel
    {
        [Required]
        public string MedicAccount { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
