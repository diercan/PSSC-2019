using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Sistem_gestiune_vanzari.Modele.Modele.Generic;

namespace Sistem_gestiune_vanzari.Modele
{
    public class Client
    {
        public ID id { get; internal set; }
        public PlainText Nume { get; internal set; }

        internal Client(PlainText nume)
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
            var client = (Client)obj;

            if (client != null)
            {
                return Nume.Equals(client.Nume);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Nume.GetHashCode();
        }



    }
}