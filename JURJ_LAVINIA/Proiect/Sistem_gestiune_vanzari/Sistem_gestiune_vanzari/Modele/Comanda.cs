using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Sistem_gestiune_vanzari.Modele.Modele.Generic;

namespace Sistem_gestiune_vanzari.Modele
{
    public class Comanda
    {
        public ID ID_Comanda { get; internal set; }
        public Client Client { get; internal set; }
        public Producator Producator { get; internal set; }

        private List<Produs> _listaDeProduse;
        public ReadOnlyCollection<Produs> ListaProduse { get { return _listaDeProduse.AsReadOnly(); } }

        internal Comanda(ID id_comanda)
        {
            //Contract.Requires(nume != null, "nume");
            //Contract.Requires(pondereExamen != null, "pondereExamen");

            ID_Comanda = id_comanda;
            _listaDeProduse = new List<Produs>();
        }
        internal Comanda(ID id_comanda, List<Produs> listaDeProduse) : this(id_comanda)
        {
            _listaDeProduse = listaDeProduse;

        }
        public void AdaugareProdus(Produs produs)
        {
            //Contract.Requires(student != null, "student");
            //Contract.Requires(Stare == StareDisciplina.Inscrieri, "nu suntem in perioada in care se fac inscrieri");

            var gasit = _listaDeProduse.FirstOrDefault(p => p.Equals(produs));
            if (gasit == null)
            {
                var copieProdus = new Produs(produs.id_produs, produs.Nume);
                _listaDeProduse.Add(copieProdus);
            }
            else
            {
                
            }
        }

    }
}