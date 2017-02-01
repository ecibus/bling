using System;
using Bling.Presenter.Secondary;
using log4net;

namespace Bling.Web.Secondary
{
    public partial class UploadLoanSolution : BasePage, ILoanSolutionProgramCodeView
    {
        LoanSolutionProgramCodePresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.FileName == String.Empty)
                {
                    ErrorMessage = "Please select a file to upload.";
                    return;
                }

                FileUpload1.SaveAs(SourceFileName);
                m_Presenter.LoadFile();
                InfoMessage = String.Format("Done uploading {0}", FileUpload1.FileName);
                m_logger.DebugFormat("{0} uploaded loan solution file {1}", CurrentUser.UserInfo.FullName, FileUpload1.FileName);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_logger = LogManager.GetLogger(typeof(UploadLoanSolution));            
            m_Presenter = new LoanSolutionProgramCodePresenter(this);
            base.OnInit(e);
        }

        public string SourceFileName
        {
            get { return Server.MapPath(@"~\Secondary\Data\Source.txt"); }
        }              

        public string Warning
        {
            set { ErrorMessage = value; }
        }
    }
}
