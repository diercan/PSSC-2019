using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_CookBook_PSSC.Models.CommonComponents;
using System.Diagnostics.Contracts;
using System.ComponentModel.DataAnnotations;
using MVC_CookBook_PSSC.Models.Exceptions;

namespace MVC_CookBook_PSSC.Models.EmailComponents
{
    public class EmailAddress
    {
        private Text EmailAddr { get; set; }
        public EmailAddress()
        {

        }
        public EmailAddress(Text EmailAddr)
        {
            
            this.EmailAddr = EmailAddr;
            //checkIfThrow();
        }
        public void checkIfThrow()
        {
            if (!this.EmailAddr.GetText.Contains('@'))
            {
                throw new InvalidEmailException();
            }
            if (!this.EmailAddr.GetText.Contains('@') || !this.EmailAddr.GetText.Contains('.') || this.EmailAddr.GetText.LastIndexOf(".") < this.EmailAddr.GetText.LastIndexOf('@')) 
            {
                throw new InvalidEmailException();
            }
        }
        public Text Email_Address { get { return this.EmailAddr; } set { this.EmailAddr = Email_Address; } }
        public Text GetEmailAddress { get { return EmailAddr; } }
    }
}
