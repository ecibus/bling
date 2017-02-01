using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Bling.Repository
{
    public interface IDao<T, IdT>
    {
        T GetById(IdT id);
        T Save(T entity);             
        IList<T> GetAll();
        DataTable GetDataTable(SqlCommand cmd);
        void ExecuteNonQuery(SqlCommand cmd);
    }
}
