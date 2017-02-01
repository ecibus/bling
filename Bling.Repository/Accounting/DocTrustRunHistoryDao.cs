using System;
using System.Collections.Generic;
using System.Linq;
using Bling.Domain.Accounting;
using NHibernate;
using NHibernate.Criterion;

namespace Bling.Repository.Accounting
{
    public interface IDocTrustRunHistoryDao : IDao<DocTrustRunHistory, int>
    {
        List<DocTrustRunHistory> GetLast10History();
    }

    public class DocTrustRunHistoryDao : AbstractDao<DocTrustRunHistory, int>, IDocTrustRunHistoryDao
    {
        public DocTrustRunHistoryDao(ISession session)
            : base(session)
        {
            
        }

        public List<DocTrustRunHistory> GetLast10History()
        {
            return m_session.CreateCriteria(typeof(DocTrustRunHistory))
                .AddOrder(Order.Desc("CreatedOn"))
                .SetMaxResults(10)
                .List<DocTrustRunHistory>().ToList();
        }

    }
}
