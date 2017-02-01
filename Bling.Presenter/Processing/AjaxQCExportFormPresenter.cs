using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Processing;
using System.IO;

namespace Bling.Presenter.Processing
{
    public class AjaxQCExportFormPresenter : Presenter
    {
        private IAjaxView m_View;
        private IQCExportDao m_Dao;
        private string m_Path;
       

        public AjaxQCExportFormPresenter(IAjaxView view)
            : this (view, new QCExportDao(DMDDataSession()))
        {
        }

        public AjaxQCExportFormPresenter(IAjaxView view, IQCExportDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void GenerateCSV(string path, string from, string to, int includeDataTrac, int includeByte, string loans, string dateType)
        {
            m_Path = path;
            string targetFile = String.Format("QCExport_{0:yyyyMMdd}.csv", DateTime.Now);
            string spName = "xGEM_QCExport";
            var data = m_Dao.GetData(spName, from, to, includeDataTrac, includeByte, loans, dateType);

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
