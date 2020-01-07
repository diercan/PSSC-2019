using Farmacie.Models.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.MedicamentComponents
{
    public class PretCumparare
    {
        private RealNumber pretc;

        public PretCumparare(RealNumber pc)
        {

            pretc = pc;
        }
        public RealNumber getPretC { get { return pretc; } }
    }
}
