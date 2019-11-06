using GameRentWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRentWeb.Repositories
{
    public class DataBaseRepo<T> : IDataBaseRepo<T>
    {
    
        private readonly AppDBContext _context;
        
        public DataBaseRepo(AppDBContext context)
        {
            _context = context;
            
        }
        public T Add(T myObject) 
        {
            _context.Add<T>(myObject);
            _context.SaveChanges();
            return myObject;
        }

        public T Delete(int id)
        {
            T deletedObject = _context.Find(id);
            if (deletedObject != null)
            {
                _context.Users.Remove(deletedObject);
                _context.SaveChanges();
            }
            return deletedObject;
        }

        public IEnumerable<T> GetAllObjects()
        {
            return _context.Users;
        }

        public T GetObject(int id)
        {
            return _context.Users.Find(id);
        }

        public T Update(T objectChanges)
        {
            var modifiedUser = _context.Users.Attach(objectChanges);
            modifiedUser.State = EntityState.Modified;
            _context.SaveChanges();
            return objectChanges;
        }
    }
}

