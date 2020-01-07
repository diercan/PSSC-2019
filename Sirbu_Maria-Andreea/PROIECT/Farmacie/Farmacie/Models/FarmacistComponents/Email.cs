using Farmacie.Models.CommonComponents;
using Farmacie.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.FarmacistComponents
{
    public class Email
    {
        private Text emailText;

        public Email(Text email)
        {

            emailText = email;
        }
        public Text getEmail { get { return emailText; } }
        public void checkIfThrow()
        {
            if (!this.emailText.getString.Contains('@') || !this.emailText.getString.Contains('.') || this.emailText.getString.LastIndexOf(".") < this.emailText.getString.LastIndexOf('@'))
            {
                throw new InvalidEmailException();
            }
        }
    }
}
