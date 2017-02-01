using System;
using Bling.Presenter.HR;

namespace Bling.Web.HR
{
    public partial class ProcessLOCommission : BasePage, IProcessLOCommissionView
    {
        private ProcessLOCommissionPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_Presenter.Load();
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new ProcessLOCommissionPresenter(this);
            base.OnInit(e);
        }

        public string BranchHtml { set; get; }
        public string PayDate { set; get; }
        public string FundedAsOf { set; get; }
        public string MonthHtml { set; get; }
        public string YearHtml { set; get; }
        public string Deadline { set; get; }
        public string EndingDate { set; get; }
    }
}