using System;
namespace Repositories.Disciplina
{
    interface IDisciplinaRepository
    {
        void ActualizeazaDisciplina(Modele.Disciplina.Disciplina disciplina);
        void AdaugaDisciplina(Modele.Disciplina.Disciplina disciplina);
        Modele.Disciplina.Disciplina GasesteDiscipilnaDupaNume(string nume);
    }
}
