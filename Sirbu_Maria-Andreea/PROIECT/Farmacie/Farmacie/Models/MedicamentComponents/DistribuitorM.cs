using Farmacie.Models.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.MedicamentComponents
{
    public class DistribuitorM
    {
        private Text distribuitorText;

        public DistribuitorM(Text dist)
        {

            distribuitorText = dist;
        }
        public Text getDistribuitor { get { return distribuitorText; } }
    }
}
