using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPSSC.Models
{
    public class CaminConfig
    {
        [Range(1, 100)]
        public int NrCamere { get; set; }
        public string Module { get; set; }
        public string Parter { get; set; }
        public string Etaj1 { get; set; }
        public string Etaj2 { get; set; }
        public string Etaj3 { get; set; }
        public string Etaj4 { get; set; }
        public string Etaj5 { get; set; }
    }
}
