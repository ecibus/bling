using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Funding;
using NHibernate;

namespace Bling.Repository.Funding
{
    public interface IFundingExceptionSummaryDao : IDao<FundingExceptionSummary, int>
    {
        IList<FundingExceptionSummary> GetList(int month, int year);
        void SaveComment(int month, int year, string brokerId, string comment);
    }

    public class FundingExceptionSummaryDao : AbstractDao<FundingExceptionSummary, int>, IFundingExceptionSummaryDao
    {
        public FundingExceptionSummaryDao(ISession session)
            : base(session)
        {
        }

        public IList<FundingExceptionSummary> GetList(int month, int year)
        {
            return m_session.CreateSQLQuery("exec dbo.xGEM_FundingExceptionSummary_GetByMonthAndYear :month, :year")
                .AddEntity(typeof(FundingExceptionSummary))
                .SetInt32("month", month)
                .SetInt32("year", year)
                .List<FundingExceptionSummary>();
        }

        public void SaveComment(int month, int year, string brokerId, string comment)
        {
            m_session.CreateSQLQuery("exec dbo.xGEM_FundingExceptionSummary_UpdateComment :month, :year, :brokerId, :comment")
                .SetInt32("month", month)
                .SetInt32("year", year)
                .SetString("brokerId", brokerId)
                .SetString("comment", comment)
                .ExecuteUpdate();
        }


    }
}
