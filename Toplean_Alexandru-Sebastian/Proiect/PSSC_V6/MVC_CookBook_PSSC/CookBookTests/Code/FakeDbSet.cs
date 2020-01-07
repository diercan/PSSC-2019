using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CookBookTests.Code
{
    public class FakeDbSet<T> : DbSet<T>, IDbSet<T> where T : class
    {
        List<T> _data;

        public FakeDbSet()
        {
            _data = new List<T>();
        }

        public override T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public override T Add(T item)
        {
            _data.Add(item);
            return item;
        }

        public override T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        public override T Attach(T item)
        {
            return null;
        }

        public T Detach(T item)
        {
            _data.Remove(item);
            return item;
        }

        public override T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public List<T> Local
        {
            get { return _data; }
        }

        public override IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _data.AddRange(entities);
            return _data;
        }

        public override IEnumerable<T> RemoveRange(IEnumerable<T> entities)
        {
            for (int i = entities.Count() - 1; i >= 0; i--)
            {
                T entity = entities.ElementAt(i);
                if (_data.Contains(entity))
                {
                    Remove(entity);
                }
            }

            return this;
        }

        Type IQueryable.ElementType
        {
            get { return _data.AsQueryable().ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return _data.AsQueryable().Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _data.AsQueryable().Provider; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}
