using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeTest.Models
{
    public class Tester
    {
        [Required]
        public string TesterId { get; set; }
        
        [Required]
        public string TesterName { get; set; }

        public ICollection<Test> Tests { get; set; }
    }
}
