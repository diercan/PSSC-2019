using Modele.Generic;

namespace Modele.Generic
{
    public class RezultatOperatieDomeniu
    {
        public bool Succes { get; private set; }

        public PlainText Mesaj { get; private set; }

        private RezultatOperatieDomeniu(PlainText mesaj, bool succes)
        {
            Mesaj = mesaj;
            Succes = succes;
        }

        public static RezultatOperatieDomeniu RezultatSucces(string mesaj)
        {
            return new RezultatOperatieDomeniu(new PlainText(mesaj), true);
        }

        public static RezultatOperatieDomeniu RezultatEsuat(string mesaj)
        {
            return new RezultatOperatieDomeniu(new PlainText(mesaj), false);
        }
    }
}