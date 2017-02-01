using System;
using System.Collections.Generic;
using Bling.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace Bling.Repository
{
    public interface IGEMApplicationDao : IDao<GEMApplication, int>
    {
        IList<GEMApplication> GetApplicationByParent(int parent);
        GEMApplication GetApplicationById(int id);
        GEMApplication GetApplicationByLink(string link);
    }

    public class GEMApplicationDao : AbstractDao<GEMApplication, int>,  IGEMApplicationDao
    {
        public GEMApplicationDao(ISession session) : base(session)
        {            
        }

        public IList<GEMApplication> GetApplicationByParent(int parent)
        {
            return m_session.CreateCriteria(typeof(GEMApplication))
                .Add(Expression.Eq("Parent", parent))
                .List<GEMApplication>();
        }

        public GEMApplication GetApplicationById(int id)
        {
            return GetById(id);
        }

        public GEMApplication GetApplicationByLink(string link)
        {
            return m_session.CreateCriteria(typeof(GEMApplication))
                .Add(Expression.Eq("Link", link))
                .SetMaxResults(1)
                .UniqueResult <GEMApplication>();
        }

    }
}
