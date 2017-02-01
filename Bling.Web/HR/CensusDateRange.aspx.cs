using System;
using Bling.Presenter.HR;

namespace Bling.Web.HR
{
    public partial class CensusDateRange : BasePage, ICensusDateRangeView
    {
        protected string m_From;
        protected string m_To;

        private CensusDateRangePresenter m_Presenter;

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
            m_Presenter = new CensusDateRangePresenter(this);
            base.OnInit(e);
        }

        public DateTime From
        {
            set { m_From = value.ToString("MM/dd/yyyy"); }
        }

        public DateTime To
        {
            set { m_To = value.ToShortDateString(); }
        }

    }
}
