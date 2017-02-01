using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Accounting;
using System.IO;

namespace Bling.Presenter.Accounting
{
    public class AjaxWarehouseAdvanceRecapPresenter : Presenter
    {
        private IAjaxView m_View;
        private IWarehouseAdvanceRecapDao m_Dao;
        private string m_Path;

        public AjaxWarehouseAdvanceRecapPresenter(IAjaxView view)
            : this(view, new WarehouseAdvanceRecapDao())
        {
        }

        public AjaxWarehouseAdvanceRecapPresenter(IAjaxView view, IWarehouseAdvanceRecapDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void GenerateCSV(string path)
        {
            var data = m_Dao.GetData();
            var targetFile = String.Format("WarehouseAdvanceRecap_{0:yyyyMMdd}.csv", DateTime.Now);
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
    }
}
