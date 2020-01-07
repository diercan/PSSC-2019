using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.CommonComponents
{
    public class Number
    {
        private int _int;
        public Number(int intt)
        {
            _int = intt;
        }
        public int getNumber { get { return _int; } }
        public Number getText { get { return this; } }
    }
}

