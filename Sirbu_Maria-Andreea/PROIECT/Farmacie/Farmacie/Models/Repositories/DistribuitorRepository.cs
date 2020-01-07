using Farmacie.Models.Atributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.Repositories
{
    public class DistribuitorRepository:IDistribuitorRepository
    {
        private readonly DistribuitorDbContext dcontext;
        public DistribuitorRepository(DistribuitorDbContext dDbContext)
        {
            dcontext = dDbContext;
        }
        public async Task Create(Distribuitor distribuitor)
        {
            dcontext.Add(distribuitor);
            await dcontext.SaveChangesAsync();

        }
        public async Task<List<Distribuitor>> GetDistribuitoriAsync()
        {
            return await dcontext.distribuitori.ToListAsync();
        }
    }
}
