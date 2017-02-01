using System;
using Bling.Presenter.Accounting;

namespace Bling.Web.Accounting
{
    public partial class DocTrustAcctTransfer : BasePage, IDocTrustView
    {
        private DocTrustPresenter m_Presenter;
        protected string m_RunHistory;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                m_Presenter.LoadHistory();
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected void btnStampTransfer_Click(object sender, EventArgs e)
        {
            try
            {                                   
                m_Presenter.Transfer();
                InfoMessage = "Done Transfering.";
            }
            catch (Exception ex)
            {
                LogError(ex);
            }            
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                string report = "Report/DTATL.rpt";
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

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new DocTrustPresenter(this);
            base.OnInit(e);
        }

        public string TransferDate
        {
            get { return txtDate.Text; }
        }

        public string AsOfDate
        {
            get { return txtTo.Text; }
        }

        public string CreatedBy
        {
            get { return CurrentUser.UserInfo.FirstName; }
        }

        public string HistoryTable
        {
            set { m_RunHistory = value; }
        }

       
    }
}
