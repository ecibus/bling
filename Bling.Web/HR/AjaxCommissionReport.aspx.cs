using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Presenter.HR;

namespace Bling.Web.HR
{
    public partial class AjaxCommissionReport : BasePage, IAjaxView
    {
        public string ResponseText { get; set; }
        private AjaxCommissionReportPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "view":
                        string report = Server.MapPath("Report/CommissionBranchLO.rpt");
                        string guid = Request.Form["guid"];
                        string pdfName = Server.MapPath(String.Format("Report/{0}-{1}.pdf", Request.Form["report"], guid));
                        m_Presenter.ViewReport(report, pdfName, Request.Form["reporttype"], Request.Form["start"], Request.Form["end"],
                            Request.Form["lo"], Request.Form["branch"]);
                        break;

                    case "viewdt":
                        string reportDT = Server.MapPath("Report/CommissionBranchLODT.rpt");
                        string guidDT = Request.Form["guid"];
                        string pdfNameDT = Server.MapPath(String.Format("Report/{0}-{1}.pdf", Request.Form["report"], guidDT));
                        m_Presenter.ViewReport(reportDT, pdfNameDT, Request.Form["reporttype"], Request.Form["start"], Request.Form["end"],
                            Request.Form["lo"], Request.Form["branch"]);
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
            m_Presenter = new AjaxCommissionReportPresenter(this);
            base.OnInit(e);
        }
    }
}