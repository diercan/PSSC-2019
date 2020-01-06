using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace Sistem_gestiune_vanzari.Repository
{
    //Class change by Interface, inheritance by the class. Then the Interface define method are must and should implemented into class.
    // This repository class is communicating the database by the dataset. This method responsible to all necessary operation is done with database
    //The DataSet contains the copy of the data we requested through the SQL statement
    public interface IRepository<Tabel_Entitate> where Tabel_Entitate:class
    {
        IEnumerable<Tabel_Entitate> GetValoare();
        IEnumerable<Tabel_Entitate> GetAllRecords();
        IQueryable<Tabel_Entitate> GetAllRecordsIQueryable();
        int GetAllRecordsCount();
        void Add(Tabel_Entitate entitate);
        void Update(Tabel_Entitate entitate);
        void UpdateByWhereClause(Expression<Func<Tabel_Entitate, bool>> wherePredict, Action<Tabel_Entitate> ForEachPredict);
        Tabel_Entitate GetFirstorDefault(int recordId);
        void Remove(Tabel_Entitate entitate);
        void RemoveByWhereClause(Expression<Func<Tabel_Entitate, bool>> wherePredict);
        object GetFirstorDefault(string id_produs);
        object GetFirstorDefaultCategorie(int id_categorie);
        void RemoveRangeByWhereClause(Expression<Func<Tabel_Entitate, bool>> wherePredict);
        void InactiveAndDeleteMarkByWhereClause(Expression<Func<Tabel_Entitate, bool>> wherePredict, Action<Tabel_Entitate> ForEachPredict);
        Tabel_Entitate GetFirstorDefaultByParameter(Expression<Func<Tabel_Entitate, bool>> wherePredict);
        IEnumerable<Tabel_Entitate> GetListParameter(Expression<Func<Tabel_Entitate, bool>> wherePredict);
        IEnumerable<Tabel_Entitate> GetResultBySqlProcedure(string query, params object[] parameters);
        IEnumerable<Tabel_Entitate> GetRecordsToShow(int NumberPage, int PageSize, int CurrentPage, Expression<Func<Tabel_Entitate, bool>> wherePredict, Expression<Func<Tabel_Entitate, int>> orderByPredict);
    }
}