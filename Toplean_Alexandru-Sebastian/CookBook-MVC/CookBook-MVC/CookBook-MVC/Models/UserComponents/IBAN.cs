using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook_MVC.Models.CommonComponents;
using System.Diagnostics.Contracts;

namespace CookBook_MVC.Models.UserComponents
{
    public class IBAN
    {
        private Text iban_nr;
        private List<String> Countries = new List<string> {"RO","GB","US","HU","DE" };
        public IBAN(Text number)
        {
            //Contract.Requires()
            iban_nr = number;
            
        }
        public Text GetIBAN { get { return iban_nr; } }
        
    }
}
