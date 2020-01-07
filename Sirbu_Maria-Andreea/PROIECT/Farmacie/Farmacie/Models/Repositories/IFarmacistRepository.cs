using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.Repositories
{
    interface IFarmacistRepository
    {
        Task Create(Farmacist farmacist);
        Task<List<Farmacist>> GetFarmacistiAsync();
    }
}
