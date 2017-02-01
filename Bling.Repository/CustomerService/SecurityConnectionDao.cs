using System;
using System.Data;
using System.Data.SqlClient;
using Bling.Domain.CustomerService;
using log4net;
using NHibernate;

namespace Bling.Repository.CustomerService
{
    public interface ISecurityConnectionDao
    {
        void UpdateShippedDateInDataTrac(SecurityConnectionShipDateInfo sc);
    }

    public class SecurityConnectionDao : AbstractDao<SecurityConnectionShipDateInfo, int>, ISecurityConnectionDao
    {
        public SecurityConnectionDao(ISession session) : base(session)
        {
            m_logger = LogManager.GetLogger(typeof(SecurityConnectionDao));
        }

        public void UpdateShippedDateInDataTrac(SecurityConnectionShipDateInfo sc)
        {
            if (sc.LoanNumber == "" || sc.DocumentType == "" || sc.ShippedDate == "")
                return;

            m_logger.DebugFormat("Updating {0} {1} {2}", sc.LoanNumber, sc.DocumentType, sc.ShippedDate);

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_UpdateShippedDate";
                    cmd.Parameters.AddWithValue("@loanNumber", sc.LoanNumber);
                    cmd.Parameters.AddWithValue("@documentType", sc.DocumentType.ToUpper());
                    cmd.Parameters.AddWithValue("@shippedDate", sc.ShippedDate);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
