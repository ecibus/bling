using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Accounting;
using Bling.Repository.Accounting;

namespace Bling.Presenter.Accounting
{
    public interface ICashDepositEntryView 
    {
        string Account { set; }        
    }

    public class CashDepositEntryPresenter : Presenter
    {
        private ICashDepositEntryView m_View;
        private ICashDepositAccountDao m_AccountListDao;

        public CashDepositEntryPresenter(ICashDepositEntryView view)
            : this (view, new CashDepositAccountDao(MWDataStoreSession()))
        {
        }

        public CashDepositEntryPresenter(ICashDepositEntryView view, ICashDepositAccountDao accountDao)
        {
            m_View = view;
            m_AccountListDao = accountDao;
        }

        public void Load()
        {
            IList<CashDepositAccount> Accounts = m_AccountListDao.GetAll().OrderBy(x => x.AccountNo).ToList();

            StringBuilder dropdown = new StringBuilder();
            dropdown.Append("<select class='span-5' id='ddlCashDepositAccount'>");
            dropdown.AppendFormat("<option value='{0}'>{1}</option>", "", "");
            foreach (var a in Accounts)
            {
                dropdown.AppendFormat("<option value='{0}'>{0} - {1}</option>", a.AccountNo, a.AccountDescription);
            }
            dropdown.Append("</select>");
            m_View.Account = dropdown.ToString();
        }
    }
}
