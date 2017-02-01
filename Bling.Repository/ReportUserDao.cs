using System;
using System.Collections.Generic;
using System.Linq;
using Bling.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace Bling.Repository
{
    public interface IReportUserDao : IDao<ReportUser, string>
    {
        List<ReportUser> GetAllFunder();
        List<ReportUser> GetAllUnderwriter();
        ReportUser AddFunder(string employId);
        ReportUser AddUnderwriter(string employId);
        ReportUser RemoveUserAsFunder(string employId);
        ReportUser RemoveUserAsUnderwriter(string employId);
    }

    public class ReportUserDao : AbstractDao<ReportUser, string>, IReportUserDao
    {
        public ReportUserDao(ISession session)
            : base(session)
        {            
        }

        public List<ReportUser> GetAllFunder()
        {
            return m_session.CreateCriteria(typeof(ReportUser))
                .Add(Expression.Eq("IsFunder", true))
                .AddOrder(Order.Asc("FirstName"))
                .List<ReportUser>().ToList();            
        }

        public List<ReportUser> GetAllUnderwriter()
        {
            return m_session.CreateCriteria(typeof(ReportUser))
                .Add(Expression.Eq("IsUnderwriter", true))
                .AddOrder(Order.Asc("FirstName"))
                .List<ReportUser>().ToList();     
        }

        public ReportUser AddFunder(string employId)
        {
            ReportUser user = GetById(employId);
            if (user == null)
            {
                user = new UserInfoDao(m_session).GetById(employId);
            }
            user.IsFunder = true;
            m_session.Save(user);
            return user;
        }

        public ReportUser AddUnderwriter(string employId)
        {
            ReportUser user = GetById(employId);
            if (user == null)
            {
                user = new UserInfoDao(m_session).GetById(employId);
            }
            user.IsUnderwriter = true;
            m_session.Save(user);
            return user;
        }

        public ReportUser RemoveUserAsFunder(string employId)
        {
            ReportUser user = GetById(employId);
            
            if (user == null)
                return user;

            if (!user.IsUnderwriter)
            {
                m_session.Delete(user);
            }
            else
            {
                user.IsFunder = false;
                Save(user);
            }
            return user;
        }

        public ReportUser RemoveUserAsUnderwriter(string employId)
        {
            ReportUser user = GetById(employId);
            
            if (user == null)
                return user;

            if (!user.IsFunder)
            {
                m_session.Delete(user);
            }
            else
            {
                user.IsUnderwriter = false;
                Save(user);
            }
            return user;
        }
    }
}
