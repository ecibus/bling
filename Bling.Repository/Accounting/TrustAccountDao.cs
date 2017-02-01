using System;
using System.Collections.Generic;
using System.Linq;
using Bling.Domain.Accounting;
using NHibernate;
using NHibernate.Criterion;

namespace Bling.Repository.Accounting
{
    public interface ITrustAccountDao
    {
        List<TrustAccount> GetByApplicationNumber(string applicationNumber);
        TrustAccount GetById(int id);
        void SaveBackup(TrustAccountBackup backup);
        void RemoveEntry(TrustAccount trust);        
    }
    public class TrustAccountDao: AbstractDao<TrustAccount, int>, ITrustAccountDao
    {
        public TrustAccountDao(ISession session) : base(session)
        {            
        }
        
        public List<TrustAccount> GetByApplicationNumber(string applicationNumber)
        {
            var list = m_session.CreateCriteria(typeof(TrustAccount))
                .Add(Expression.Eq("ApplicationNumber", applicationNumber))
                .AddOrder(Order.Desc("Date"))
                .List<TrustAccount>().ToList();

            return list;
        }

        public void SaveBackup(TrustAccountBackup backup)
        {
            m_session.Save(backup);
        }

        public void RemoveEntry(TrustAccount trust)
        {
            m_session.Delete(trust);
        }
    }
}
