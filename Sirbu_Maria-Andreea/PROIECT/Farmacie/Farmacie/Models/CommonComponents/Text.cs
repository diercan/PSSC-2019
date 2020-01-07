using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.CommonComponents
{
    public class Text
    {
        private String _string;
        public Text(String str)
        {
            _string = str;
        }
        public String getString { get { return _string; } }
        public Text getText { get { return this; } }
    }
}
