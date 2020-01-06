using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistem_gestiune_vanzari.Models.DDD.Modele_Generic
{
    public class PlainText
    {
        private string _text;
        public string Text { get { return _text; } }
        public PlainText(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new Exception("string is null");
            _text = text;
        }
    }
}