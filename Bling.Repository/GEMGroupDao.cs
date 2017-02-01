using System;
using System.Collections.Generic;
using System.Linq;
using Bling.Domain;
using NHibernate;
using NHibernate.Criterion;
namespace Bling.Repository
{
    public interface IGEMGroupDao
    {
        List<GEMGroup> GetAvailableGroup();
        List<GEMApplication> GetApplicationByGroup(string groupName);
        void AddApplication(string groupName, GEMApplication app);
        void RemoveApplication(string groupName, GEMApplication app);

    }

    public class GEMGroupDao : AbstractDao<GEMGroup, int>, IGEMGroupDao
    {
        public GEMGroupDao(ISession session)
            : base(session)
        {            
        }

        public List<GEMGroup> GetAvailableGroup()
        {
            return GetAll().ToList();
        }

        public List<GEMApplication> GetApplicationByGroup(string groupName)
        {
            GEMGroup group = m_session.CreateCriteria(typeof(GEMGroup))
                .Add(Expression.Eq("GroupName", groupName))
                //.SetFetchMode("Applications", FetchMode.Eager)
                .UniqueResult<GEMGroup>();

            return group.Applications.ToList();
        }

        public void AddApplication(string groupName, GEMApplication app)
        {
            GEMGroup group = m_session.CreateCriteria(typeof(GEMGroup))
                .Add(Expression.Eq("GroupName", groupName))
                .UniqueResult<GEMGroup>();
            group.Applications.Add(app);
        }

        public void RemoveApplication(string groupName, GEMApplication app)
        {
            GEMGroup group = m_session.CreateCriteria(typeof(GEMGroup))
               .Add(Expression.Eq("GroupName", groupName))
               .UniqueResult<GEMGroup>();

            List<GEMApplication> apps = new List<GEMApplication>(group.Applications);

            apps.ForEach(a =>
            {
                if (app.Id == a.Id)
                    group.Applications.Remove(a);
            });
        }

    }
}
