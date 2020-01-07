using Farmacie.Models.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.MedicamentComponents
{
    public class Locatie
    {
        private Text AdresaText;
        public Locatie(Text Adresa)
        {

            AdresaText = Adresa;

        }
        public Text getLocatie{ get { return AdresaText; } }
    }
}
