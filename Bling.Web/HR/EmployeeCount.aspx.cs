using System;
using Bling.Presenter.HR;

namespace Bling.Web.HR
{
    public partial class EmployeeCount : BasePage, IEmployeeCount
    {
        private EmployeeCountPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new EmployeeCountPresenter(this);
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

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                string report = "Report/EmpCount.rpt";
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

    }
}
