using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sistem_gestiune_vanzari.Modele.Modele.Generic;

namespace Sistem_gestiune_vanzari.Modele
{
    public class Produs
    {
        public ID id_produs { get; internal set; }
        public PlainText Nume { get; internal set; }
        public Cantitate Cantitate { get; internal set; }
        internal Produs(ID id, PlainText nume)
        {
            //Contract.Requires(nrMatricol != null, "numar matricol");
            //Contract.Requires(nume != null, "nume");
            id_produs = id;
            Nume = nume;
        }





    }
}