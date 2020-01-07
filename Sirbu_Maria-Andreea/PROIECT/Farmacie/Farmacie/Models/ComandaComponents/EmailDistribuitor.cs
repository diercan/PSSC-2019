using Farmacie.Models.CommonComponents;
using Farmacie.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.ComandaComponents
{
    public class EmailDistribuitor
    {
        private Text emailText;
        public EmailDistribuitor(Text email)
        {

            emailText = email;
        }
        public Text getEmailDistribuitor { get { return emailText; } }
        public void checkIfThrow()
        {
            if (!this.emailText.getString.Contains('@') || !this.emailText.getString.Contains('.') || this.emailText.getString.LastIndexOf(".") < this.emailText.getString.LastIndexOf('@'))
            {
                throw new InvalidEmailException();
            }
        }
    }
}
