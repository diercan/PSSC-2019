using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace MVC_CookBook_PSSC.Models.CommonComponents
{
    public class Number
    {
        private float number;
        public Number(float number)
        {
            Contract.Requires(number > 0, "The quantity number must be positive");
            this.number = number;

        }
        public float GetNumber { get { return number; } } 
    }
}
