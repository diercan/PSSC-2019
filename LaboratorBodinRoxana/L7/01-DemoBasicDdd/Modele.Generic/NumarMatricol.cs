using Modele.Generic.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Modele.Generic
{
    public class NumarMatricol
    {
        private string _numar;
        public string Numar { get { return _numar; } }

        public NumarMatricol(string numar)
        {
            //Contract.Requires<ArgumentNullException>(numar != null, "text");
            //Contract.Requires<ArgumentCannotBeEmptyStringException>(!string.IsNullOrEmpty(numar), "text");
            //Contract.Requires<ArgumentException>(numar.Length==7, "Numarul matricol are exact 7 caractere.");

            _numar = numar;
        }


        #region override object
        public override string ToString()
        {
            return Numar;
        }

        public override bool Equals(object obj)
        {
            var nume = (NumarMatricol)obj;
            return Numar.Equals(nume.Numar);
        }

        public override int GetHashCode()
        {
            return Numar.GetHashCode();
        }
        #endregion
    }
}
