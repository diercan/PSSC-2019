using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.Repositories
{
   public interface IDistribuitorRepository
    {
        Task Create(Distribuitor distribuitor);
        Task<List<Distribuitor>> GetDistribuitoriAsync();
    }
}
