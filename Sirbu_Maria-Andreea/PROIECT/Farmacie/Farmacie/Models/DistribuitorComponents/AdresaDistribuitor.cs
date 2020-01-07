using Farmacie.Models.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.DistribuitorComponents
{
    public class AdresaDistribuitor
    {
        private Text AdresaText;
        public AdresaDistribuitor(Text Adresa)
        {

            AdresaText = Adresa;

        }
        public Text getAdresa { get { return AdresaText; } }
    }
}
