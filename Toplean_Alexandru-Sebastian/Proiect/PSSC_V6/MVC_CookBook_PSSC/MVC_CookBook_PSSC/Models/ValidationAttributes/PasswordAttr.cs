using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CookBook_PSSC.Models.ValidationAttributes
{
    public class PasswordAttr:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            String val = value as String;
            if (val.Length < 8 || !hasUpper(val) || !hasNumber(val) || !hasLower(val))
                return false;
            else return true;


        }
        private bool hasUpper(String val)
        {
            foreach(Char c in val)
            {
                if (c >= 'A' && c <= 'Z')
                    return true;
            }
            return false;
        }
        private bool hasNumber(String val)
        {
            foreach(Char c in val)
            {
                if (c >= '0' && c <= '9')
                    return true;
            }
            return false;
        }
        private bool hasLower(String val)
        {
            foreach (Char c in val)
            {
                if (c >= 'a' && c <= 'z')
                    return true;
            }
            return false;
        }
    }
}
