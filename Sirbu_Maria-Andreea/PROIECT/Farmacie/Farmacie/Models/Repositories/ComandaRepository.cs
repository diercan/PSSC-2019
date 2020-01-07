using Farmacie.Models.Atributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.Repositories
{
    public class ComandaRepository:IComandaRepository
    {
        private readonly ComandaDbContext dcontext;
        public ComandaRepository(ComandaDbContext dDbContext)
        {
            dcontext = dDbContext;
        }
        public async Task Create(Comanda comanda)
        {
            dcontext.Add(comanda);
            await dcontext.SaveChangesAsync();

        }
        public async Task<List<Comanda>> GetComenziAsync()
        {
            return await dcontext.comenzi.ToListAsync();
        }
    }
}
