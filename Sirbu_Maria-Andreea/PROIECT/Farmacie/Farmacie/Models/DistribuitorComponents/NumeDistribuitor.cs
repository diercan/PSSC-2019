using Farmacie.Models.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.DistribuitorComponents
{
    public class NumeDistribuitor
    {
        private Text numeDistText;

        public NumeDistribuitor(Text nume)
        {

            numeDistText = nume;
        }
        public Text getNumeDistribuitor{ get { return numeDistText; } }
    }
}
