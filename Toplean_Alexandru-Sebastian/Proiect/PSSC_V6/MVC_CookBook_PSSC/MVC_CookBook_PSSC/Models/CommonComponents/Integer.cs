using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CookBook_PSSC.Models.CommonComponents
{
    public class Integer
    {
        private int _int;
        public Integer(int num)
        {
            _int = num;
        }
        public int GetInteger { get { return _int; } }
       
    }
}
