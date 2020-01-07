using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CookBook_PSSC.Models.ValidationAttributes
{
    public sealed class EmailAttr:ValidationAttribute
    {
        
        public override bool IsValid(object value)
        {
            var EmailAddr = value as String;

            if (EmailAddr.Contains('@') && EmailAddr.Contains('.') || EmailAddr.LastIndexOf(".") < EmailAddr.LastIndexOf('@'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
