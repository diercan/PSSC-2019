using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ProiectPSSC.Models
{
    public class StudentFacultateViewModel
    {
        public List<Student> Students { get; set; }
        public SelectList Facultati { get; set; }
        public string Facultate { get; set; }
        public string SearchString { get; set; }
    }
}
