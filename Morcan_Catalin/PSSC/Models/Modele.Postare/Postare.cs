using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PSSC.Models.Modele.Generic;
using PSSC.Models.Modele.Postare;
using System.Collections.ObjectModel;

namespace PSSC.Models.Modele.Postare
{
    public class Postare
    {
        public Titlu Titlu{ get; internal set; }
        public Autor Autor { get; internal set; }
        public Body Body { get; internal set; }
        public Topic Topic { get; internal set; }
        public GradImportantaPostare NivelImportanta { get; internal set; }
        public TipPostare Tip{ get; internal set; }
        private List<Comentariu> _comentarii;
        public ReadOnlyCollection<Comentariu> ComentariiPostare { get { return _comentarii.AsReadOnly(); } }

        public Postare(Titlu titlu, Autor autor, Body body,Topic topic, GradImportantaPostare grad,
            TipPostare tip)
        {
            _comentarii = new List<Comentariu>();
            Titlu = titlu;
            Autor = autor;
            Body = body;
            Topic = topic;
            NivelImportanta = grad;
            Tip = tip;
        }
    }


}

