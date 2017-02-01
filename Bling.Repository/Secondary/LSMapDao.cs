using System;
using System.Collections.Generic;
using System.Linq;
using Bling.Domain.Secondary;
using NHibernate;
using NHibernate.Criterion;

namespace Bling.Repository.Secondary
{
    public interface ILSMapDao : IDao<LSMap, int>
    {
        List<string> GetInvestor();
        List<string> GetProgramCode();
        List<LSMap> GetByProgramCode(string loancode);        
    }

    public class LSMapDao : AbstractDao<LSMap, int>, ILSMapDao
    {
        public LSMapDao(ISession session)
            : base(session)
        {
            
        }

        public List<string> GetInvestor()
        {
            return m_session.CreateQuery("select distinct m.LS_InvestorCode from LSMap m Order by m.LS_InvestorCode")
                .List<string>().ToList();       
        }

        public List<string> GetProgramCode()
        {
            return m_session.CreateQuery("select distinct m.LS_LoanCode from LSMap m order by m.LS_LoanCode")                
                .List<string>().ToList();
        }

        public List<LSMap> GetByProgramCode(string loancode)
        {
            return m_session.CreateCriteria(typeof(LSMap))
                .Add(Expression.Eq("LS_LoanCode", loancode))
                .AddOrder(Order.Asc("LS_InvestorCode"))
                .List<LSMap>().ToList();
        }
    }
}
