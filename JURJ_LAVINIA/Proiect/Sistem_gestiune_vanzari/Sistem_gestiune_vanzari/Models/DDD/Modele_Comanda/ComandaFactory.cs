using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sistem_gestiune_vanzari.Models.DDD.Modele_Generic;

namespace Sistem_gestiune_vanzari.Models.DDD.Modele_Comanda
{
    public class ComandaFactory
    {
        //A Factory is an object that has the single responsibility of creating other objects.
        public static readonly ComandaFactory Instance = new ComandaFactory();
        private ComandaFactory()
        {

        }

        //public Comanda CreareComanda(Id_produs comanda)
        //{
        //    var comanda = new Comanda(comanda);
        //    return comanda;
        //}


       
    }
}