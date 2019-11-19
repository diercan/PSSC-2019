using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook_MVC.Models.CommonComponents;
using System.Diagnostics.Contracts;
namespace CookBook_MVC.Models.EmailComponents
{
    public class EmailAddress
    {
        private Text EmailAddr;
        public EmailAddress(Text EmailAddr)
        {
            Contract.Requires(EmailAddr.GetText.Contains('@'), "This is not a valid Email Address");
            this.EmailAddr = EmailAddr;
        }
        public Text GetEmailAddress { get { return EmailAddr; } }
    }
}
