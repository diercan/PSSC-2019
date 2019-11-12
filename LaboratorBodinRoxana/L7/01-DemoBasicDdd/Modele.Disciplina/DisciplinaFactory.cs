using Modele.Generic;
using Modele.Generic.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Modele.Disciplina
{
    public class DisciplinaFactory
    {
        public static readonly DisciplinaFactory Instance = new DisciplinaFactory();

        private DisciplinaFactory()
        {

        }

        public Disciplina CreeazaDisciplina(string nume)
        {
            //Contract.Requires<ArgumentNullException>(nume != null, "text");
            //Contract.Requires<ArgumentInvalidLengthException>(
                    //nume.Length >= 2 && nume.Length <= 50, 
                    //"Numele disciplinei trebuie sa contina intre 2 si 50 de caractere.");
            
            var disciplina = new Disciplina(
                                        new PlainText(nume), 
                                        new Coeficient(1,2));
            
            return disciplina;
        }

        //gresit nu ar trebui sa poata fi obtinuta o entitate in afara root-ului de agregare
        //aceasta metoda ar trebui sa fie INTERNAL alt fel se poate ca un student sa fie asociat cu
        //doua discipline ceea ce nu este suportat de entitatea student
        public Student CreeazaStudent(string numarMatricol, string numeStudent)
        {
            return new Student(
                                new NumarMatricol(numarMatricol), 
                                new PlainText(numeStudent));
        }
    }
}
