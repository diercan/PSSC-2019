using Modele.Generic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Modele.Disciplina
{
    public class Laborator
    {
        public PlainText Nume { get; internal set; }

        //aceasta proprietate ar putea fi si o lista de linkuri
        public Uri LinkContinut { get; internal set; }

        internal Laborator(PlainText nume)
        {
            //Contract.Requires(nume != null, "nume");
            Nume = nume;
        }

        #region operations
        internal void ActualizareLinkContinut(Uri url)
        {
            //Contract.Requires(url != null, "url");
            LinkContinut = url;
        }
        #endregion

        #region override object
        public override string ToString()
        {
            return Nume.ToString();
        }

        public override bool Equals(object obj)
        {
            var laborator = (Laborator)obj;

            if (laborator != null)
            {
                return Nume.Equals(laborator.Nume);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Nume.GetHashCode();
        }
        #endregion

    }
}
