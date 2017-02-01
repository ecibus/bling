using System;
using Bling.Presenter.Secondary;

namespace Bling.Web.Secondary
{
    public partial class UploadMCM : BasePage, IUploadMCMView
    {
        private UploadMCMPresenter m_presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {

            try
            {
                m_presenter.SendFile();
                InfoMessage = "Done.";
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_presenter = new UploadMCMPresenter(this);
            base.OnInit(e);
        }

        public bool LockLoan
        {
            get { return chkLockedLoan.Checked; }
        }

        public bool ClosedLoan
        {
            get { return chkClosedLoan.Checked; }
        }

        public bool FalloutLoan
        {
            get { return chkFalloutLoan.Checked; }
        }

        public bool Trades
        {
            get { return chkTrades.Checked; }
        }

        public bool FTP
        {
            get { return chkSend.Checked; }
        }

        public bool IncludeByte
        {
            get { return true; }
        }

    }
}
