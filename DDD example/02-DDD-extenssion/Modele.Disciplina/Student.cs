using Modele.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Modele.Disciplina
{
    public class Student
    {
        public NumarMatricol NumarMatricol { get; internal set; }
        public PlainText Nume { get; internal set; }
        public Note NoteActivitate { get; internal set; }
        public Nota NotaExamen { get; internal set; }
        public DateTime DataNotaExamen { get; internal set; }
        public bool NotaExamenPublicata { get; internal set; }
        public Nota NotaFinala {get; internal set;}
        

        internal Student(NumarMatricol nrMatricol, PlainText nume)
        {
            ////Contract.Requires(nrMatricol != null, "numar matricol");
            ////Contract.Requires(nume != null, "nume");
            NumarMatricol = nrMatricol;
            Nume = nume;
            NoteActivitate = new Note();
        }

        #region operations
        internal void CalculeazaNotaFinala(Coeficient coeficient)
        {
            ////Contract.Requires(coeficient != null, "coeficient");
            ////Contract.Requires(NotaExamen != null, "noata examen");
            ////Contract.Requires(NoteActivitate.Media.Valoare>=5, "noata activitate");
            ////Contract.Requires(NotaExamen.Valoare>= 5, "noata examen");

            var valCoeficient = coeficient.Fractie;
            NotaFinala = new Nota(Math.Round(valCoeficient * NotaExamen.Valoare + (1 - valCoeficient) * NoteActivitate.Media.Valoare));

        }
        #endregion

        #region object override
        public override string ToString()
        {
            return string.Format("{0}:{1}", NumarMatricol, Nume);
        }

        public override bool Equals(object obj)
        {
            var student = (Student)obj;

            if (student != null)
            {
                return NumarMatricol.Equals(student.NumarMatricol);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return NumarMatricol.GetHashCode();
        }
        #endregion
    }
}
