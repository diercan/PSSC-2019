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
        T GetObjectById(int id);
        IEnumerable<T> GetAllObjects();
        void Insert(T myObject);
        void Update(T objectChanges);
        void Delete(int id);
    }
}
