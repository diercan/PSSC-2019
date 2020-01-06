using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_CookBook_PSSC.Models.CommonComponents;
using System.Diagnostics.Contracts;
using System.ComponentModel.DataAnnotations;

namespace MVC_CookBook_PSSC.Models.UserComponents
{
    public class IBAN
    {
        
        private Text iban_nr { get; set; }
        private List<String> Countries = new List<string> {"RO","GB","US","HU","DE" };
        public IBAN()
        {

        }
        public IBAN(Text number)
        {
            //Contract.Requires()
            iban_nr = number;
            
        }
        public Text GetIBAN { get { return iban_nr; } }
        
    }
}
