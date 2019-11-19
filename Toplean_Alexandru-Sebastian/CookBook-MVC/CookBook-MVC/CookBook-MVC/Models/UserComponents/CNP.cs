using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using CookBook_MVC.Models.CommonComponents;

namespace CookBook_MVC.Models.UserComponents
{
    public class CNP
    {
        private Text CNP_txt;
        public CNP(Text cnp) {
            Contract.Requires(cnp.GetText.Length == 10, "The Unique Identification Number must be exactly 10 characters long!");
            CNP_txt = cnp;
        }
        public Text GetCNP { get { return CNP_txt; } }
    }
}
