using System;
using Bling.Presenter.Underwriting;

namespace Bling.Web.Underwriting
{
    public partial class BranchScoreCard : BasePage, IBranchScoreCardView
    {
        private BranchScoreCardPresenter m_Presenter;
        protected string m_MonthHtml;
        protected string m_YearHtml;
        protected string m_BranchHtml;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_Presenter.Load();
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new BranchScoreCardPresenter(this, DateTime.Now);
            base.OnInit(e);
        }

        public string MonthHtml
        {
            set { m_MonthHtml = value; }
        }

        public string YearHtml
        {
            set { m_YearHtml = value; }
        }

        public string BranchHtml
        {
            set { m_BranchHtml = value; }
        }
    }
}
