using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeTest.Models
{
    public class Test
    {
        public string TestId { get;set; }
        public string TestTitle { get; set; }
        public Tester Author { get; set; }

        public Functionality Functionality { get; set; }
    }
}
