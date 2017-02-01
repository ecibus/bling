using System;
using Bling.Presenter.Accounting;
using Bling.Domain.Extension;
using log4net;

namespace Bling.Web.Accounting
{
    public partial class AjaxTrustAccount : BasePage, IAjaxDocTrustView
    {
        protected string m_ResponseText;
        private AjaxTrustAccountPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {            
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "gettrustaccountbyappnumber":
                        m_Presenter.GetEntryByApplicationNumber(Request["AppNumber"].ToString());
                        break;

                    case "deleteentrybyid":
                        m_Presenter.DeleteEntryById(Request["id"].ToInteger(), CurrentUser.UserName);
                        break;

                    default:
                        break;

                }

            }
            catch (Exception ex)
            {
                m_ResponseText = ex.Message;
            }
            finally
            {                
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_subTitle = "Accounting Ajax Call";
            m_logger = LogManager.GetLogger(typeof(AjaxTrustAccount));            
            m_Presenter = new AjaxTrustAccountPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText
        {
            set { m_ResponseText = value; }
        }
    }
}
