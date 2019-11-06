using GameRentWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRentWeb.Repositories
{
    public class DataBaseRepo<T> : IDataBaseRepo<T> where T : class
    {
    
        private  AppDBContext _context;
        private DbSet<T> table;
        
        public DataBaseRepo(AppDBContext context)
        {
            _context = context;
            table = _context.Set<T>();
            
        }

        public void Insert(T myObject) 
        {
            table.Add(myObject);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            T deletedObject = table.Find(id);
            if (deletedObject != null)
            {
                table.Remove(deletedObject);
                _context.SaveChanges();
            }
 
        }

        public IEnumerable<T> GetAllObjects()
        {
            return table.ToList();
        }

        public T GetObjectById(int id)
        {
            return table.Find(id);
        }

        public void Update(T objectChanges)
        {
            var modifiedObject = table.Attach(objectChanges);
            modifiedObject.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}

