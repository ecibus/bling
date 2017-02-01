using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Accounting;
using Bling.Repository.Accounting;

namespace Bling.Presenter.Accounting
{
    public interface IIncomeBreakdownServerView : IAjaxView
    {        
    }

    public class IncomeBreakdownServerPresenter : Presenter
    {
        private IIncomeBreakdownServerView m_View;
        private IIncomeBreakdownDao m_Dao;

        public IncomeBreakdownServerPresenter(IIncomeBreakdownServerView view)
            : this(view, new IncomeBreakdownDao(MWDataStoreSession()))            
        {            
        }

        public IncomeBreakdownServerPresenter(IIncomeBreakdownServerView view, IIncomeBreakdownDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void GetIncomeBreakdown(string loanNumber)
        {
            IncomeBreakdown ib = m_Dao.GetByApplicationOrLoanNumber(loanNumber);

            m_View.ResponseText = ib == null ? String.Format("Could not find loan number {0}.", loanNumber) : ib.ToHtmlTable();
        }
    }
}
