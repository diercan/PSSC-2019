using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_PSSC.Models
{
    public class Pacient
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public long CNP { get; set; }
        public Sex Sexul { get; set; }
        
        public string Adresa { get; set; }
    }
}
