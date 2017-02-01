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
    public partial class AjaxCSVExportForm : BasePage, IAjaxView
    {
        private AjaxCSVExportFormPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "generate":
                        m_Presenter.GenerateCSV(Server.MapPath("Report"), Request.Form["reportType"], Request.Form["from"], Request.Form["to"], Request.Form["includeByte"] == "true" ? 1 : 0);
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
            m_Presenter = new AjaxCSVExportFormPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { get; set; }
    }
}