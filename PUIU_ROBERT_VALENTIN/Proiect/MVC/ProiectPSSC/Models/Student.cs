using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPSSC.Models
{
    public class Student
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string Nume { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string Prenume { get; set; }
        [Range(1, 4)]
        public int An { get; set; }
        [RegularExpression(@"^[A-Z]+[A-Z""'\s-]*$")]
        [Required]
        [StringLength(30)]
        public string Facultate { get; set; }
        [RegularExpression(@"^[0-9""'\s-]*$")]
        [StringLength(3, MinimumLength = 3)]
        [Required]
        public string Camera { get; set; }
        [Range(0, 5)]
        public int Etaj { get; set; }
        [RegularExpression(@"^[A-Z]+[A-Z""'\s-]*$")]
        [Required]
        [StringLength(1)]
        public string Sex { get; set; }
        [Display(Name = "Taxa Achitata")]
        [RegularExpression(@"^[A-Z]+[A-Z""'\s-]*$")]
        [StringLength(2, MinimumLength = 2)]
        [Required]
        public string TaxaAchitata { get; set; }
    }
}
