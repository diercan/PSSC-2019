using Farmacie.Models.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.MedicamentComponents
{
    public class PretVanzare
    {
        private RealNumber pretv;

        public PretVanzare(RealNumber pv)
        {

            pretv = pv;
        }
        public RealNumber getPretV { get { return pretv; } }
    }
}
