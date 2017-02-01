using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Accounting;

namespace Bling.Presenter.Accounting
{
    public interface IIncomeBreakdownView
    {
        string ApplicationOrLoanNumber { get; }
        IncomeBreakdown IncomeBreakdown { set; }
    }

    public class IncomeBreakdownPresenter : Presenter
    {
        private IIncomeBreakdownView m_View;

        public IncomeBreakdownPresenter(IIncomeBreakdownView view)
        {
            m_View = view;
        }

        
    }
}
