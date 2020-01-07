using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CookBook_PSSC.Models.Exceptions
{
    public class ExistentRecipeException:SystemException
    {
        public ExistentRecipeException(string errMsg)
        {

        }
    }
}
