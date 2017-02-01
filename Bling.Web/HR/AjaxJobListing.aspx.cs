using System;
using Bling.Presenter;
using Bling.Presenter.HR;
using Bling.Domain.Extension;
using Bling.Domain.HR;

namespace Bling.Web.HR
{
    public partial class AjaxJobListing : BasePage, IAjaxView
    {
        protected string m_ResponseText;
        private AjaxJobListingPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "getjob":
                        m_Presenter.GetJobAsJson(Request["id"].ToInteger());
                        break;

                    case "updatejob":
                        UpdateJob();
                        break;

                    case "createjob":
                        CreateJob();
                        break;

                    case "getopenjob":
                        GetOpenJob();
                        break;

                    case "getclosejob":
                        GetCloseJob();
                        break;

                    case "printjob":
                        PrintJob(Request["jobId"].ToInteger(), Request["guid"].ToString());
                        break;

                    case "emailjob":
                        EmailJob();
                        break;

                    case "emailjobtome":
                        EmailJobToMe();
                        break;

                    default:
                        break;

                }
            }
            catch (Exception ex)
            {                
                m_ResponseText = ex.Message;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxJobListingPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText
        {
            set { m_ResponseText = value; }
        }

        private void GetOpenJob()
        {
            m_Presenter.GetOpenJob();
        }

        private void GetCloseJob()
        {
            m_Presenter.GetCloseJob();
        }

        private void CreateJob()
        {
            Job job = new Job();

            SetJobValue(job);

            m_Presenter.SaveJob(job);
        }
        
        private void UpdateJob()
        {
            Job job = m_Presenter.GetJob(Request["id"].ToInteger());

            SetJobValue(job);
            m_Presenter.SaveJob(job);
        }

        private void PrintJob(int jobId, string guid)
        {
            Job job = m_Presenter.GetJob(jobId);

            string report = Server.MapPath("Report/JobDT.rpt");
            string pdfName = Server.MapPath(String.Format("Report/{0}-{1}.pdf", job.Title.Replace(" ", "").Replace("/", ""), guid));
            m_Presenter.PrintJob(report, jobId, pdfName);

        }

        private void EmailJob()
        {
            string guid = Guid.NewGuid().ToString();

            PrintJob(Request["jobId"].ToInteger(), guid);
            Job job = m_Presenter.GetJob(Request["jobId"].ToInteger());
            string pdfName = String.Format("Report/{0}-{1}.pdf", job.Title.Replace(" ", "").Replace("/", ""), guid);
            string additionalAttachment = "";
            if (!String.IsNullOrEmpty(job.Attachment))
            {
                additionalAttachment = Server.MapPath(String.Format("JobPdf/{0}", job.Attachment));
            }
            m_Presenter.EmailJob(Server.MapPath(pdfName), Server.MapPath("Report/Employee Referral Program.pdf"), 
                additionalAttachment, "");
        }

        private void EmailJobToMe()
        {
            string guid = Guid.NewGuid().ToString();
            PrintJob(Request["jobId"].ToInteger(), guid);
            Job job = m_Presenter.GetJob(Request["jobId"].ToInteger());
            string pdfName = String.Format("Report/{0}-{1}.pdf", job.Title.Replace(" ", "").Replace("/", ""), guid);
            string additionalAttachment = "";
            if (!String.IsNullOrEmpty(job.Attachment))
            {
                additionalAttachment = Server.MapPath(String.Format("JobPdf/{0}", job.Attachment));
            }
            m_Presenter.EmailJob(Server.MapPath(pdfName), Server.MapPath("Report/Employee Referral Program.pdf"),
                additionalAttachment, CurrentUser.UserInfo.EMail);
        }

        private void SetJobValue(Job job)
        {
            job.Title = Request["position"];
            job.Qualification = Request["qualification"];
            job.Description = Request["description"];
            job.Skills = Request["skills"];
            job.Education = Request["education"];
            job.FileName = Request["filename"];
            job.LocationCity = Request["locationCity"];
            job.LocationBranch = Request["locationBranch"];
            job.Salary = Request["salary"];
            job.Hourly = Request["hourly"];
            job.Benefits = Request["benefits"];
            job.CloseDate = Request["closeDate"].ToNullDateTime();
            job.PostDate = Request["postDate"].ToNullDateTime();
            job.FillDate = Request["fillDate"].ToNullDateTime();
            job.StartDate = Request["startDate"].ToNullDateTime();
            job.StartDateText = Request["startDateText"];
            job.Attachment = Request["attachment"];

        }

    }
}
