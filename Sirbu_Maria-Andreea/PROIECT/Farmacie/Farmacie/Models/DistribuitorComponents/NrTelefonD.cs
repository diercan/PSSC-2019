using Farmacie.Models.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.DistribuitorComponents
{
    public class NrTelefonD
    {
        private Text nrTelefonText;

        public NrTelefonD(Text nrTel)
        {

            nrTelefonText = nrTel;
        }
        public Text getNrTelefonD { get { return nrTelefonText; } }
    }
}
