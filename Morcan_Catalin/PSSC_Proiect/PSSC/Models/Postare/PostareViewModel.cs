using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PSSC.Models.Postare
{
    public class PostareViewModel
    {
        public Guid id { get; set; }
        public DateTime DataPostarii { get; set; }
        public string Titlu { get; set; }
        public string Autor { get; set; }
        public string Body { get; set; }
        public string Topic { get; set; }
        public GradImportantaPostare NivelImportanta { get; set; }
        public TipPostare Tip { get; set; }


        //private List<Comentariu> _comentarii;
        //public ReadOnlyCollection<Comentariu> ComentariiPostare { get { return _comentarii.AsReadOnly(); } }

        public PostareViewModel()
        {

        }
        public PostareViewModel(string t, string top, TipPostare tip, GradImportantaPostare grad, string b)
        {
            Titlu = t;
            Topic = top;
            Tip = tip;
            NivelImportanta = grad;
            Body = b;
        }
    }
}
