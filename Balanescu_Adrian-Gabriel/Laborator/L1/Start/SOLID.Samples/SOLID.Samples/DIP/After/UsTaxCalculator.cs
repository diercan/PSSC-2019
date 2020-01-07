using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLID.Samples.DIP.After
{
    public class UsTaxCalculator : ITaxCalculator
    {
        public decimal GetTax()
        {
            return 1m;
        }
    }
}
