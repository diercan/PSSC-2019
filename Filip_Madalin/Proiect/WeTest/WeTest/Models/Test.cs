using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeTest.Models
{
    public class Test
    {   
        [Required]
        public string TestId { get;set; }
        [Required]
        public string TestTitle { get; set; }

        public int? TesterId { get; set; }
        public Tester Tester { get; set; }
        [Required]
        public Functionality Functionality { get; set; }
    }
}
