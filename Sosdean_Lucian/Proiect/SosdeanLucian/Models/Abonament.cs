using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SosdeanLucian.Models
{
    public class Abonament
    {
        public int AbonamentID { get; set; }
        public int OfertaID { get; set; }
        public int ClientID { get; set; }

        public virtual Client Client {get;set;}
        public virtual Oferta Oferta { get; set; }

    }
}