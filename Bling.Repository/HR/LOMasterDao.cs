using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.HR;
using NHibernate;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository.HR
{
    public interface ILOMasterDao : IDao<LOMaster, string>
    {
        IList<LOMaster> GetActiveLO();
    }

    public class LOMasterDao : AbstractDao<LOMaster, string>, ILOMasterDao
    {
        public LOMasterDao(ISession session)
            : base (session)
        {
        }

        public IList<LOMaster> GetActiveLO()
        {
            IList<LOMaster> list = new List<LOMaster>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_GetActiveLO";
                    cmd.CommandTimeout = 0;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        LOMaster c = new LOMaster
                        {
                            Code = reader["Username"].ToString(),
                            Name = reader["FullName"].ToString()
                        };
                        list.Add(c);
                    }
                    reader.Close();
                }
            }

            return list;

        }

    }
}
