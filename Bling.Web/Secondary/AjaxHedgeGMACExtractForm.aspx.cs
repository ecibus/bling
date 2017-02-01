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
    public partial class AjaxHedgeGMACExtractForm : BasePage, IAjaxView
    {
        public string ResponseText { get; set; }
        private AjaxHedgeGMACPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "generate":
                        m_Presenter.Generate(Server.MapPath("Report"), Request.Form["start"], Request.Form["end"]);
                        ResponseText = "\"Right Click\" then \"Save Target As\" this <a href='Report/GMAC.csv'>link</a> to get the generated file.";
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
            m_Presenter = new AjaxHedgeGMACPresenter(this);
            base.OnInit(e);
        }
    }
}