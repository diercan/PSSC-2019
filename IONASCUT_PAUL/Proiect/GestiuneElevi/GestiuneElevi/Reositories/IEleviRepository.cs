using GestiuneElevi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestiuneElevi.Reositories
{
    public interface IEleviRepository
    {
        Task AdaugaElevAsyncTask(ElevEntity elev);
        Task<List<ElevEntity>> GetAllEleviAsyncTask();
        Task<ElevEntity> GetElevAsyncTask(string cnp);
        Task AdaugaNotaAsyncTask(NotaEntity nota);
        Task<List<NotaEntity>> GetAllNoteAsyncTask();
    }
}
