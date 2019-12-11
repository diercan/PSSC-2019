using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace scoala.Models
{
    public class Materie
    {
        private string _text;
        public string Text { get { return _text; } }

        public Materie(string text)
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
            var nume = (Nume)obj;
            return Text.Equals(nume.Text);
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }
        #endregion
    
}
}