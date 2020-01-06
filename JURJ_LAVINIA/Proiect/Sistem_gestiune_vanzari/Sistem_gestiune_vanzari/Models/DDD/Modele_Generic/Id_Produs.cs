using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistem_gestiune_vanzari.Models.DDD.Modele_Generic
{
    public class Id_Produs
    { 
        private PlainText _idProdus;
        public PlainText ID { get { return _idProdus; } }
        public Id_Produs(PlainText idProdus)
        {
            _idProdus = idProdus;
        }

        #region override object
        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            var id_produs = (Id_Produs)obj;
            return ID.Equals(id_produs.ID);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
        #endregion
    }
}