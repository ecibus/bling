using System;
using Bling.Domain.Secondary;
using NHibernate;

namespace Bling.Repository.Secondary
{
    public interface ILoanLockDetailDao : IDao<LoanLockDetail, string>
    {
        
    }

    public class LoanLockDetailDao : AbstractDao<LoanLockDetail, string>, ILoanLockDetailDao
    {
        public LoanLockDetailDao(ISession session)
            : base(session)
        {            
        }
    }
}
