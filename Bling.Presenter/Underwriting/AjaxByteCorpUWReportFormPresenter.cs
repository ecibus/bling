using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Bling.Domain;

namespace Bling.Presenter.Underwriting
{
    public class AjaxByteCorpUWReportFormPresenter : Presenter
    {
        private IAjaxView m_View;

        public AjaxByteCorpUWReportFormPresenter(IAjaxView view)
	    {
            m_View = view;
	    }

        public void GetParameters(string reportName)
        {
            Crystal crystal = new Crystal(reportName)
               .ConnectToDataDepot();

            crystal.Dispose();
        }

        public void GetReports(string path)
        {
            var files = Directory.GetFiles(path, "*.rpt");

            Crystal crystal = new Crystal("");

            var data = crystal.GetReportInfo(files.ToList());

            crystal.Dispose();
            m_View.ResponseText = JsonConvert.SerializeObject(data.OrderBy(x => x.Title));
        }

        public void ViewReport(string reportName, string pdfName, string parameters, string username)
        {
            try
            {

                if (File.Exists(pdfName))
                    File.Delete(pdfName);

                var crystal = new Crystal(reportName)
                   .ConnectToDataDepot()
                   .SetDestinationToPDFAndRename(reportName, pdfName);

                if (parameters != String.Empty)
                {
                    var param = parameters.Substring(0, parameters.Length - 1).Split(',');

                    foreach (var p in param)
                    {
                        var kvt = p.Split('|');
                        if (kvt[2] == "crDateTimeField")
                        {
                            crystal.AddParameter(kvt[0], Convert.ToDateTime(kvt[1]));
                        }
                        else
                        {
                            crystal.AddParameter(kvt[0], kvt[1]);
                        }
                    }
                    crystal.AddParameter("@und", username);
                    //crystal.AddParameter("@und", "vcheatwood");
                }

                crystal.ViewReport();

                crystal.Dispose();
                m_View.ResponseText = " { \"Message\" : \"Done Saving.\" } ";
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ \"Message\" : \"{0}\" }}", ex.Message.Replace("'", "\\'")); 
            }
        }



    }
}
