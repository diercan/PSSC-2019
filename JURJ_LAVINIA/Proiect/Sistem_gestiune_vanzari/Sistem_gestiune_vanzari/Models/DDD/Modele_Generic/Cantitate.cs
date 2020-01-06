using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistem_gestiune_vanzari.Models.DDD.Modele_Generic
{
    public class Cantitate
    {
        private int _cantitate = 0;
        public int ValoareInt { get => _cantitate; }

        public Cantitate(int cantitate)
        {
            if (cantitate < 0)
                _cantitate = cantitate;
        }
    }
}