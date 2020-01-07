using Farmacie.Models.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.ComandaComponents
{
    public class PrenumeFarmacist
    {
        private Text prenumeFarmacist;

        public PrenumeFarmacist(Text nume)
        {

            prenumeFarmacist = nume;
        }
        public Text getPrenumeFarmacist { get { return prenumeFarmacist; } }
    }
}
