using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sistem_gestiune_vanzari.Modele.Modele.Generic;

namespace Sistem_gestiune_vanzari.Modele
{
    public class Producator
    {
        public ID id { get; internal set; }
        public PlainText Nume { get; internal set; }

        internal Producator(PlainText nume)
        {
            //Contract.Requires(nume != null, "nume");
            Nume = nume;
        }
        public override string ToString()
        {
            return Nume.ToString();
        }

        public override bool Equals(object obj)
        {
            var producator = (Producator)obj;

            if (producator != null)
            {
                return Nume.Equals(producator.Nume);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Nume.GetHashCode();
        }
    }
}