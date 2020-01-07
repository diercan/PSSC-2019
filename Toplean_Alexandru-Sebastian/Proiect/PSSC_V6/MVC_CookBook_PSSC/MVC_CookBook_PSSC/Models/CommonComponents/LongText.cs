using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CookBook_PSSC.Models.CommonComponents
{
    public class LongText
    {
        private String _txt;
        public LongText(String txt)
        {
            _txt = txt;
        }
        public String GetText { get { return _txt; } }
    }
}
