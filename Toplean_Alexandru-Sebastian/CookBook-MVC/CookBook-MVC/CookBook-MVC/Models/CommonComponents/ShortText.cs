using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace CookBook_MVC.Models.CommonComponents
{
    public class ShortText
    {
        private String text;
        public ShortText(String text)
        {
            Contract.Requires(text.Length <= 5, "The input text must be 5 characters or lower");
            this.text = text;
        }
        public String GetText { get { return this.text; } }
    }
}
