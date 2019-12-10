using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Models
{
    public class Medic: Cabinet
    {
        public string MedicAccount { get; set; }

        public string Name { get; set; }
        
        public Cabinet CabinetMedic { get; set; }
    }
}
