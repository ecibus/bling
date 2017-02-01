using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Accounting;
using NHibernate;

namespace Bling.Repository.Accounting
{
    public interface ICashDepositAccountDao : IDao<CashDepositAccount, string>
    {
    }

    public class CashDepositAccountDao : AbstractDao<CashDepositAccount, string>, ICashDepositAccountDao
    {
        public CashDepositAccountDao(ISession session)
            : base(session)
        {
        }
    }
}
