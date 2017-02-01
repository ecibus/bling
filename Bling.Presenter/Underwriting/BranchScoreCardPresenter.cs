using System;
using System.Linq;
using System.Text;
using Bling.Repository;
using Bling.Domain;

namespace Bling.Presenter.Underwriting
{
    public interface IBranchScoreCardView
    {
        string MonthHtml { set; }
        string YearHtml { set; }
        string BranchHtml { set; }
    }

    public class BranchScoreCardPresenter : Presenter
    {
        private IBranchScoreCardView m_View;
        private IBrokerDao m_BrokerDao;
        private DateTime m_Now;

        public BranchScoreCardPresenter(IBranchScoreCardView view, DateTime now)
            : this(view, now, new BrokerDao(DMDDataSession()))
        {
        }

        public BranchScoreCardPresenter(IBranchScoreCardView view, DateTime now, IBrokerDao brokerDao)
        {
            m_View = view;
            m_Now = now;
            m_BrokerDao = brokerDao;            
        }

        public void Load()
        {
            m_View.MonthHtml = CreateMonthDropdown();
            m_View.YearHtml = CreateYearDropdown();
            m_View.BranchHtml = Broker.ToHtmlOptionList("", m_BrokerDao.GetActiveBroker().OrderBy(x => x.DBA).ToList(), "1");
        }

        private string CreateMonthDropdown()
        {
            StringBuilder dropdown = new StringBuilder();
            
            dropdown.Append("<select id=\"ddlMonth\">");
            for (int i = 1; i <= 12; i++)
            {
                string month = Convert.ToDateTime(String.Format("{0}/1/2009", i)).ToString("MMMM");
                dropdown.AppendFormat("<option{0}>{1}</option>", m_Now.Month == i ? " selected=\"selected\"" : "", month);
            }
            dropdown.Append("</select>");

            return dropdown.ToString();
        }

        private string CreateYearDropdown()
        {
            StringBuilder dropdown = new StringBuilder();
            DateTime startYear = m_Now;

            dropdown.Append("<select id=\"ddlYear\">");
            for (int i = 0; i < 10; i++)
            {
                string year = startYear.AddYears(i * -1).ToString("yyyy");                
                dropdown.AppendFormat("<option{0}>{1}</option>", m_Now.Year.ToString() == year ? " selected=\"selected\"" : "", year);
            }
            dropdown.Append("</select>");

            return dropdown.ToString();
        }
    }
}
