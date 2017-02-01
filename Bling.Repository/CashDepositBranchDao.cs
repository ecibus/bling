using System;
using Bling.Domain;
using NHibernate;

namespace Bling.Repository
{
    public interface ICashDepositBranchDao : IDao<CashDepositBranch, string>
    {        
    }

    public class CashDepositBranchDao : AbstractDao<CashDepositBranch, string>, ICashDepositBranchDao
    {
        public CashDepositBranchDao(ISession session)
            : base(session)
        {            
        }
    }
}
