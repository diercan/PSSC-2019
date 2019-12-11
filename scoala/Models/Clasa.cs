using System;

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace scoala.Models
{
    public class Clasa
    {
        private string _nr;
        public string Nr { get { return _nr; } }
        public Clasa(string nr)
        {
            //Constrangeri
            Contract.Requires<ArgumentNullException>(nr != null);
           
                Contract.Requires<ArgumentException>(nr.Length == 2, "Clasa are exact 2 caractere");
           

            _nr = nr;

        }
        #region override object
        public override string ToString()
        {
            return Nr;
        }

        public override bool Equals(object obj)
        {
            var nume = (Clasa)obj;
            return Nr.Equals(nume.Nr);
        }

        public override int GetHashCode()
        {
            return Nr.GetHashCode();
        }
       #endregion

    }
}