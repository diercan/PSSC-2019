using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Modele.Generic
{
    public class Coeficient
    {
        private int _numarator;
        private int _numitor;
        
        public decimal Fractie { get { return (decimal)_numarator/(decimal)_numitor; } }

        internal Coeficient(int numarator, int numitor)
        {
            //Contract.Requires<ArgumentException>(numarator > 0, "numarator");
            //Contract.Requires<ArgumentException>(numitor > 0, "numitor");
            //Contract.Requires<ArgumentException>(numitor > numarator, "nu este subunitar");

            _numitor = numitor;
            _numarator = numarator;
        }

        #region override object
        public override bool Equals(object obj)
        {
            var coeficient = (Coeficient)obj;
            return coeficient._numarator == _numarator && coeficient._numitor == _numitor;
        }

        public override int GetHashCode()
        {
            return Fractie.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", _numarator, _numitor);
        }
        #endregion
    }
}
