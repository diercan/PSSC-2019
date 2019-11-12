using Modele.Generic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Modele.Disciplina
{
    public class Disciplina
    {
        public PlainText Nume { get; internal set; }
        public Coeficient PondereExamen { get; internal set; }

        private List<Student> _studentiInscrisi;
        public ReadOnlyCollection<Student> StudentiInscrisi { get { return _studentiInscrisi.AsReadOnly(); } } 
        
        public StareDisciplina Stare { get; internal set; }
        public Laboratoare Laboratoare { get; internal set; }
        public Cursuri Cursuri { get; internal set; }
        
        internal Disciplina(PlainText nume, Coeficient pondereExamen)
        {
            //Contract.Requires(nume != null, "nume");
            //Contract.Requires(pondereExamen != null, "pondereExamen");

            Nume = nume;
            PondereExamen = pondereExamen;
            _studentiInscrisi = new List<Student>();
            Stare = StareDisciplina.Inscrieri;
            Laboratoare = new Laboratoare();
            Cursuri = new Cursuri();
        }

        internal Disciplina(PlainText nume, Coeficient pondereExamen, List<Student> studentiInscrisi)
            :this(nume, pondereExamen)
        {
            //Contract.Requires(studentiInscrisi != null, "lista de studenti inscrisi");
            _studentiInscrisi = studentiInscrisi;
        }

        #region operatii
        public void InscrieStudent(Student student)
        {
            //Contract.Requires(student != null, "student");
            //Contract.Requires(Stare == StareDisciplina.Inscrieri, "nu suntem in perioada in care se fac inscrieri");

            var gasit = _studentiInscrisi.FirstOrDefault(s => s.Equals(student));
            if (gasit == null)
            {
                var copieStudent = new Student(student.NumarMatricol, student.Nume);
                _studentiInscrisi.Add(copieStudent);
            }
            else
            {
                throw new StudentulExistaExceptions();
            }
        }

        public void IncepeSemestru()
        {
            //Contract.Requires(Stare == StareDisciplina.Inscrieri, "nu suntem in perioada in care se fac inscrieri");
            Stare = StareDisciplina.InDesfasurare;
        }

        public void IncheieSemestru()
        {
            //Contract.Requires(Stare == StareDisciplina.InDesfasurare, "semestrul nu a inceput");

            //calculeaza nota finala
            //BUG: metoda poate arunca exceptii daca studentii nu au toate notele, 
            //ar trebui refactorizat sa permita ca anumiti studenti sa nu poata fi incheiati
            foreach (var student in _studentiInscrisi)
            {
                student.CalculeazaNotaFinala(PondereExamen);
            }
            Stare = StareDisciplina.Incheiata;
        }

        public void NoteazaActivitateStudent(NumarMatricol nrMatricolStudent, Nota nota)
        {
            //Contract.Requires(nrMatricolStudent != null);
            //Contract.Requires(nota != null);
            //Contract.Requires(Stare == StareDisciplina.InDesfasurare, "semestrul nu a inceput");

            var student = _studentiInscrisi.First(s => s.NumarMatricol.Equals(nrMatricolStudent));
            student.NoteActivitate.AdaugaNota(nota);
        }

        public void TreceNoteExamen(Dictionary<NumarMatricol, Nota> rezultateExamen)
        {
            //Contract.Requires(rezultateExamen != null);
            //Contract.Requires(Stare == StareDisciplina.InDesfasurare, "semestrul nu a inceput");
            foreach (var pair in rezultateExamen)
            {
                var student = _studentiInscrisi.First(s => s.NumarMatricol.Equals(pair.Key));
                student.NotaExamen = pair.Value;
            }
        }

        public void IncarcaMaterialCurs(PlainText numeCurs, Uri continutCurs)
        {
            //Contract.Requires(numeCurs != null, "numeCurs");
            //Contract.Requires(continutCurs != null, "continutCurs");
            var curs = Cursuri.Valori.FirstOrDefault(c => c.Nume.Equals(numeCurs));
            if (curs == null)
            {
                //cursul trebuie creat
                curs = new Curs(numeCurs);
                curs.LinkContinut = continutCurs;
                Cursuri.AdaugaCurs(curs);
            }
            else
            {
                //cursul exista
                curs.ActualizareLinkContinut(continutCurs);
            }
        }

        public void IncarcaMaterialLaborator(PlainText numeLaborator, Uri continutLaborator)
        {
            //Contract.Requires(numeLaborator != null, "numeLaborator");
            //Contract.Requires(continutLaborator != null, "continutLaborator");
            var laborator= Laboratoare.Valori.FirstOrDefault(c => c.Nume.Equals(numeLaborator));
            if (laborator == null)
            {
                //Laboratorul trebuie creat
                laborator = new Laborator(numeLaborator);
                laborator.LinkContinut = continutLaborator;
                Laboratoare.AdaugaLaborator(laborator);
            }
            else
            {
                //Laboratorul exista
                laborator.ActualizareLinkContinut(continutLaborator);
            }
        }
        #endregion

        #region override object
        public override string ToString()
        {
            return Nume.ToString();
        }

        public override bool Equals(object obj)
        {
            var disciplina = (Disciplina)obj;

            if (disciplina != null)
            {
                return Nume.Equals(disciplina.Nume);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Nume.GetHashCode();
        }
        #endregion
    }
}
