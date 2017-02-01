using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain;

namespace Bling.Presenter.HR
{
    public interface ITimeCardFormView
    {
        string MonthHtml { set; }
        string YearHtml { set; }
    }

    public class TimeCardFormPresenter : Presenter
    {
        private ITimeCardFormView m_View;

        public TimeCardFormPresenter(ITimeCardFormView view)
        {
            m_View = view;
        }

        public void Load()
        {
            CalendarHtml cal = new CalendarHtml();

            m_View.MonthHtml = cal.MonthDropDown(DateTime.Now.AddMonths(-1).ToString("MM"));
            m_View.YearHtml = cal.YearDropDown2(2, DateTime.Now.Year.ToString());
        }
    }
}
