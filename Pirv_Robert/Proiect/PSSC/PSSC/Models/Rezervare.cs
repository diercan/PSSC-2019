using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSSC.Models
{
    public class Rezervare
    {
        public Guid IdUnic { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public DateTime data { get; set; }
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