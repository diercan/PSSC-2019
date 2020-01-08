using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PSSC.Models
{
    public class Admin
    {
        [Required]
        public string username = "admin";

        [Required]
        [DataType(DataType.Password)]
        public string Password = "1234";

        public Guid IdUnic { get; set; }
        [Required]
        public string Nume { get; set; }
        [Required]
        public string Prenume { get; set; }
        [Required]

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        [Display(Name = "Data+ora")]
        public DateTime data { get; set; }
        [Required]
        public stareMasina murdarie { get; set; }

        public optiuni optiune1 { get; set; }
        public optiuni optiune2 { get; set; }
        public optiuni optiune3 { get; set; }
        public optiuni optiune4 { get; set; }
        public double total { get; set; }
    }
}
