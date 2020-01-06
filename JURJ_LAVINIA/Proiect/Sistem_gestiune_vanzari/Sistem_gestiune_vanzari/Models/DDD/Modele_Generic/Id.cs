using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistem_gestiune_vanzari.Models.DDD.Modele_Generic
{
    public class Id
    {
        private int _id;
        public int ID { get { return _id; } }
        public Id(int id)
        {
            _id = id;
        }
    }
}