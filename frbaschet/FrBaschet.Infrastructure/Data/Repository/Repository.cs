using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrBaschet.Domain.Interfaces;
using FrBaschet.Domain.Models;
using FrBaschet.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FrBaschet.Infrastructure.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityModel
    {
        protected readonly FrBaschetContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(FrBaschetContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity obj)
        {
            obj.Id = Guid.NewGuid();
            // obj.Created = DateTime.Now;
            // obj.Deleted = false;
            DbSet.Add(obj);
            return obj;
        }

        public async Task<TEntity> AddAsync(TEntity obj)
        {
            return await Task.Run(() => Add(obj));
        }

        public virtual TEntity GetById(string id)
        {
            return DbSet.Find(Guid.Parse(id));
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetPaginated(int skip, int take)
        {
            var entities = await DbSet.ToListAsync();
            return entities.Skip(skip).Take(take);
        }

        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return DbSet.AsQueryable();
        }

        public virtual void Remove(string id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}