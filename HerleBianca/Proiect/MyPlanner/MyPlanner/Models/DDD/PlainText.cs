using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlanner.Models.DDD
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

        #region override object
        public override string ToString()
        {
            return Text;
        }

        public override bool Equals(object obj)
        {
            var nume = new PlainText((string)obj);
            return Text.Equals(nume.Text);
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }
        #endregion
    }
}
