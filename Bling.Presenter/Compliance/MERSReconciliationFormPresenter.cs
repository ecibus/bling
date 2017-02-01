using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bling.Repository.Compliance;

namespace Bling.Presenter.Compliance
{
    public interface IMERSReconciliationFormView
    {
        string SourceFileName { get; }
        string MERSData { set; }
    }

    public class MERSReconciliationFormPresenter : Presenter
    {
        private IMERSReconciliationFormView m_View;
        private IMERSReconciliationDao m_Dao;

        public MERSReconciliationFormPresenter(IMERSReconciliationFormView view)
            : this(view, new MERSReconciliationDao(DMDDataSession()))
        {
        }

        public MERSReconciliationFormPresenter(IMERSReconciliationFormView view, IMERSReconciliationDao dao)
        {
            m_Dao = dao;
            m_View = view;
        }

        public void LoadData()
        {
            StringBuilder list = new StringBuilder();
            string sql = " Select g.loan_num, convert(varchar(10), a.purchased, 101) purchased, g.mers_no from dbo.gen g left join dbo.act a on g.file_id = a.file_id where a.purchased is not null and g.mers_no in ( ";
            using (TextReader reader = File.OpenText(m_View.SourceFileName))
            {
                while (reader.Peek() != -1)
                {
                    //SecurityConnectionShipDateInfo sc = new SecurityConnectionShipDateInfo(reader.ReadLine());
                    string[] data = reader.ReadLine().Split('\t');

                    if (data.Length == 0)
                        return;
                    

                    if (data.Length > 1)
                    {

                        list.AppendFormat("'{0}', ", data[1]);
                    }
                }
                reader.Close();
            }

            list.Remove(list.Length - 2, 1);

            sql = sql + list.ToString() + " ) order by g.loan_num";

            var purchasedData = m_Dao.GetData(sql);

            StringBuilder html = new StringBuilder("<table class='t1'>");
            html.AppendFormat("<tr class='yellow'><td>{0}</td><td>{1}</td><td>{2}</td></tr>", "Loan Number", "Purchased Date", "Mers No");

            purchasedData.ToList().ForEach(x => html.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", x.LoanNumber, x.PurchasedDate, x.MersNo));


            html.AppendFormat("</table>");

            m_View.MERSData = html.ToString();
        }
    }
}
