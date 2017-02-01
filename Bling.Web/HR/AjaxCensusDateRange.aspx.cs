using System;
using Bling.Presenter;
using Bling.Presenter.HR;
using Bling.Domain.Extension;

namespace Bling.Web.HR
{
    public partial class AjaxCensusDateRange : BasePage, IAjaxView
    {
        protected string m_ResponseText;
        private AjaxCensusDateRangePresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "update":
                        m_Presenter.Save(Request["from"].ToDateTime(), Request["to"].ToDateTime());
                        break;

                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                m_ResponseText = ex.Message;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxCensusDateRangePresenter(this);
            base.OnInit(e);
        }

        public string ResponseText
        {
            set { m_ResponseText = value; }
        }

    }
}
