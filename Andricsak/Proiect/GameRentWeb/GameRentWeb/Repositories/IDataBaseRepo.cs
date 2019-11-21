using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRentWeb.Models;
// this interface will handle database actions
namespace GameRentWeb.Repositories
{
    
    public interface IDataBaseRepo<T> where T : class
    {
        Task<T> GetObjectById(int id);
        Task<IEnumerable<T>> GetAllObjects();
        Task Insert(T myObject);
        Task Update(T objectChanges);
        Task Delete(int id);
    }
}
