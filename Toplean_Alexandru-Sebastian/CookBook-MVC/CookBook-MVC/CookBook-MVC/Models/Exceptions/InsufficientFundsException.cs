using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook_MVC.Models.Exceptions
{
    public class InsufficientFundsException:SystemException
    {
        public InsufficientFundsException(String errMsg)
        {

        }
    }
}
