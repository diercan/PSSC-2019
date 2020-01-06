using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CookBook_PSSC.Models.ValidationAttributes
{
    public class CnpAttr:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            String val = value as String;
            try
            {
                if (val.Length != 0)
                {
                    if (!val.StartsWith('*') && !checkIfValid(val))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else return true;
            }
            catch
            {
                return true;
            }

        }

        public bool checkIfValid(String val)
        {
            if (val.Length == 13)
            {
                foreach (Char c in val)
                {
                    if (c >= '0' && c <= '9') { }
                    else return false;
                }
                return true;
            }
            else return false;
        }
    }
}
