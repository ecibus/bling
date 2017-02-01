using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Accounting;
using Bling.Domain.Accounting;

namespace Bling.Presenter.Accounting
{
    public class AjaxCashDepositEntryPresenter : Presenter
    {
        private IAjaxView m_View;
        private ICashDepositDao m_Dao;

        public AjaxCashDepositEntryPresenter(IAjaxView view)
            : this (view, new CashDepositDao(MWDataStoreSession()))
        {
        }

        public AjaxCashDepositEntryPresenter(IAjaxView view, CashDepositDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void Load(string inputDate)
        {
            IList<CashDeposit> list = m_Dao.GetByInputDate(inputDate);

            StringBuilder tr = new StringBuilder();

            foreach (var l in list)
            {
                tr.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td class='number dollarAmount'>{3}</td><td class='bankAccount'>{4}</td><td></td></tr>",
                    l.AppNum, l.Branch, l.AccountNo, l.DollarAmount.ToString("#,###.00"), l.BankAcct
                    );
            }

            m_View.ResponseText = tr.ToString();
        }

        public void Save(CashDeposit cd)
        {
            m_Dao.Save(cd);
        }
    }
}
