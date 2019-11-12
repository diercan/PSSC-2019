using Modele.Disciplina;
using Modele.Generic;
using Repositories.Disciplina;
using Servicii.Disciplina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoDdd
{
    class Program
    {
        static void Main(string[] args)
        {
            //creare disciplina
            var discilina = DisciplinaFactory.Instance.CreeazaDisciplina("PSSC");
            var repository = new DisciplinaRepository();
            repository.AdaugaDisciplina(discilina);
			
            //inscrie student - gresit se poate adauga studentul in doua discipline, 
            //crearea obiecului student ar trebui sa fie facuta intern in model (aici student este o entitate)
            discilina.InscrieStudent(DisciplinaFactory.Instance.CreeazaStudent("1234567", "test1"));
            //inscrierea aceluias student arunca exceptie
            //discilina.InscrieStudent(DisciplinaFactory.Instance.CreeazaStudnet("1234567", "test1"));

            discilina.IncepeSemestru();

            //nota activitate
            discilina.NoteazaActivitateStudent(new Modele.Generic.NumarMatricol("1234567"), new Modele.Generic.Nota(8));
            discilina.NoteazaActivitateStudent(new Modele.Generic.NumarMatricol("1234567"), new Modele.Generic.Nota(10));

            var noteExamen = new Dictionary<Modele.Generic.NumarMatricol, Modele.Generic.Nota>();
            noteExamen.Add(new NumarMatricol("1234567"), new Nota(7));
            discilina.TreceNoteExamen(noteExamen);

            discilina.IncheieSemestru();

            //publica notele
            var publish = new PublicareNoteService();
            publish.PublicareNotePeWebSite("PSSC");

            repository.ActualizeazaDisciplina(discilina);

            Console.WriteLine("Press any key to terminate.");
            Console.ReadLine();
        }
    }
}
