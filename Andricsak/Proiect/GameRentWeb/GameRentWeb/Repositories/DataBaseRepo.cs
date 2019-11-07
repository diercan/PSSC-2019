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

        public async Task Insert(T myObject) 
        {
            await table.AddAsync(myObject);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            T deletedObject = await table.FindAsync(id);
            if (deletedObject == null)
            {
                return;
            }
            table.Remove(deletedObject);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllObjects()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetObjectById(int id)
        {
            return await table.FindAsync(id);
        }

        public Task Update(T objectChanges)
        {
            var modifiedObject = table.Attach(objectChanges);
            modifiedObject.State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
}

