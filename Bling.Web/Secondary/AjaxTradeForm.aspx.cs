using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Presenter.Secondary;

namespace Bling.Web.Secondary
{
    public partial class AjaxTradeForm : BasePage, IAjaxView
    {
        private AjaxTradeFormPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "generate":
                        m_Presenter.GenerateCSV(Server.MapPath("Report"), Request.Form["from"], Request.Form["to"], Request.Form["dateForRange"], Request.Form["sortBy"]);
                        break;
                    case "generate2":
                        m_Presenter.GenerateCSV2(Server.MapPath("Report"), Request.Form["fromSettleDate"], Request.Form["toSettleDate"], Request.Form["fromPairOffDate"], Request.Form["toPairOffDate"], Request.Form["status"], Request.Form["sortBy"]);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ResponseText = ex.Message;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxTradeFormPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { get; set; }
    }
}