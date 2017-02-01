using System;
using System.Collections.Generic;
using System.Linq;
using Bling.Domain.Accounting;
using log4net;
using NHibernate;
using NHibernate.Criterion;
using Bling.Domain.Extension;

namespace Bling.Repository.Accounting
{
    public interface IBranchDao <T>
    {
        List<Branch> GetActiveBranch();
        List<T> GetTBranch();
        void Add(T branch);
    }

    public class BranchDao<T> : AbstractDao<T, int>, IBranchDao<T>
        where T : IBranchId
    {
        public BranchDao(ISession session)
            : base(session)
        {
            m_logger = LogManager.GetLogger(typeof(BranchDao<T>));            
        }

        public List<Branch> GetActiveBranch()
        {
            return m_session.CreateCriteria(typeof(Branch))
                .Add(Expression.Eq("Active", true))
                .List<Branch>().ToList();
        }

        public List<T> GetTBranch()
        {
            return m_session.CreateCriteria(typeof(T))
                .List<T>().ToList();
        }

        public void Add(T branch)
        {
            m_session.Save(branch);
            m_logger.DebugFormat("{0} added Branch {1}", branch.CreatedBy.Capitalize(), branch.BranchId);
        }
    }

}
