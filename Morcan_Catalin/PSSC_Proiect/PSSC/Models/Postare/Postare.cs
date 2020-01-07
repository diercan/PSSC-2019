using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using PSSC.Models.Generic;

namespace PSSC.Models.Postare

{
    public class Postare
    {
        public Guid id { get; internal set; }
        public DateTime DataPostarii { get; internal set; }
        public PlainText Titlu { get; internal set; }
        public PlainText Autor { get; internal set; }
        public PlainText Body { get; internal set; }
        public PlainText Topic { get; internal set; }
        public GradImportantaPostare NivelImportanta { get; internal set; }
        public TipPostare Tip { get; internal set; }

        

        private List<Comentariu> _comentarii;
        public ReadOnlyCollection<Comentariu> ComentariiPostare { get { return _comentarii.AsReadOnly(); } }

        public Postare() { }

        public Postare(PlainText titlu, PlainText autor, PlainText body, PlainText topic,
            GradImportantaPostare grad, TipPostare tip, DateTime d)
        {
            _comentarii = new List<Comentariu>();
            Titlu = titlu;
            Autor = autor;
            Body = body;
            Topic = topic;
            NivelImportanta = grad;
            Tip = tip;
            DataPostarii = new DateTime();
            DataPostarii = DateTime.Now;
            _comentarii = null;
            
        }
    }
}
