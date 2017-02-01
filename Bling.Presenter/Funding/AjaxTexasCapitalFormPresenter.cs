using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bling.Repository.Funding;

namespace Bling.Presenter.Funding
{
    public class AjaxTexasCapitalFormPresenter : Presenter
    {
        private IAjaxView m_View;
        private ITexasCapitalDao m_Dao;
        private string m_Path;

        public AjaxTexasCapitalFormPresenter(IAjaxView view)
            : this(view, new TexasCapitalDao(DMDDataSession()))
        {
        }

        public AjaxTexasCapitalFormPresenter(IAjaxView view, ITexasCapitalDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }


        public string PreviewCSV(string path, string start, string end, string batchno)
        {
            string message;

            try
            {
                message = Preview(path, start, end, batchno);
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return message;
        }

        public string GenerateCSV(string path, string start, string end, string batchno)
        {
            string message;

            try
            {
                message = Generate(path, start, end, batchno);
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return message;
        }

        private string Generate(string path, string start, string end, string batchno)
        {       
            string targetFile = String.Format("TCB_{0}.csv", DateTime.Now.ToString("yyyyMMdd"));
            m_Path = path;

            var data = m_Dao.GetData(start, end, batchno);

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

            return String.Format("<a href='Report/{0}'>Right click and 'Save Target As' to get the CSV file</a>", targetFile);

        }

        private string Preview(string path, string start, string end, string batchno)
        {
            StringBuilder html = new StringBuilder();

            var data = m_Dao.GetData(start, end, batchno);

            html.Append("<table>");

            bool isHeader = true;

            foreach (var row in data)
            {
                int colCount = row.Count;
                if (isHeader)
                {
                    html.Append("<thead>");
                }

                html.Append("<tr>");

                foreach (var col in row)
                {
                    html.AppendFormat("<td>{0}</td>", col);
                }

                html.Append("</tr>");
                if (isHeader)
                {
                    html.Append("</thead>");
                    html.Append("<tbody>");
                    isHeader = false;
                }

            }
            html.Append("</tbody>");

            html.Append("</table>");


            return html.ToString();

        }
    }
}
