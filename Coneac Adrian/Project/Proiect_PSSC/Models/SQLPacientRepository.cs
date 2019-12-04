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

        public IEnumerable<Pacient> GetAllPacient()
        {
            return context.Pacienti; 
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
    }
}
