using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SosdeanLucian.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public string NumeClient { get; set; }
        public string Telefon { get; set; }

        public virtual ICollection<Abonament> Abonamente { get; set; }
    }
}