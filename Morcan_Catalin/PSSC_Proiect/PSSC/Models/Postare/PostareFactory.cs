using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSSC.Models.Generic;

namespace PSSC.Models.Postare
{
    public class PostareFactory
    {
        public PostareFactory()
        {

        }

        public Postare CreeazaPostare(string titlu, string autor, string body, string topic, GradImportantaPostare grad,
            TipPostare tip, DateTime d)
        {
            if ((int)tip > 2 || (int)grad > 2)
            {
                throw new GradSauTipIntroduseGresitException();
            }
            else
            {
                var postare = new Postare(new PlainText(titlu), new PlainText(autor),
                    new PlainText(body), new PlainText(topic), grad,
                    tip, d);
                return postare;
            }
        }
    }
}
