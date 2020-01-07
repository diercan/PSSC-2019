using Data;
using System;

namespace Datc.Data.Repository
{
    public class RepositoryFactory : IDisposable, IRepositoryFactory
    {
        private DatcDbContext _dbContext;
        private bool disposedValue = false;

        public RepositoryFactory()
        {
            _dbContext = new DatcDbContext();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_dbContext);
        }

        public DatcDbContext GetContext()
        {
            return _dbContext;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue)
                return;

            if (disposing)
            {
                _dbContext.Dispose();
            }
            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
