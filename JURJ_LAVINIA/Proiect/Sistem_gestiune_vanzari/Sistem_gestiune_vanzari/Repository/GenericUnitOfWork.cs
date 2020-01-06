using Sistem_gestiune_vanzari.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistem_gestiune_vanzari.Repository
{
    //SaveChanges() this method is responsible to DBEntity save and Dispose() method is responsible  to DBEntity Dispose
    public class GenericUnitOfWork:IDisposable
    {
        private dbSistem_gestiune_vanzariEntities DBEntitate = new dbSistem_gestiune_vanzariEntities();
        public IRepository<Tabel_EntitateTip> GetRepositoryInstance<Tabel_EntitateTip>() where Tabel_EntitateTip : class
        {
            return new GenericRepository<Tabel_EntitateTip>(DBEntitate);
        }
        public void SaveChanges()
        {
            DBEntitate.SaveChanges();
        }

       protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DBEntitate.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

    }
}