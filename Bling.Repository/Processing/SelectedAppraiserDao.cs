using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Bling.Domain.Processing;
using NHibernate.Criterion;


namespace Bling.Repository.Processing
{
    public interface ISelectedAppraiserDao : IDao<SelectedAppraiser, int>
    {
        string GetLastAppraiserForBranch(string branchNo);
    }

    public class SelectedAppraiserDao : AbstractDao<SelectedAppraiser, int>, ISelectedAppraiserDao
    {
        public SelectedAppraiserDao(ISession session)
            : base(session)
        {            
        }

        public string GetLastAppraiserForBranch(string branchNo)
        {            
            SelectedAppraiser appraiser = m_session.CreateCriteria(typeof(SelectedAppraiser))
                .Add(Expression.Eq("BranchNo", branchNo))
                .AddOrder(Order.Desc("Id"))
                .SetMaxResults(1)
                .UniqueResult<SelectedAppraiser>();

            if (appraiser != null)
                return appraiser.AppraiserId;

            return "";
        }

    }
}
