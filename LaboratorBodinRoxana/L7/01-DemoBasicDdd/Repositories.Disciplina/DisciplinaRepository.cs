using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories.Disciplina
{
    public class DisciplinaRepository : Repositories.Disciplina.IDisciplinaRepository
    {
        private static List<Modele.Disciplina.Disciplina> _discipline = new List<Modele.Disciplina.Disciplina>();

        public DisciplinaRepository()
        {
        }

        public void AdaugaDisciplina(Modele.Disciplina.Disciplina disciplina)
        {
            var result = _discipline.FirstOrDefault(d => d.Equals(disciplina));

            if (result != null) throw new DuplicateWaitObjectException();

            _discipline.Add(disciplina);
            Console.WriteLine("O noua disciplina a fostr adaugata.");
        }

        public void ActualizeazaDisciplina(Modele.Disciplina.Disciplina disciplina)
        {
            //persit changes to the database
            Console.WriteLine("Modificarile au fost salvate.");
        }

        public Modele.Disciplina.Disciplina GasesteDiscipilnaDupaNume(string nume)
        {
            return _discipline.FirstOrDefault(d => d.Nume.Text == nume);
        }
    }
}
