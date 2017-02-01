using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Accounting;
using NHibernate;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository.Accounting
{
    public interface IActiveBranchDao : IDao<ActiveBranch, int>
    {
        void Delete(ActiveBranch ab);
        void Update(string id, string monthEnd, string currentMonth, string currentMonthM1, string currentMonthM2, string fytd);
    }

    public class ActiveBranchDao : AbstractDao<ActiveBranch, int>, IActiveBranchDao
    {
        public ActiveBranchDao(ISession session) 
            : base(session)
        {
        }

        public void Delete(ActiveBranch ab)
        {
            m_session.Delete(ab);
        }

        public void Update(string id, string monthEnd, string currentMonth, string currentMonthM1, string currentMonthM2, string fytd)
        {
            string sql = "Update top (1) dbo.xGEM_ActiveBranch " +
                         "Set MonthEnd = '" + monthEnd + "', " +
                         "    CurrentMonth = " + currentMonth + ", " +
                         "    CurrentMonthM1 = " + currentMonthM1 + ", " +
                         "    CurrentMonthM2 = " + currentMonthM2 + ",  " +
                         "    FYTD = " + fytd + "  " +
                         "Where id = " + id;

            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                ExecuteNonQuery(cmd);
            }
        }

    }
}
