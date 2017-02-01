using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Accounting;
using NHibernate;
using NHibernate.Criterion;

namespace Bling.Repository.Accounting
{
    public interface IIncomeBreakdownDao
    {
        IncomeBreakdown GetByApplicationOrLoanNumber(string number);
    }

    public class IncomeBreakdownDao : AbstractDao<IncomeBreakdown, string>, IIncomeBreakdownDao
    {
        public IncomeBreakdownDao(ISession session)
            : base(session)
        {            
        }

        public IncomeBreakdown GetByApplicationOrLoanNumber(string number)
        {
            return m_session.CreateCriteria(typeof(IncomeBreakdown))
                .Add(Expression.Eq("ApplicationNumber", number))
                .UniqueResult<IncomeBreakdown>();            
        }

    }
}
