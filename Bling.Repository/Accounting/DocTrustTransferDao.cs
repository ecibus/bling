using System;
using System.Data;
using System.Data.SqlClient;
using log4net;
using NHibernate;

namespace Bling.Repository.Accounting
{
    public interface IDocTrustDao
    {
        void Transfer(string transferDate, string asOfDate);
    }

    public class DocTrustDao : AbstractDao<string, int>, IDocTrustDao
    {
        public DocTrustDao(ISession session) : base(session)
        {
            m_logger = LogManager.GetLogger(typeof(DocTrustDao));
        }

        public void Transfer(string transferDate, string asOfDate)
        {
            m_logger.DebugFormat("DocTrust Transfer Date : {0} ; As Of Date : {1}", transferDate, asOfDate);
            

            using (var cn = new SqlConnection(MWDataStoreConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "qappDOCTrustAcctLog";
                    cmd.Parameters.AddWithValue("@txtDate", transferDate);
                    cmd.Parameters.AddWithValue("@txtTo", asOfDate);

                    cmd.ExecuteNonQuery();
                }
            }

            m_logger.DebugFormat("DocTrust Transfer End");
        }
    }
}
