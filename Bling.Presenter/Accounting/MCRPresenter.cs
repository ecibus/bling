using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain;

namespace Bling.Presenter.Accounting
{
    public interface IMCRView
    {
        string YearHtml { set; }
        string QuarterHtml { set; }
    }

    public class MCRPresenter : Presenter
    {
        private IMCRView m_View;

        public MCRPresenter(IMCRView view)
        {
            m_View = view;
        }

        public void Load()
        {
            CalendarHtml cal = new CalendarHtml();
            m_View.YearHtml = cal.YearDropDown2(2, DateTime.Now.Year.ToString());
            m_View.QuarterHtml = cal.QuarterlyDropDown();
        }

    }
}
