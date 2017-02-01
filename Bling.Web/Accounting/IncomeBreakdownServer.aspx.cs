using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter.Accounting;
using log4net;

namespace Bling.Web.Accounting
{
    public partial class IncomeBreakdownServer : BasePage, IIncomeBreakdownServerView
    {
        protected string m_ResponseText;
        private IncomeBreakdownServerPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "getincomebreakdown":
                        m_Presenter.GetIncomeBreakdown(Request.Form["loannumber"]);
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
            m_subTitle = "Income Breakdown Ajax Call";
            m_logger = LogManager.GetLogger(typeof(IncomeBreakdownServer));
            m_Presenter = new IncomeBreakdownServerPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText
        {
            set { m_ResponseText = value; }
        }

    }
}
