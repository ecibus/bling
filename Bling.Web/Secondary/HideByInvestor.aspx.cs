using System;
using Bling.Presenter.Secondary;

namespace Bling.Web.Secondary
{
    public partial class HideByInvestor : BasePage, IHideByInvestorView
    {
        private HideByInvestorPresenter m_Presenter;
        protected string m_InvestorDropdown;
        protected string m_Summary;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                m_Presenter.Load();
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new HideByInvestorPresenter(this);
            base.OnInit(e);
        }

        public string InvestorDropdown
        {
            set { m_InvestorDropdown = value; }
        }
        
        public string InvestorSummary
        {
            set { m_Summary = value; }
        }

    }
}
