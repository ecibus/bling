using System;
using System.Linq;
using Bling.Domain.HR;
using Bling.Repository.HR;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Bling.Presenter.HR
{
    public interface IJobListingView
    {
        string OpenJobsDropDown { set; }
        string AvailablePdfs { set; }
    }

    public class JobListingPresenter : Presenter
    {
        private IJobListingView m_View;
        private IJobDao m_Dao;

        public JobListingPresenter(IJobListingView view) : this(view, new JobDao(MWDataStoreSession()))        
        {            
        }

        public JobListingPresenter(IJobListingView view, IJobDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void Load()
        {
            m_View.OpenJobsDropDown = Job.ToSelectHTML(m_Dao.GetOpenJobs().ToList());
        }

        public void GetPdf(string path)
        {
            var files = Directory.GetFiles(path, "*.pdf");
            var pdfs = new StringBuilder("<option value=''>Choose Attachment</option>");
            foreach (var file in files)
            {
                var pos = file.LastIndexOf("\\");
                pdfs.AppendFormat("<option value='{0}'>{0}</option>", file.Substring(pos + 1));
            }
            m_View.AvailablePdfs = pdfs.ToString();
        }
    }
}
