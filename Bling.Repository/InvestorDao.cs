using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace Bling.Repository
{
    public interface IInvestorDao : IDao<Investor, string>
    {
        List<Investor> GetAllActiveInvestor();
    }

    public class InvestorDao : AbstractDao<Investor, string> , IInvestorDao
    {
        public InvestorDao(ISession session)
            : base(session)
        {            
        }

        public List<Investor> GetAllActiveInvestor()
        {
            return m_session.CreateCriteria(typeof(Investor))
                .Add(Expression.Eq("Exclude", false))
                .List<Investor>()
                .OrderBy(i => i.Inv)
                .ToList();
        }
    }
}
