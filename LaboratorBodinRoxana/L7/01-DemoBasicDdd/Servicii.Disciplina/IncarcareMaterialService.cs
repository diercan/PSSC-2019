using Modele.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSitePublisher;

namespace Servicii.Disciplina
{
    public class IncarcareMaterialService
    {

        public void IncarcareMaterialLaborator(string numeDisciplina, string numeLaborator, string locatieContinut)
        {
            //apeleaza infrastructura pentru upload
            var publisher = new OneDrivePublisher();
            var uri = publisher.PublishToOneDrive(locatieContinut);

            //actualizeaza modelul
            var repository = new Repositories.Disciplina.DisciplinaRepository();
            var disciplina = repository.GasesteDiscipilnaDupaNume(numeDisciplina);
            disciplina.IncarcaMaterialLaborator(new PlainText(numeLaborator), uri);

        }

        public void IncarcareMaterialCurs(string numeDisciplina, string numeCurs, string locatieContinut)
        {
            //apeleaza infrastructura pentru upload
            var publisher = new OneDrivePublisher();
            var uri = publisher.PublishToOneDrive(locatieContinut);

            //actualizeaza modelul
            var repository = new Repositories.Disciplina.DisciplinaRepository();
            var disciplina = repository.GasesteDiscipilnaDupaNume(numeDisciplina);
            disciplina.IncarcaMaterialCurs(new PlainText(numeCurs), uri);
        }

    }
}
