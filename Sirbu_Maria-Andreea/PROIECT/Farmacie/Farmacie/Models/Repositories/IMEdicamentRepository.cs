using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.Repositories
{
   public interface IMedicamentRepository
    {

        Task Create(Medicament medicament);
        Task<List<Medicament>> GetMedicamenteAsync();

    
    }
}
