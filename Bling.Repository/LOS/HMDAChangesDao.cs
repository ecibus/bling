using System;
using System.Collections.Generic;
using System.Linq;
using Bling.Domain.LOS;
using NHibernate;
using NHibernate.Criterion;
using log4net;

namespace Bling.Repository.LOS
{
    public interface IHMDAChanges
    {
        List<HMDAChanges> FindByLoanNumber(string loanNumber);
        void Add(HMDAChanges newData);
        void Delete (int id);
    }

    public class HMDAChangesDao : AbstractDao<HMDAChanges, int>, IHMDAChanges
    {
        public HMDAChangesDao(ISession session) : base(session)
        {
            m_logger = LogManager.GetLogger(typeof(HMDAChangesDao));
        }

        public List<HMDAChanges> FindByLoanNumber(string loanNumber)
        {
            return m_session.CreateCriteria(typeof(HMDAChanges))
                .Add(Expression.Eq("LoanNumber", loanNumber))
                .List<HMDAChanges>().OrderBy(x => x.FieldName).ToList();
        }

        public void Add(HMDAChanges newData)
        {
            m_session.Save(newData);
        }

        public void Delete(int id)
        {
            HMDAChanges hmdaChanges = GetById(id);
            m_logger.DebugFormat("Deleting HMDAChanges for loan number '{0}' on a field of '{1}' with a value of '{2}'", 
                hmdaChanges.LoanNumber, hmdaChanges.FieldName, hmdaChanges.NewData);
            m_session.Delete(hmdaChanges);
        }
    }
}
