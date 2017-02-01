using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Bling.Domain.LOS;
using NHibernate;

namespace Bling.Repository.LOS
{
    public interface IHMDADao
    {
        List<HMDA> GetAllData(string year, bool includeCurrentMonth);
    }

    public class HMDADao : AbstractDao<HMDA, int>, IHMDADao
    {
        public HMDADao(ISession session) : base(session)
        {            
        }

        public List<HMDA> GetAllData(string year, bool includeCurrentMonth)
        {
            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_GenerateHMDAData";
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.Parameters.AddWithValue("@include_current_month", includeCurrentMonth);
                    cmd.ExecuteNonQuery();
                }
            }
            return GetAll().OrderBy(x => x.ActionDate).ToList();
        }
    }
}
