using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bling.Repository.Secondary;

namespace Bling.Presenter.Secondary
{
    public class AjaxCSVExportFormPresenter : Presenter
    {
        private IAjaxView m_View;
        private ICSVExportDao m_Dao;
        private string m_Path;


        public AjaxCSVExportFormPresenter(IAjaxView view)
            : this(view, new CSVExportDao(DMDDataSession()))
        {
        }

        public AjaxCSVExportFormPresenter(IAjaxView view, ICSVExportDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void GenerateCSV(string path, string reportInfo, string from, string to, int includeByte)
        {
            m_Path = path;

            var reportInfoArray = reportInfo.Split('|');
            string spName = reportInfoArray[0];
            string targetFile = String.Format("{0}_{1:yyyyMMdd}.csv", reportInfoArray[1], DateTime.Now);

            var data = m_Dao.GetData(spName, from, to, includeByte);

            using (TextWriter writer = File.CreateText(m_Path + "\\" + targetFile))
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

            var r = new Random().Next(10000);
            m_View.ResponseText = "Click this <a href='Report/" + targetFile + "?r=" + r.ToString() + "'>link</a> to get the CSV file.";
        }
    }
}
