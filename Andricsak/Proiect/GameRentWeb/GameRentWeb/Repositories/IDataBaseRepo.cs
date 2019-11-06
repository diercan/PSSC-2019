using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRentWeb.Models;
// this interface will handle database actions
namespace GameRentWeb.Repositories
{
    
    public interface IDataBaseRepo<T>
    {
        T GetObject(int id);
        IEnumerable<T> GetAllObjects();
        T Add(T myObject);
        T Update(T objectChanges);
        T Delete(int id);
    }
}
