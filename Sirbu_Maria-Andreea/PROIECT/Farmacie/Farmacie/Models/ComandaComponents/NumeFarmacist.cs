using Farmacie.Models.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.ComandaComponents
{
    public class NumeFarmacist
    {
        private Text numeFarmacist;

        public NumeFarmacist(Text nume)
        {

            numeFarmacist = nume;
        }
        public Text getNumeFarmacist { get { return numeFarmacist; } }
    }
}
