using Farmacie.Models.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.ComandaComponents
{
    public class NumeMedicament
    {
        private Text numeMedText;

        public NumeMedicament(Text nume)
        {

            numeMedText = nume;
        }
        public Text getNumeMedicament { get { return numeMedText; } }
    }
}
