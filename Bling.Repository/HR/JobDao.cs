using System;
using Bling.Domain.HR;
using NHibernate;
using System.Collections.Generic;
using NHibernate.Criterion;

namespace Bling.Repository.HR
{
    public interface IJobDao : IDao<Job, int>
    {
        IList<Job> GetOpenJobs();
        IList<Job> GetCloseJobs();
    }

    public class JobDao : AbstractDao<Job, int>, IJobDao
    {
        public JobDao(ISession session)
            : base(session)
        {            
        }

        public IList<Job> GetOpenJobs()
        {
            return m_session.CreateCriteria(typeof(Job))
                .Add(Expression.IsNull("CloseDate"))
                .AddOrder(Order.Asc("Title"))
                .List<Job>();
        }

        public IList<Job> GetCloseJobs()
        {
            return m_session.CreateCriteria(typeof(Job))
                .Add(Expression.IsNotNull("CloseDate"))
                .AddOrder(Order.Asc("Title"))
                .List<Job>();
        }
    }
}
