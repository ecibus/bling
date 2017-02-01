using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using log4net;
using System.Data.SqlClient;
using System.Data;
using Bling.Domain.Compliance;

namespace Bling.Repository.Compliance
{
    public interface IMERSReconciliationDao
    {
        IList<MERSReconciliation> GetData(string sql);
    }

    public class MERSReconciliationDao : AbstractDao<string, int>, IMERSReconciliationDao
    {
        public MERSReconciliationDao(ISession session)
            : base(session)
        {
            m_logger = LogManager.GetLogger(typeof(MERSReconciliationDao));
        }

        public IList<MERSReconciliation> GetData(string sql)
        {
            IList<MERSReconciliation> list = new List<MERSReconciliation>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new MERSReconciliation { LoanNumber = reader[0].ToString(), PurchasedDate = reader[1].ToString(), MersNo = reader[2].ToString() });
                    }
                    reader.Close();
                }
            }

            return list;
        }
    }
}
