using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook_MVC.Models.Exceptions
{
    public class ExistentRecipeException:SystemException
    {
        public ExistentRecipeException(string errMsg)
        {

        }
    }
}
