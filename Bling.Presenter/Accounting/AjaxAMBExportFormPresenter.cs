using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Accounting;
using System.IO;

namespace Bling.Presenter.Accounting
{
    public class AjaxAMBExportFormPresenter : Presenter
    {
        private IAjaxView m_View;
        private IAMBExportDao m_Dao;
        private string m_Path;
       

        public AjaxAMBExportFormPresenter(IAjaxView view)
            : this (view, new AMBExportDao(DMDDataSession()))
        {
        }

        public AjaxAMBExportFormPresenter(IAjaxView view, IAMBExportDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void GenerateCSV(string path, string from, string to)
        {
            m_Path = path;
            string targetFile = String.Format("AMB_{0:yyyyMMdd}.csv", DateTime.Now);
            var data = m_Dao.GetData(from, to);

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

            m_View.ResponseText = "Click this <a href='Report/" + targetFile + "'>link</a> to get the CSV file.";
        }
    }
}
