using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSSC.Models.Modele.Generic
{
    public class PlainText
    {
        private string _text;
        public string Text { get { return _text; } }

        public PlainText(string text)
        {
            //Contract.Requires<ArgumentNullException>(text != null, "text");
            //Contract.Requires<ArgumentCannotBeEmptyStringException>(!string.IsNullOrEmpty(text), "text");

            _text = text;
        }

        public override string ToString()
        {
            return Text;
        }

        public override bool Equals(object obj)
        {
            var text = (PlainText)obj;
            return Text.Equals(text.Text);
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }

    }
}