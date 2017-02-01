using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Accounting;
using NHibernate;

namespace Bling.Repository.Accounting
{
    public interface ICashDepositDao : IDao<CashDeposit, int>
    {
        IList<CashDeposit> GetByInputDate(string inputDate);
    }

    public class CashDepositDao: AbstractDao<CashDeposit, int>, ICashDepositDao
    {
        public CashDepositDao(ISession session)
            : base(session)
        {
        }

        public IList<CashDeposit> GetByInputDate(string inputDate)
        {
            return m_session.CreateSQLQuery("exec xGEM_CashDeposit_GetByInputDate :inputDate")
                .AddEntity(typeof(CashDeposit))
                .SetString("inputDate", inputDate)
                .List<CashDeposit>()
                .ToList();
        }
    }
}
