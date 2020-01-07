using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Datc.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void TransactionalDelete(object id);
        void TransactionalDelete(TEntity entityToDelete);
        void TransactionalInsert(TEntity entity);
        void TransactionalUpdate(TEntity entityToUpdate);
        void Update(TEntity entityToUpdate);
    }
}