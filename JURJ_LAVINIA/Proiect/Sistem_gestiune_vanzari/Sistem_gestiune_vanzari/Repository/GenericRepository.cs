using Sistem_gestiune_vanzari.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Sistem_gestiune_vanzari.Repository
{
    //This repository file is passing the value into Repository file and receives the result.
    public class GenericRepository<Tabel_Entitate> : IRepository<Tabel_Entitate> where Tabel_Entitate : class
    {
        DbSet<Tabel_Entitate> _dbSet;
        private dbSistem_gestiune_vanzariEntities _DBEntitate;

        public GenericRepository(dbSistem_gestiune_vanzariEntities DBEntitate)
        {
            _DBEntitate = DBEntitate;
            _dbSet = _DBEntitate.Set<Tabel_Entitate>();
        }
        public void Add(Tabel_Entitate entitate)
        {
            _dbSet.Add(entitate);
            _DBEntitate.SaveChanges();
        }
        public IEnumerable<Tabel_Entitate> GetValoare()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<Tabel_Entitate> GetAllRecords()
        {
            return _dbSet.ToList();
        }

        public int GetAllRecordsCount()
        {
            return _dbSet.Count();
        }

        public IQueryable<Tabel_Entitate> GetAllRecordsIQueryable()
        {
            return _dbSet;
        }
        //Returns the first element of a sequence, or a default value if no element is found
        public Tabel_Entitate GetFirstorDefault(int recordId)
        {
            return _dbSet.Find(recordId);
        }

        public object GetFirstorDefault(string id_produs)
        {
            return _dbSet.Find(id_produs);
        }
        public object GetFirstorDefaultCategorie(int id_categorie)
        {
            return _dbSet.Find(id_categorie);
        }

        public Tabel_Entitate GetFirstorDefaultByParameter(Expression<Func<Tabel_Entitate, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).FirstOrDefault();
        }

        public IEnumerable<Tabel_Entitate> GetListParameter(Expression<Func<Tabel_Entitate, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).ToList();
        }

        public IEnumerable<Tabel_Entitate> GetRecordsToShow(int NumberPage, int PageSize, int CurrentPage, Expression<Func<Tabel_Entitate, bool>> wherePredict, Expression<Func<Tabel_Entitate, int>> orderByPredict)
        {
            if (wherePredict != null)
            {
                return _dbSet.OrderBy(orderByPredict).Where(wherePredict).ToList();

            }

            else
            {
                return _dbSet.OrderBy(orderByPredict).ToList();
            }
        }

        public IEnumerable<Tabel_Entitate> GetResultBySqlProcedure(string query, params object[] parameters)
        {
            if (parameters != null)
            {
                return _DBEntitate.Database.SqlQuery<Tabel_Entitate>(query, parameters).ToList();
            }
            else
                return _DBEntitate.Database.SqlQuery<Tabel_Entitate>(query).ToList();
              
        }

        public void InactiveAndDeleteMarkByWhereClause(Expression<Func<Tabel_Entitate, bool>> wherePredict, Action<Tabel_Entitate> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public void Remove(Tabel_Entitate entitate)
        {
            if (_DBEntitate.Entry(entitate).State == EntityState.Detached)
                _dbSet.Attach(entitate);
            _dbSet.Remove(entitate);
        }

        public void RemoveByWhereClause(Expression<Func<Tabel_Entitate, bool>> wherePredict)
        {
            Tabel_Entitate entitate = _dbSet.Where(wherePredict).FirstOrDefault();
            Remove(entitate);
        }

        public void RemoveRangeByWhereClause(Expression<Func<Tabel_Entitate, bool>> wherePredict)
        {
            List<Tabel_Entitate> entitate = _dbSet.Where(wherePredict).ToList();
            foreach(var e in entitate)
            {
                Remove(e);
            }
        }

        public void Update(Tabel_Entitate entitate)
        {
            _dbSet.Attach(entitate);
            _DBEntitate.Entry(entitate).State = EntityState.Modified;
            _DBEntitate.SaveChanges();

        }

        public void UpdateByWhereClause(Expression<Func<Tabel_Entitate, bool>> wherePredict, Action<Tabel_Entitate> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        
    }
}