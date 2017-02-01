using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.HR;
using NHibernate;

namespace Bling.Repository.HR
{
    public interface ICommissionAnalysisDao : IDao<CommissionAnalysis, string>
    {
        IList<CommissionAnalysis> GetAwaitingApproval();
        IList<CommissionAnalysis> GetLoanForStamping(string payDate, string endDate, int isWeekly);
        CommissionAnalysis GetLoan(string loanNumber);
        void Save(string loanNumber, string status, string approvedLO, string comment, string payDate);
        void StampPayDate(string loanNumber, string payDate);
        string GetStampedPayDate(string loanNumber);
    }

    public class CommissionAnalysisDao : AbstractDao<CommissionAnalysis, string>, ICommissionAnalysisDao
    {
        public CommissionAnalysisDao(ISession session)
            : base(session)
        {
        }

        public IList<CommissionAnalysis> GetAwaitingApproval()
        {
            return m_session.CreateSQLQuery("exec dbo.xGEM_CommissionAnalysis_AwaitingApproval")
                .AddEntity(typeof(CommissionAnalysis))
                .List<CommissionAnalysis>();
        }

        public void StampPayDate(string loanNumber, string payDate)
        {
            m_session.CreateSQLQuery("exec dbo.xGEM_CommissionAnalysis_StampPayDate :loanNumber, :payDate")
                .SetString("loanNumber", loanNumber)
                .SetString("payDate", payDate)
                .ExecuteUpdate();

        }


        public string GetStampedPayDate(string loanNumber)
        {
            return m_session.CreateSQLQuery("exec dbo.xGEM_CommissionAnalysis_GetStampedPayDate :loanNumber")
                .SetString("loanNumber", loanNumber)
                .UniqueResult<string>();

        }

        public IList<CommissionAnalysis> GetLoanForStamping(string payDate, string endDate, int isWeekly)
        {
            return m_session.CreateSQLQuery("exec dbo.xGEM_CommissionAnalysis_GetLoansForStamping :payDate, :endDate, :isWeekly")
                .AddEntity(typeof(CommissionAnalysis))
                .SetString("payDate", payDate)
                .SetString("endDate", endDate)
                .SetInt32("isWeekly", isWeekly)
                .List<CommissionAnalysis>();
        }

        public CommissionAnalysis GetLoan(string loanNumber)
        {
            return m_session.CreateSQLQuery("exec dbo.xGEM_CommissionAnalysis_GetLoan :loanNumber")
                .AddEntity(typeof(CommissionAnalysis))
                .SetString("loanNumber", loanNumber)
                .UniqueResult<CommissionAnalysis>();
        }

        public void Save(string loanNumber, string status, string approvedLO, string comment, string payDate)
        {
            m_session.CreateSQLQuery("exec dbo.xGEM_CommissionAnalysis_Save2 :loanNumber, :status, :approvedLO, :comment, :payDate")
                .SetString("loanNumber", loanNumber)
                .SetString("status", status)
                .SetString("approvedLO", approvedLO)
                .SetString("comment", comment)
                .SetString("payDate", payDate)
                .ExecuteUpdate();
        }
    }
}
