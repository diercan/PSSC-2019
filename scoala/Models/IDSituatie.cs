using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace scoala.Models
{
    public class IDSituatie
    {
        private string _numar;
        public string Numar { get { return _numar; } }
        public IDSituatie(string numar)
        {
            Contract.Requires<ArgumentNullException>(numar != null);
            Contract.Requires<ArgumentException>(numar.Length == 3, "Id are exact 3 caractere");

            _numar = numar;

        }

        #region override object
        public override string ToString()
        {
            return Numar;
        }

        public override bool Equals(object obj)
        {
            var nume = (IDElev)obj;
            return Numar.Equals(nume.Numar);
        }

        public override int GetHashCode()
        {
            return Numar.GetHashCode();
        }
        #endregion
    }
}