using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_PSSC.Models
{
    public class SQLPacientRepository : IPacientRepository
    {
        private readonly AppDbContext context;
        public SQLPacientRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Pacient Add(Pacient pacient)
        {
            context.Pacienti.Add(pacient);
            context.SaveChanges();
            return pacient;
        }

        public Pacient Delete(int id)
        {
            Pacient pacient = context.Pacienti.Find(id);
            if (pacient != null)
            {
                context.Pacienti.Remove(pacient);
                context.SaveChanges();
            }
            return pacient;
        }

        public List<Pacient> GetAllPacient()
        {
            return context.Pacienti.ToList(); 
        }

        public Pacient GetPacient(int Id)
        {
            return context.Pacienti.Find(Id);
        }

        

        public Pacient Update(Pacient pacientChanges)
        {
            var pacient = context.Pacienti.Attach(pacientChanges);
            pacient.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return pacientChanges;

        }

        public List<Pacient> SearchPacienti(string search)
        {
            int cnp;
            bool success = Int32.TryParse(search, out cnp);
            if (!success)
                cnp = 0;

            return context.Pacienti
                .Where(e => e.CNP.Contains(search) || e.Nume.Contains(search) || e.Prenume.Contains(search)).ToList();

        }

        public bool VerifyName(string nume)
        {
            return context.Pacienti.Any(e => e.Nume.Equals(nume));
        }

        
    }
}
