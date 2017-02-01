using System;
using Bling.Domain.HR;
using NHibernate;
using System.Collections.Generic;
using NHibernate.Criterion;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository.HR
{
    public interface IInsuranceTitleDao
    {
        IList<InsuranceTitle> GetAllCorp();
        void UpdateTitle(string yearmonth, string column, string value);
        InsuranceTitle GetByYearMonth(string yearmonth);
    }

    public class InsuranceTitleDao : AbstractDao<InsuranceTitle, string>, IInsuranceTitleDao
    {
        public InsuranceTitleDao(ISession session)
            : base(session)
        {            
        }

        public IList<InsuranceTitle> GetAllCorp()
        {
            return m_session.CreateCriteria(typeof(InsuranceTitle))
                .Add(Expression.Eq("Location", "C"))
                .AddOrder(Order.Desc("YearMonth"))
                .List<InsuranceTitle>();            
        }

        public InsuranceTitle GetByYearMonth(string yearmonth)
        {
            return m_session.CreateCriteria(typeof(InsuranceTitle))
                .Add(Expression.Eq("Location", "C"))
                .Add(Expression.Eq("YearMonth", yearmonth))
                .UniqueResult<InsuranceTitle>();
        }

        public void UpdateTitle(string yearmonth, string column, string value)
        {            
            string sql = String.Format(
                "Update hr_insid " +
                "Set {0} = '{1}' " +
                "Where " +
                "   HR_InsId = '{2}' " +
                "   and HR_InsLocation = 'C'",
                column, value, yearmonth
                );

            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                ExecuteNonQueryForMWDataStore(cmd);
            }
        }
    }
}
