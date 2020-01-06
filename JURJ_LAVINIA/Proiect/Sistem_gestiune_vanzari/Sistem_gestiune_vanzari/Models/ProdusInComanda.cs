using Sistem_gestiune_vanzari.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistem_gestiune_vanzari.Models
{
    public class ProdusInComanda
    {
        public Tabel_Produs Produs { get; set; }
        public int Cantitate { get; set; }
        public Tabel_Client Client { get; set;}

    }
}