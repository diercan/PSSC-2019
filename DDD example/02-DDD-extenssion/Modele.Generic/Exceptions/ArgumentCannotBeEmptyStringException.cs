using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele.Generic.Exceptions
{
    public class ArgumentCannotBeEmptyStringException:ArgumentException
    {
        public ArgumentCannotBeEmptyStringException(string parameterName)
            : base(string.Format("Argument {0} cannot be empty string.", parameterName), parameterName)
        {

        }
    }
}
