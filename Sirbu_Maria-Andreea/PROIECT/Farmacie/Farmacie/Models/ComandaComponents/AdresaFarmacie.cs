using Farmacie.Models.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.ComandaComponents
{
    public class AdresaFarmacie
    {
        private Text StradaText;
        private Number nrStradaNumber;
        public AdresaFarmacie(Text Strada, Number nrStrada)
        {

            StradaText = Strada;
            nrStradaNumber = nrStrada;
        }
        public Text getStrada { get { return StradaText; } }
        public Number getnrStrada { get { return nrStradaNumber; } }
    }
}
