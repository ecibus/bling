using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Accounting;
using Bling.Domain.Extension;
using System.IO;

namespace Bling.Presenter.Accounting
{
    public class AjaxAMBReportFormPresenter : Presenter
    {
        private IAjaxView m_View;
        private IAMBReportDao m_Dao;
        private string m_Path;

        public AjaxAMBReportFormPresenter(IAjaxView view)
            : this (view, new AMBReportDao(DMDDataSession()))
        {
        }

        public AjaxAMBReportFormPresenter(IAjaxView view, IAMBReportDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void ExportToCSV(string path, string reportType, string from, string to)
        {
            m_Dao.GetAppraisalBalanceData(reportType, from.ToDateTime().ToString("dd-MMM-yyyy"), to.ToDateTime().ToString("dd-MMM-yyyy"));


            var data = m_Dao.GetData("xReport_AMB_AppraisalBalanceCSV");
            string targetFile = String.Format("AppraisalBalance.csv");

            using (TextWriter writer = File.CreateText(path + "\\" + targetFile))
            {
                foreach (var row in data)
                {
                    int colCount = row.Count;
                    int counter = 1;
                    foreach (var col in row)
                    {
                        writer.Write("\"{0}\"{1}", col, counter++ < colCount ? "," : "");
                    }
                    writer.WriteLine("");
                }
            }

            m_View.ResponseText = "Click this <a href='Report/" + targetFile + "'>link</a> to get the CSV file.";

        }

        public void ViewReport(string reportName, string pdfName, string reportType, string from, string to)
        {
            if (File.Exists(pdfName))
                File.Delete(pdfName);

            var data = m_Dao.GetAppraisalBalanceData(reportType, from.ToDateTime().ToString("dd-MMM-yyyy"), to.ToDateTime().ToString("dd-MMM-yyyy"));

            new Crystal(reportName)
               .ConnectToDataDepot()
               .SetDestinationToPDFAndRename(reportName, pdfName)
               .ViewReport();

            m_View.ResponseText = "";
            //m_View.ResponseText = "Click this <a href='Report/" + targetFile + "'>link</a> to get the CSV file.";
        }

    }
}
