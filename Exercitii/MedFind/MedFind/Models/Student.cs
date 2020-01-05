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

        private List<Medic> ListMedics = new List<Medic>();
        public void Load(Medic data)
        {
            ListMedics.Add(data);
        }
        public List<Medic> List()
        {
            return ListMedics;
        }
    }
}
