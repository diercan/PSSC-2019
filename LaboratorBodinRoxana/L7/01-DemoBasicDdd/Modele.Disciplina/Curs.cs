using Modele.Generic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Modele.Disciplina
{
    public class Curs
    {
        public PlainText Nume { get; internal set; }
        public Uri LinkContinut { get; internal set; }

        internal Curs(PlainText nume)
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
            var curs = (Curs)obj;

            if (curs != null)
            {
                return Nume.Equals(curs.Nume);
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
