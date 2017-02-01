using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.HR;
using System.IO;

namespace Bling.Presenter.HR
{
    public class Ajax401KPresenter : Presenter
    {
        private IAjaxView m_View;
        private I401KDao m_Dao;

        public Ajax401KPresenter(IAjaxView view)
            : this (view, new _401KDao(DMDDataSession()))
        {
        }

        public Ajax401KPresenter(IAjaxView view, I401KDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public int View401KReport(string reportName, string pdfName, string start, string end, string isWeekly)
        {
            Crystal crystal = new Crystal(reportName)
               .ConnectToDataDepot()
               .SetDestinationToPDFAndRename(reportName, pdfName)
               .AddParameter("@start", start)
               .AddParameter("@end", end)
               .AddParameter("@isWeekly", isWeekly == "w" ? 1 : 0)
               .ViewReport(true);

            crystal.Dispose();

            int recCount = crystal.NumberOfRecordsSelected;
            m_View.ResponseText = "{}";
            return recCount;
        }

        public void GenerateCSV(string path, string start, string end, string isWeekly)
        {
            var data = m_Dao.GetData(start, end, isWeekly);

            using (TextWriter writer = File.CreateText(path + "\\" + "401K.csv"))
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

            m_View.ResponseText = "{ 'Message' : 'Click this <a href=\"Report/" + "401K.csv" + "\">link</a> to get the CSV file.' } ";

        }

        public void LoadDates(string reportType)
        {
            DateTime now = DateTime.Now;
            //now = new DateTime(2012, 8, 15);
            string startDate = "";
            string endDate = "";

            if (reportType.ToLower() == "w")
            {
                DateTime mondayOneWeekAgo = now.AddDays(-7);
                while (mondayOneWeekAgo.DayOfWeek != DayOfWeek.Monday)
                {
                    mondayOneWeekAgo = mondayOneWeekAgo.AddDays(-1);
                }
                startDate = mondayOneWeekAgo.ToShortDateString();
                endDate = mondayOneWeekAgo.AddDays(4).ToShortDateString();
            }
            else
            {
                if (now.Day > 15)
                {
                    startDate = new DateTime(now.Year, now.Month, 1).ToShortDateString();
                    endDate = new DateTime(now.Year, now.Month, 15).ToShortDateString();
                }
                else
                {
                    DateTime lastMonth = now.AddMonths(-1);
                    startDate = new DateTime(lastMonth.Year, lastMonth.Month, 16).ToShortDateString();
                    endDate = (new DateTime(now.Year, now.Month, 1)).AddDays(-1).ToShortDateString();
                }
            }

            m_View.ResponseText = String.Format("{{ Start : '{0}', End : '{1}' }}", startDate, endDate);
        }
    }

}
