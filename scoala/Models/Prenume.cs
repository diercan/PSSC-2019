using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace scoala.Models
{
    public class Prenume
    {
        private string _text;
        public string Text { get { return _text; } }

        public Prenume(string text)
        {
            Contract.Requires<ArgumentNullException>(text != null);

            _text = text;
        }

        #region override object
        public override string ToString()
        {
            return Text;
        }

        public override bool Equals(object obj)
        {
            var nume = (Prenume)obj;
            return Text.Equals(nume.Text);
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }
        #endregion
    }
}