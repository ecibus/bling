using System;
using Bling.Domain.Extension;
using Bling.Presenter.Processing;

namespace Bling.Web.Processing
{
    public partial class GEMAppraisal : BasePage, IProcessingReport
    {
        private GEMAppraisalPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                string report = "Report/PntGAR2.rpt";
                if (chkGroup.Checked)
                    report = "Report/PntGAR.rpt";

                m_Presenter.ViewReport(Server.MapPath(report));

                string popupScript =
                    String.Format("$(function () {{ window.open('{0}', 'Report'); }});",
                    report.Replace(".rpt", ".pdf") + "?a=" + new Random().Next(9999));

                ClientScript.RegisterStartupScript(GetType(), "Report", popupScript, true);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new GEMAppraisalPresenter(this);
            base.OnInit(e);
        }

        public string From
        {
            get { return txtFrom.Text; }
        }

        public string To
        {
            get { return txtTo.Text; }
        }

        public int DateToSearch
        {
            get { return ddlDate.SelectedValue.ToInteger(); }
        }
    }
}
