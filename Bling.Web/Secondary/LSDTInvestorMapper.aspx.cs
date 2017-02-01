using System;
using System.Web.UI;
using Bling.Presenter.Secondary;

namespace Bling.Web.Secondary
{
    public partial class LSDTINvestorMapper : BasePage, ILSDTInvestorMapperView
    {
        protected string m_TableMapping;
        private LSDTInvestorMapperPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                    m_Presenter.LoadData();
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new LSDTInvestorMapperPresenter(this);
            base.OnInit(e);
        }
        public string InvestorMapping
        {
            set
            {
                m_TableMapping = value;
                if (m_TableMapping == String.Empty)
                    InfoMessage = "Please import data using \"Upload Loan Solution\" before using this screen.";
            }            
        }

    }
}
