using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Presenter.Processing;

namespace Bling.Web.Processing
{
    public partial class AjaxQCExportForm : BasePage, IAjaxView
    {
        private AjaxQCExportFormPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "generate":
                        m_Presenter.GenerateCSV(Server.MapPath("Report"), Request.Form["from"], Request.Form["to"], Request.Form["includeDataTrac"] == "true" ? 1 : 0, Request.Form["includeByte"] == "true" ? 1 : 0, Request.Form["loans"], Request.Form["dateType"]);
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
            m_Presenter = new AjaxQCExportFormPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { get; set; }
    }
}