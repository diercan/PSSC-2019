using Data;

namespace Datc.Data.Repository
{
    public interface IRepositoryFactory
    {
        void Dispose();
        IRepository<T> GetRepository<T>() where T : class;
        DatcDbContext GetContext();
    }
}