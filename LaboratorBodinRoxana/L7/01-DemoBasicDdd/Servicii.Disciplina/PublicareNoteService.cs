using Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSitePublisher;

namespace Servicii.Disciplina
{
    public class PublicareNoteService
    {
        public void PublicareNoteInCatalog(string numeFacultate, string numeDisciplina)
        {
            //gaseste model facultate
            //gaseste model disciplina
            //pentru fiecare student care este la disciplina actualizeaza catalogul de notedin cadrul facultatii

            //salveaza modificarile facute in catalog
        }

        public Uri PublicareNotePeWebSite(string numeDisciplina)
        {
            var repository = new Repositories.Disciplina.DisciplinaRepository();
            var disciplina = repository.GasesteDiscipilnaDupaNume(numeDisciplina);

            //genereaza continut raport
            var continutRaport = new List<List<string>>();
            foreach (var student in disciplina.StudentiInscrisi)
            {
                var line = new List<string>();
                line.Add(student.ToString());
                if (student.NotaFinala != null)
                {
                    line.Add(student.NotaFinala.ToString());
                }
                else
                {
                    line.Add("");
                }

                if (student.NotaExamen != null)
                {
                    line.Add(student.NotaExamen.ToString());
                }
                else
                {
                    line.Add("");
                }

                continutRaport.Add(line);
            }

            //genereaza PDF
            var pdf = new GeneratorRapoartePdf();
            var locatie = pdf.GenerareRaportTabelar(continutRaport);

            //[publica pe OneDrive
            var publisher = new OneDrivePublisher();
            return publisher.PublishToOneDrive(locatie);
        }
    }
}
