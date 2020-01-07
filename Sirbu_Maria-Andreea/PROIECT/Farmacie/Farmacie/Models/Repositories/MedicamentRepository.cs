using Farmacie.Models.Atributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.Repositories
{
    public class MedicamentRepository : IMedicamentRepository
    {
        private readonly MedicamentDbContext fcontext;
        public MedicamentRepository(MedicamentDbContext fDbContext)
        {
            fcontext = fDbContext;
        }

        
        public async Task Create(Medicament medicament)
        {
            fcontext.Add(medicament);
            await fcontext.SaveChangesAsync();

        }
        public async Task<List<Medicament>> GetMedicamenteAsync()
        {
            return await fcontext.medicamente.ToListAsync();
        }
    }
}
