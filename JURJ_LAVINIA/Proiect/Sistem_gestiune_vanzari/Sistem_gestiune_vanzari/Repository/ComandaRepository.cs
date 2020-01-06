using Sistem_gestiune_vanzari.Database;
using Sistem_gestiune_vanzari.Models.DDD.Modele_Comanda;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace Sistem_gestiune_vanzari.Repository
{
    public class ComandaRepository : IComandaRepository
    {
        private List<Comanda> _comenzi = new List<Comanda>();

        public ComandaRepository()
        {

        }
        public void AdaugaComanda(Comanda comanda)
        {
            var result = _comenzi.FirstOrDefault(d => d.Equals(comanda));

            if (result != null) throw new DuplicateWaitObjectException();

            _comenzi.Add(comanda);

        }


    }
}