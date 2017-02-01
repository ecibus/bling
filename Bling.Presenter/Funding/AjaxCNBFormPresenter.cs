using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Funding;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace Bling.Presenter.Funding
{
    public class AjaxCNBFormPresenter : Presenter
    {
        private IAjaxView m_View;
        private ICNBDao m_Dao;
        private string m_Path;

        public AjaxCNBFormPresenter(IAjaxView view)
            : this(view, new CNBDao(DMDDataSession()))
        {
        }

        public AjaxCNBFormPresenter(IAjaxView view, ICNBDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }


        public string PreviewCSV(string path, string start, string end, string batchno, int includeByte)
        {
            string message;

            try
            {
                message = Preview(path, start, end, batchno, includeByte);
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return message;
        }

        public string GenerateCSV(string path, string start, string end, string batchno, int includeByte)
        {
            string message;

            try
            {
                message = Generate(path, start, end, batchno, includeByte);
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return message;
        }

        //private string Generate(string path, string start, string end, string batchno)
        //{
        //    string targetFile = String.Format("CNB_{0}.csv", DateTime.Now.ToString("yyyyMMdd"));
        //    m_Path = path;

        //    var data = m_Dao.GetData(start, end, batchno);

        //    using (TextWriter writer = File.CreateText(m_Path + "\\" + targetFile))
        //    {
        //        foreach (var row in data)
        //        {
        //            int colCount = row.Count;
        //            int counter = 1;
        //            foreach (var col in row)
        //            {
        //                writer.Write("\"{0}\"{1}", col, counter++ < colCount ? "," : "");
        //            }
        //            writer.WriteLine("");
        //        }
        //    }

        //    return String.Format("<a href='Report/{0}'>Right click and 'Save Target As' to get the CSV file</a>", targetFile);

        //}

        private string Generate(string path, string start, string end, string batchno, int includeByte)
        {
            string targetFile = String.Format("CNB_{0}.xls", DateTime.Now.ToString("yyyyMMdd"));
            m_Path = path;

            var data = m_Dao.GetData(start, end, batchno, includeByte);
            ApplicationClass app = new ApplicationClass();
            app.Workbooks.Add();
            Workbook wb = app.ActiveWorkbook;
            Worksheet ws = (Worksheet)wb.ActiveSheet;
            int cntr = 1;
            ws.Cells.ColumnWidth = 20;
            foreach (var row in data)
            {
                int colCount = row.Count;
                int colIndex = 1;
                foreach (var col in row)
                {
                    if (colIndex == 37)
                    {
                        ws.get_Range(ws.Cells[cntr, colIndex], ws.Cells[cntr, colIndex]).NumberFormat = "@";
                        ws.Cells[cntr, colIndex] = col;
                    }
                    if (colIndex == 53 || colIndex == 54)
                    {
                        ws.get_Range(ws.Cells[cntr, colIndex], ws.Cells[cntr, colIndex]).NumberFormat = "@";
                        ws.Cells[cntr, colIndex] = col;
                    }
                    else
                    {
                        ws.Cells[cntr, colIndex] = col;
                    }
                    colIndex += 1;
                }

                cntr += 1;

            }

            if (File.Exists(Path.Combine(m_Path, targetFile)))
            {
                File.Delete(Path.Combine(m_Path, targetFile));
            }
            wb.SaveAs(Path.Combine(m_Path, targetFile));
            wb.Close();
            app.Quit();
            Marshal.ReleaseComObject(ws);
            Marshal.ReleaseComObject(wb);
            Marshal.ReleaseComObject(app);

            return String.Format("<a href='Report/{0}'>Right click and 'Save Target As' to get the Excel file</a>", targetFile);

        }

        private string Preview(string path, string start, string end, string batchno, int includeByte)
        {
            StringBuilder html = new StringBuilder();

            var data = m_Dao.GetData(start, end, batchno, includeByte);

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
