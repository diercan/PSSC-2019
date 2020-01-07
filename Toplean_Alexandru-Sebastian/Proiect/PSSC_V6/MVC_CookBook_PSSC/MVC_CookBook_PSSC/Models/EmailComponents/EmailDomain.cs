using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_CookBook_PSSC.Models.CommonComponents;


namespace MVC_CookBook_PSSC.Models.EmailComponents
{
    public class EmailDomain
    {
        private Text EmDomain;
        public EmailDomain(Text Domain)
        {
            EmDomain = Domain;
        }
        public Text GetDomain { get { return EmDomain; } }
    }
}
