using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Sistem_gestiune_vanzari.Models.DDD.Modele_Comanda
{
    public class Produse
    {
        private List<Produs> _produse;
        public ReadOnlyCollection<Produs> Valori { get { return _produse.AsReadOnly(); } }

        internal Produse()
        {
            _produse = new List<Produs>();
        }
        internal Produse(List<Produs> produse)
        {
            _produse = produse;
        }
        internal void AdaugaProdus(Produs produs)
        {
            _produse.Add(produs);
        }

    }
}