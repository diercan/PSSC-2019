using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sistem_gestiune_vanzari.Models.DDD.Modele_Generic;

namespace Sistem_gestiune_vanzari.Models.DDD.Modele_Comanda
{
    public class Client
    {
        public Id Id { get; internal set; }
        public PlainText Nume_Client { get; internal set; }
       
        internal Client(PlainText nume_client)
        {
            Nume_Client = nume_client;
        }
    }
}