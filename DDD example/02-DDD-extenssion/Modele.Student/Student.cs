using Modele.Generic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Modele.Student
{
    public class Student
    {
        public NumarMatricol NumarMatricol { get; private set; }

        public PlainText Nume { get; private set; }

        private readonly List<NoteDisciplina> _note;

        public ReadOnlyCollection<NoteDisciplina> Note { get { return _note.AsReadOnly(); } }

        public Student(NumarMatricol nrMatricol, PlainText nume)
        {
            NumarMatricol = nrMatricol;
            Nume = nume;
            _note = new List<NoteDisciplina>();
        }

        public RezultatOperatieDomeniu AdaugareDisciplina(PlainText denumireDisciplina)
        {
            RezultatOperatieDomeniu rezultat;
            var existaDisciplina = _note.Any(d=>d.Denumire == denumireDisciplina);
            if (!existaDisciplina)
            {
                var noteDisciplina = new NoteDisciplina(denumireDisciplina);
                _note.Add(noteDisciplina);
                rezultat = RezultatOperatieDomeniu.RezultatSucces("Adaugarea a fost cu succes");
            }
            else
            {
                rezultat = RezultatOperatieDomeniu.RezultatEsuat("Adaugarea a esuat: disciplina duplicta.");
            }
            return rezultat;
        }

        public RezultatOperatieDomeniu AdaugareNotaActivitate(PlainText denumireDisciplina, Nota nota, DateTime data)
        {
            RezultatOperatieDomeniu rezultat;
            var noteDisciplina = _note.SingleOrDefault(d=>d.Denumire.Equals(denumireDisciplina));
            if(noteDisciplina!=null)
            {
                var notaDisciplina = new NotaDisciplina(nota, data);
                noteDisciplina.AdaugareNotaActivitate(notaDisciplina);
                rezultat = RezultatOperatieDomeniu.RezultatSucces("Nota a fost adaugata.");
            }
            else
            {
                rezultat = RezultatOperatieDomeniu.RezultatEsuat("Adaugarea a esuat: disciplina nu exista.");
            }
            return rezultat;
        }

        public RezultatOperatieDomeniu AdaugareNotaExamen(PlainText denumireDisciplina, Nota nota, DateTime data)
        {
            RezultatOperatieDomeniu rezultat;
            var noteDisciplina = _note.SingleOrDefault(d => d.Denumire == denumireDisciplina);
            if (noteDisciplina != null)
            {
                var notaDisciplina = new NotaDisciplina(nota, data);
                noteDisciplina.AdaugareNotaExamen(notaDisciplina);
                rezultat = RezultatOperatieDomeniu.RezultatSucces("Nota a fost adaugata.");
            }
            else
            {
                rezultat = RezultatOperatieDomeniu.RezultatEsuat("Adaugarea a esuat: disciplina nu exista.");
            }
            return rezultat;
        }
    }
}
