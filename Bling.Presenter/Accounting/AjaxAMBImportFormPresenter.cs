using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Accounting;
using System.IO;

namespace Bling.Presenter.Accounting
{
    public class AjaxAMBImportFormPresenter : Presenter
    {
        private IAjaxView m_View;
        private IAMBImportDao m_Dao;
        private string m_Path;
       

        public AjaxAMBImportFormPresenter(IAjaxView view)
            : this (view, new AMBImportDao(DMDDataSession()))
        {
        }

        public AjaxAMBImportFormPresenter(IAjaxView view, IAMBImportDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void GenerateCSV(string path, string reportType, string from, string to, int includeByte)
        {
            m_Path = path;
            string targetFile = ""; // String.Format("AMB_{0:yyyyMMdd}.csv", DateTime.Now);
            string spName = "";

            switch (reportType)
            {
                case "1" :
                    targetFile = String.Format("BorrImport_{0:yyyyMMdd}.csv", DateTime.Now);
                    spName = "xGEM_AMBBorrowerImport";
                    break;

                case "2":
                    targetFile = String.Format("AddBorrImportFunding_{0:yyyyMMdd}.csv", DateTime.Now);
                    spName = "xGEM_AMBAdditionalBorrowerImportFunding";
                    break;

                case "3":
                    targetFile = String.Format("AddBorrImportPurchase_{0:yyyyMMdd}.csv", DateTime.Now);
                    spName = "xGEM_AMBAdditionalBorrowerImportPurchase";
                    break;

                case "4":
                    targetFile = String.Format("LoanPaymentPosting_{0:yyyyMMdd}.csv", DateTime.Now);
                    spName = "xGEM_AMBLoanPaymentPosting";
                    break;

                case "5":
                    targetFile = String.Format("FundingExtract_{0:yyyyMMdd}.csv", DateTime.Now);
                    spName = "xGEM_AMBFundingExtract";
                    break;

                case "6":
                    targetFile = String.Format("SecondaryGainReceivable_{0:yyyyMMdd}.csv", DateTime.Now);
                    spName = "xGEM_AMBSecondaryGainReceivable";
                    break;

                case "7":
                    targetFile = String.Format("HedgeLoanRevenueExtract_{0:yyyyMMdd}.csv", DateTime.Now);
                    spName = "xGEM_AMBHedgeLoanRevenueExtract";
                    break;

                case "8":
                    targetFile = String.Format("PurchaseExtract_{0:yyyyMMdd}.csv", DateTime.Now);
                    spName = "xGEM_AMBPurchaseExtract";
                    break;

                case "9":
                    targetFile = String.Format("BrokeredLoanExtract_{0:yyyyMMdd}.csv", DateTime.Now);
                    spName = "xGEM_AMBBrokeredLoanExtract";
                    break;

                case "10":
                    targetFile = String.Format("CommissionAccrual_{0:MMyyyy}.csv", DateTime.Now);
                    spName = "xGEM_AMBCommissionAccrual";
                    break;

                case "11":
                    targetFile = String.Format("CancelledDenied_{0:MMyyyy}.csv", DateTime.Now);
                    spName = "xReport_CancelledDeniedReportByte";
                    break;

            }

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
