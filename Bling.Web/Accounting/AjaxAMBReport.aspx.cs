using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Presenter.Accounting;

namespace Bling.Web.Accounting
{
    public partial class AjaxAMBReport : BasePage, IAjaxView
    {

        private AjaxAMBReportFormPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "viewreport":
                        string report = Server.MapPath("Report/AppraisalBalance.rpt");
                        string pdfName = Server.MapPath(String.Format("Report/AppraisalBalance.pdf"));
                        m_Presenter.ViewReport(report, pdfName, Request.Form["reportType"], Request.Form["from"], Request.Form["to"]);
                        break;

                    case "exportreport":
                        m_Presenter.ExportToCSV(Server.MapPath("Report"), Request.Form["reportType"], Request.Form["from"], Request.Form["to"]);
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
            m_Presenter = new AjaxAMBReportFormPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { get; set; }
    }
}