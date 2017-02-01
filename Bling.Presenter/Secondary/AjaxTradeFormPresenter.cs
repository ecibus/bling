using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Secondary;
using System.IO;

namespace Bling.Presenter.Secondary
{
    public class AjaxTradeFormPresenter : Presenter
    {
        private IAjaxView m_View;
        private ITradeDao m_Dao;
        private string m_Path;


        public AjaxTradeFormPresenter(IAjaxView view)
            : this(view, new TradeDao(DMDDataSession()))
        {
        }

        public AjaxTradeFormPresenter(IAjaxView view, ITradeDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void GenerateCSV2(string path, string fromSettleDate, string toSettleDate, string fromPairOffDate, string toPairOffDate, string status, string sortBy)
        {
            m_Path = path;

            string targetFile = String.Format("{0}_{1:yyyyMMdd}.csv", "trade", DateTime.Now);

            var data = m_Dao.GetTradeFile2(fromSettleDate, toSettleDate, fromPairOffDate, toPairOffDate, status, sortBy);

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

        public void GenerateCSV(string path, string from, string to, string dateForRange, string sortBy)
        {
            m_Path = path;

            string targetFile = String.Format("{0}_{1:yyyyMMdd}.csv", "trade", DateTime.Now);

            var data = m_Dao.GetTradeFile(from, to, dateForRange, sortBy);

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
