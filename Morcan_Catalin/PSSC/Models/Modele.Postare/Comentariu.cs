using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PSSC.Models.Modele.Generic;

namespace PSSC.Models.Modele.Postare
{
    public class Comentariu
    {
        public PlainText Body { get; internal set; }
        public Autor _Autor;
    }
}