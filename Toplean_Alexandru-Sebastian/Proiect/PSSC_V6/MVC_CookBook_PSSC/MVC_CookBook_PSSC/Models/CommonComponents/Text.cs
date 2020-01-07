using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.ComponentModel.DataAnnotations;

namespace MVC_CookBook_PSSC.Models.CommonComponents
{
    public class Text
    {
        public Text()
        {

        }
        [Key]
        public string txt { get;  set; }
        public Text(string txt)
        {
            this.txt = txt;
            Contract.Requires(txt.Length < 256, "Text too long, maximum size allowed is 256 characters!");
        }
        public string GetText { get { return txt; } }

    }
}
