using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_PSSC.Models
{
    public interface IPacientRepository
    {
        Pacient GetPacient(int Id);
        List<Pacient> GetAllPacient();
        Pacient Add(Pacient pacient);
        Pacient Update(Pacient pacientChanges);
        Pacient Delete(int id);

       bool VerifyName(string nume);

        List<Pacient> SearchPacienti(string search);
        
    }
}
