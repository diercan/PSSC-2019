using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.Repositories
{
    public interface IComandaRepository
    {
        Task Create(Comanda comanda);
        Task<List<Comanda>> GetComenziAsync();
    }
}
