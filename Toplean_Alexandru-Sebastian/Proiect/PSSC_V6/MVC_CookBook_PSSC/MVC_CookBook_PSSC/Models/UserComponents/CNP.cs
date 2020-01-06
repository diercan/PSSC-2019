using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using MVC_CookBook_PSSC.Models.CommonComponents;
using System.ComponentModel.DataAnnotations;

namespace MVC_CookBook_PSSC.Models.UserComponents
{
    public class CNP
    {
       
        private Text CNP_txt { get; set; }
        
        public CNP()
        {

        }
        public CNP(Text cnp) {
            Contract.Requires(cnp.GetText.Length == 10, "The Unique Identification Number must be exactly 10 characters long!");
            CNP_txt = cnp;
        }
       
        public Text GetCNP { get { return CNP_txt; } }
    }
}
