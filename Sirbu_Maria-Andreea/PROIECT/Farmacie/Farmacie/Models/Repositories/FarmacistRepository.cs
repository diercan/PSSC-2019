using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.Repositories
{
    public class FarmacistRepository : IFarmacistRepository
    {
        private readonly FarmacistDbContext fcontext;
        public FarmacistRepository(FarmacistDbContext fDbContext)
        {
            fcontext = fDbContext;
        }
        public async Task Create(Farmacist farmacist)
        {
            fcontext.Add(farmacist);
            await fcontext.SaveChangesAsync();

        }
        public async Task<List<Farmacist>> GetFarmacistiAsync()
        {
            return await fcontext.farmacisti.ToListAsync();
        }
    }
}
