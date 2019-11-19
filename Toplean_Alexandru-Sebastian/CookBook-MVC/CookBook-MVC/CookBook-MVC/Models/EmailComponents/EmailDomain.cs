using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook_MVC.Models.CommonComponents;

namespace CookBook_MVC.Models.EmailComponents
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
