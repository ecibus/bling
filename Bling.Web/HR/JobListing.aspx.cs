using System;
using Bling.Presenter.HR;

namespace Bling.Web.HR
{
    public partial class JobListing : BasePage, IJobListingView
    {
        private JobListingPresenter m_Presenter;
        protected string m_OpenJobsDropdown;
        protected string m_AvailablePdfs;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                m_Presenter.Load();
                m_Presenter.GetPdf(Server.MapPath(@"~\hr\jobpdf"));
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new JobListingPresenter(this);
            base.OnInit(e);
        }

        public string OpenJobsDropDown
        {
            set { m_OpenJobsDropdown = value; }
        }

        public string AvailablePdfs
        {
            set { m_AvailablePdfs = value; }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.FileName == String.Empty)
                {
                    ErrorMessage = "Please select a file to upload.";
                    return;
                }

                string filename = Server.MapPath(@"~\hr\jobpdf\") + FileUpload1.FileName;
                FileUpload1.SaveAs(filename);
                m_Presenter.GetPdf(Server.MapPath(@"~\hr\jobpdf"));

                m_logger.DebugFormat("Uploading {0}", FileUpload1.FileName);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
    }
}
