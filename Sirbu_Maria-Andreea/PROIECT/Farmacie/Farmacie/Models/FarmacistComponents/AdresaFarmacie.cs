using Farmacie.Models.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.FarmacistComponents
{
    public class AdresaFarmacie
    {
        private Text AdresaText;
        public AdresaFarmacie(Text Adresa)
        {
            
            AdresaText = Adresa;
           
        }
        public Text getAdresa { get { return AdresaText; } }
      
    }
}
