using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Sistem_gestiune_vanzari.Models.DDD.Modele_Comanda;
using Sistem_gestiune_vanzari.Models.DDD.Modele_Generic;

namespace Sistem_gestiune_vanzari.Models.DDD.Modele_Comanda
{
    public class Comanda
    {
        
        public Id_Produs Id_Produs { get; internal set; }

        private List<Produs> _produseInLista;
        //public ReadOnlyCollection<Produs> ProduseInLista { get { return _produseInLista.AsReadOnly(); } }

        internal Comanda(Id_Produs id_produs)
        {
            Id_Produs = id_produs;
            _produseInLista = new List<Produs>();
        }
        internal Comanda(Id_Produs id_produs, List<Produs> produseInLista):this(id_produs)
        {
            _produseInLista = produseInLista;

        }
        public void AdaugareProduse(Produs produs)
        {
            var gasit = _produseInLista.FirstOrDefault(p => p.Equals(produs));
            if (gasit == null)
            {
                var copieProdus = new Produs(produs.Id); //produs.Nume_Produs
                _produseInLista.Add(copieProdus);
            }
        }
        
    }
}