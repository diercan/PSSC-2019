using Modele.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele.Student
{
    public class NoteDisciplina
    {
        public PlainText Denumire { get; private set; }
        private List<NotaDisciplina> _noteActivitate;
        private List<NotaDisciplina> _noteExamen;

        public NoteDisciplina(PlainText denumire)
        {
            Denumire = denumire;
            _noteActivitate = new List<NotaDisciplina>();
            _noteExamen = new List<NotaDisciplina>();
        }

        internal void AdaugareNotaActivitate(NotaDisciplina nota)
        {
            _noteActivitate.Add(nota);
        }

        internal void AdaugareNotaExamen(NotaDisciplina nota)
        {
            _noteExamen.Add(nota);
        }
    }
}
