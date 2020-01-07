using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PSSC.Models
{
    public class Rezervare
    {
        public Guid IdUnic { get; set; }
        [Required]
        public string Nume { get; set; }
        [Required]
        public string Prenume { get; set; }
        [Required]

        [DataType(DataType.Date)]
        public DateTime data { get; set; }
        [Required]
        public stareMasina murdarie { get; set; }

        public optiuni optiune1 { get; set; }
        public optiuni optiune2 { get; set; }
        public optiuni optiune3 { get; set; }
        public optiuni optiune4 { get; set; }
    }
    public enum stareMasina
    {
        relativ_curata,
        murdara,
        foarte_murdara
    }
    public enum optiuni
    {
        interior,
        exterior,
        portbagaj,
        ceara
    }
}
