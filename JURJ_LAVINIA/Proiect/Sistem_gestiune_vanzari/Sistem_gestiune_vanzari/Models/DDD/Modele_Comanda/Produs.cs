using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sistem_gestiune_vanzari.Models.DDD.Modele_Generic;

namespace Sistem_gestiune_vanzari.Models.DDD.Modele_Comanda
{
    public class Produs
    {
        public Id_Produs Id { get; internal set; }
        public PlainText Nume_Produs { get; internal set; }
        public PlainText Descriere { get; internal set; }
        public Cantitate Cantitate { get; internal set; }
        public Bani Pret { get; internal set; }

        internal Produs(Id_Produs id)
        {
            Id = id;
            //Nume_Produs = nume_produs;
        }
        public Produs ModificareCantitate(int numar_produse) // ModificareCantitate(Cantitate cantitate){ ... }
        {
            if (Cantitate.ValoareInt > 0 && Cantitate.ValoareInt - numar_produse > 0)
                Cantitate = new Cantitate(Cantitate.ValoareInt - numar_produse);
            return this;
        }
    }
}