using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TakeAuction.Models
{
    public class Licitatii
    { 
        public int Id { get; set; }

        public string NumeProdus { get; set; }
        public double Pret { get; set; }

        public string User { get; set; }
    }
}