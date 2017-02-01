using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Accounting;
using NHibernate;
using log4net;
using NHibernate.Criterion;

namespace Bling.Repository.Accounting
{
    public interface IBranchCodeDao
    {
        IList<BranchCode> GetMarketingGainBranch();
    }

    public class BranchCodeDao : AbstractDao<BranchCode, int>, IBranchCodeDao
    {
        public BranchCodeDao(ISession session)
            : base(session)
        {
            m_logger = LogManager.GetLogger(typeof(BranchCodeDao));            
        }

        public IList<BranchCode> GetMarketingGainBranch()
        {
            return m_session.CreateCriteria(typeof(BranchCode))                
                .Add(Expression.Or(
                    Expression.Eq("MarketingGain", true),
                    Expression.And(
                        Expression.IsNotNull("MarketingGainBranchCode"), 
                        Expression.Not(Expression.Eq("MarketingGainBranchCode", 5))
                )))
                .List<BranchCode>().ToList();
        }
    }
}
