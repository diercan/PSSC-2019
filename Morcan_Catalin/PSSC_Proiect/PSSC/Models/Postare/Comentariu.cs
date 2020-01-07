using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSSC.Models.Generic;

namespace PSSC.Models.Postare
{
    public class Comentariu
    {
        public PlainText Body { get; internal set; }
        public PlainText _Autor { get; internal set; }
        public DateTime DataComentariu { get; internal set; }

        public Comentariu(PlainText b, PlainText a, DateTime d)
        {
            Body = b;
            _Autor = a;
            DataComentariu = d;
        }
    }
}
