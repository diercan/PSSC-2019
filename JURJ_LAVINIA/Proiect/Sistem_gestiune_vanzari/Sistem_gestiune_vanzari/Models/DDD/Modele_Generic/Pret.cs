using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistem_gestiune_vanzari.Models.DDD.Modele_Generic
{
    public class Bani
    {
        private decimal _bani;
        public decimal ValoareDecimal { get { return _bani; } }

        public Bani(decimal bani)
        {
            _bani = bani;
        }
    }
}