using Sistem_gestiune_vanzari.Database;
using Sistem_gestiune_vanzari.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sistem_gestiune_vanzari.Models
{
    public class InterfataUtilizatorModel
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        dbSistem_gestiune_vanzariEntities _DBEntitate = new dbSistem_gestiune_vanzariEntities();
        public IEnumerable<Tabel_Produs> lista_de_produse { get; set; }
        public InterfataUtilizatorModel CreateModel(string cautare)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@cautare",cautare??(object)DBNull.Value)

            };
            IEnumerable<Tabel_Produs> data = _DBEntitate.Database.SqlQuery<Tabel_Produs>("GetBySearch @cautare", parameter).ToList();
            return new InterfataUtilizatorModel()
            {
                lista_de_produse = data
                //lista_de_produse = _unitOfWork.GetRepositoryInstance<Tabel_Produs>().GetAllRecords()
            };
        }
    }
}
