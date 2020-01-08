using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrBaschet.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Add(TEntity obj);
        Task<TEntity> AddAsync(TEntity obj);
        TEntity GetById(string id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetPaginated(int skip, int take);
        void Update(TEntity obj);
        IQueryable<TEntity> GetQueryable();
        void Remove(string id);
        int SaveChanges();
    }
}