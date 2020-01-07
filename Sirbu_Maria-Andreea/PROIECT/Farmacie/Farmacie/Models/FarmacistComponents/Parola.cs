using Farmacie.Models.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.FarmacistComponents
{
    public class Parola
    {
        private Text parolaText;

        public Parola(Text parola)
        {

            parolaText = parola;
        }
        public Text getParola { get { return parolaText; } }
    }
}

