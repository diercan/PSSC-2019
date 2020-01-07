using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Models
{
    public class Student
    {
        public string StudentAccount { get; set; }
        public string StudentID { get; set; }
        public string Name { get; set; }

        public List<Medic> ListCabinets { get; set; } = new List<Medic>();

        public List<Medic> List()
        {
            return ListCabinets;
        }
    }
}
