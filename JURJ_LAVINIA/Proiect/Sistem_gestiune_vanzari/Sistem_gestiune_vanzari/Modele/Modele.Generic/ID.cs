using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistem_gestiune_vanzari.Modele.Modele.Generic
{
    public class ID
    {
        private int _valoare;
        public int Valoare { get { return _valoare; } }

        public ID(int valoare)
        {
            //Contract.Requires<ArgumentException>(valoare > 0, "valoare");
            //Contract.Requires<ArgumentException>(valoare <= 10, "valoare");

            _valoare = valoare;
        }

        #region override object
        public override bool Equals(object obj)
        {
            var nota = (ID)obj;
            return Valoare == nota.Valoare;
        }

        public override int GetHashCode()
        {
            return Valoare.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}", Valoare);
        }
        #endregion
    }
}