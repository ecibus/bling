using System;
using System.Linq;
using Bling.Domain;
using Bling.Repository;

namespace Bling.Presenter.HR
{
    public interface IProcessLOCommissionView
    {
        string BranchHtml { set; }
        string PayDate { set; }
        string FundedAsOf { set; }
        string MonthHtml { set; }
        string YearHtml { set; }
        string Deadline { set; }
        string EndingDate { set; }
    }

    public class ProcessLOCommissionPresenter : Presenter
    {
        private IProcessLOCommissionView m_View;
        private IBrokerDao m_BrokerDao;

        public ProcessLOCommissionPresenter(IProcessLOCommissionView view)
            : this(view, new BrokerDao(DMDDataSession()))
        {
        }

        public ProcessLOCommissionPresenter(IProcessLOCommissionView view, IBrokerDao brokerDao)
        {
            m_View = view;
            m_BrokerDao = brokerDao;            
        }

        public void Load()
        {
            m_View.BranchHtml = Broker.ToHtmlOptionListWithAll("ddlBranchNo", 
                m_BrokerDao.GetActiveBranch(), "1");
                //m_BrokerDao.GetActiveBroker().OrderBy(x => x.IDNum).ToList(), "1");

            DateTime payDate = DateTime.Now;
            
            while (payDate.DayOfWeek != DayOfWeek.Friday)
            {
                payDate = payDate.AddDays(1);
            }

            m_View.PayDate = payDate.ToString("MM/dd/yyyy");
            m_View.FundedAsOf = payDate.AddDays(-14).ToString("MM/dd/yyyy");
            m_View.Deadline = payDate.AddDays(-3).ToString("MM/dd/yyyy");

            DateTime now = DateTime.Now;
            DateTime lastDayOfMonth = Convert.ToDateTime(now.Month.ToString() + "/1/" + now.Year.ToString()).AddDays(-1);
            m_View.EndingDate = lastDayOfMonth.ToString("MM/dd/yyyy");

            CalendarHtml cal = new CalendarHtml();

            m_View.MonthHtml = cal.MonthDropDown(DateTime.Now.AddMonths(-1).ToString("MM"));
            m_View.YearHtml = cal.YearDropDown2(2, DateTime.Now.Year.ToString());
        }
    }
}
