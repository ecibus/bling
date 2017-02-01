using System;
using Bling.Presenter.Processing;
using Bling.Domain.Extension;

namespace Bling.Web.Processing
{
    public partial class CPP : BasePage, IProcessingReport
    {
        private CPPPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new CPPPresenter(this);
            base.OnInit(e);
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                string report = "Report/PntCPP.rpt";
                m_Presenter.ViewReport(Server.MapPath(report));

                string popupScript =
                    String.Format("$(function () {{ window.open('{0}', 'Report'); }});",
                    report.Replace(".rpt", ".pdf"));

                ClientScript.RegisterStartupScript(GetType(), "Report", popupScript, true);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }         
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
