using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Funding;
using System.IO;

namespace Bling.Presenter.Funding
{
    public class AjaxCustomersBankFormPresenter : Presenter
    {
        private IAjaxView m_View;
        private ICustomerBankDao m_Dao;
        private string m_Path;

        public AjaxCustomersBankFormPresenter(IAjaxView view)
            : this(view, new CustomerBankDao(DMDDataSession()))
        {
        }

        public AjaxCustomersBankFormPresenter(IAjaxView view, ICustomerBankDao dao)
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

        private string Generate(string path, string start, string end, string batchno, int includeByte)
        {       
            string targetFile = String.Format("CustomersBank_{0}.csv", DateTime.Now.ToString("yyyyMMdd"));
            m_Path = path;

            var list = m_Dao.GetData(start, end, batchno, includeByte);


            using (TextWriter writer = File.CreateText(m_Path + "\\" + targetFile))
            {
                writer.Write("\"Originator Loan Number\",");
                writer.Write("\"Mortgagee Loan Name\",");
                writer.Write("\"Originator Product Code\",");
                writer.Write("\"Requested Date\",");
                writer.Write("\"Note Amount\",");
                writer.Write("\"Note Date\",");
                writer.Write("\"Note Term\",");
                writer.Write("\"Note Rate\",");
                writer.Write("\"Borrower 1 First Name\",");
                writer.Write("\"Borrower 1 Last Name\",");
                writer.Write("\"Borrower 1 SSN\",");
                writer.Write("\"Borrower 2 First Name\",");
                writer.Write("\"Borrower 2 Last Name\",");
                writer.Write("\"Borrower 2 SSN\",");
                //writer.Write("\"Borrower 3 First Name\",");
                //writer.Write("\"Borrower 3 Last Name\",");
                //writer.Write("\"Borrower 3 SSN\",");
                writer.Write("\"Address\",");
                writer.Write("\"City\",");
                writer.Write("\"U.S. State\",");
                writer.Write("\"Zip\",");
                writer.Write("\"County\",");
                writer.Write("\"Closing Agent Primary Bank ABA Routing No\",");
                writer.Write("\"Closing Agent Final Bank Account Number\",");
                writer.Write("\"Closing Agent Order Number\",");
                writer.Write("\"Amortization Type\",");
                writer.Write("\"Documentation Type\",");
                writer.Write("\"Loan Type\",");
                writer.Write("\"MERS Number\",");
                writer.Write("\"Lien Position\",");
                writer.Write("\"Occupancy\",");
                writer.Write("\"Property Type\",");
                writer.Write("\"Rate Type\",");
                writer.Write("\"Transaction Type\",");
                writer.Write("\"Appraised Value\",");
                writer.Write("\"Credit Score\",");
                writer.Write("\"Warehouse Principal\",");
                writer.Write("\"Funds to Send Amount\",");
                writer.Write("\"Investor Name\"\n");

                foreach (var item in list)
                {
                    writer.Write("\"{0}\",", item.OriginatorLoanNumber);
                    writer.Write("\"{0}\",", item.MortgageeLoanName);
                    writer.Write("\"{0}\",", item.OriginatorProductCode);
                    writer.Write("\"{0}\",", item.RequestedDate);
                    writer.Write("\"{0}\",", item.NoteAmount);
                    writer.Write("\"{0}\",", item.NoteDate);
                    writer.Write("\"{0}\",", item.NoteTerm);
                    writer.Write("\"{0}\",", item.NoteRate);
                    writer.Write("\"{0}\",", item.Borrower1FirstName);
                    writer.Write("\"{0}\",", item.Borrower1LastName);
                    writer.Write("\"{0}\",", item.Borrower1SSN);
                    writer.Write("\"{0}\",", item.Borrower2FirstName);
                    writer.Write("\"{0}\",", item.Borrower2LastName);
                    writer.Write("\"{0}\",", item.Borrower2SSN);
                    //writer.Write("\"{0}\",", item.Borrower3FirstName);
                    //writer.Write("\"{0}\",", item.Borrower3LastName);
                    //writer.Write("\"{0}\",", item.Borrower3SSN);
                    writer.Write("\"{0}\",", item.Address);
                    writer.Write("\"{0}\",", item.City);
                    writer.Write("\"{0}\",", item.USState);
                    writer.Write("\"{0}\",", item.Zip);
                    writer.Write("\"{0}\",", item.County);
                    writer.Write("\"{0}\",", item.ClosingAgentPrimaryBankABARoutingNo);
                    writer.Write("\"{0}\",", item.ClosingAgentFinalBankAccountNumber);
                    writer.Write("\"{0}\",", item.ClosingAgentOrderNumber);
                    writer.Write("\"{0}\",", item.AmortizationType);
                    writer.Write("\"{0}\",", item.DocumentationType);
                    writer.Write("\"{0}\",", item.LoanType);
                    writer.Write("\"{0}\",", item.MERSNumber);
                    writer.Write("\"{0}\",", item.LienPosition);
                    writer.Write("\"{0}\",", item.Occupancy);
                    writer.Write("\"{0}\",", item.PropertyType);
                    writer.Write("\"{0}\",", item.RateType);
                    writer.Write("\"{0}\",", item.TransactionType);
                    writer.Write("\"{0}\",", item.AppraisedValue);
                    writer.Write("\"{0}\",", item.CreditScore);
                    writer.Write("\"{0}\",", item.WarehousePrincipal);
                    writer.Write("\"{0}\",", item.FundToSendAmount);
                    writer.Write("\"{0}\"\n", item.InvestorName);
                }
            }
            return String.Format("<a href='Report/{0}'>Right click and 'Save Target As' to get the CSV file</a>", targetFile);

        }

        private string Preview(string path, string start, string end, string batchno, int includeByte)
        {
            StringBuilder html = new StringBuilder();

            var list = m_Dao.GetData(start, end, batchno, includeByte);

            html.Append("<table>");

            #region Header
            html.Append("<thead>");
            html.Append("<tr>");

    
                html.Append("<td>Originator Loan Number</td>");
                html.Append("<td>Mortgagee Loan Name</td>");
                html.Append("<td>Originator Product Code</td>");
                html.Append("<td>Requested Date</td>");
                html.Append("<td>Note Amount</td>");
                html.Append("<td>Note Date</td>");
                html.Append("<td>Note Term</td>");
                html.Append("<td>Note Rate</td>");
                html.Append("<td>Borrower 1 First Name</td>");
                html.Append("<td>Borrower 1 Last Name</td>");
                html.Append("<td>Borrower 1 SSN</td>");
                html.Append("<td>Borrower 2 First Name</td>");
                html.Append("<td>Borrower 2 Last Name</td>");
                html.Append("<td>Borrower 2 SSN</td>");
                //html.Append("<td>Borrower 3 First Name</td>");
                //html.Append("<td>Borrower 3 Last Name</td>");
                //html.Append("<td>Borrower 3 SSN</td>");
                html.Append("<td>Address</td>");
                html.Append("<td>City</td>");
                html.Append("<td>U.S. State</td>");
                html.Append("<td>Zip</td>");
                html.Append("<td>County</td>");
                html.Append("<td>Closing Agent Primary Bank ABA Routing No</td>");
                html.Append("<td>Closing Agent Final Bank Account Number</td>");
                html.Append("<td>Closing Agent Order Number</td>");
                html.Append("<td>Amortization Type</td>");
                html.Append("<td>Documentation Type</td>");
                html.Append("<td>Loan Type</td>");
                html.Append("<td>MERS Number</td>");
                html.Append("<td>Lien Position</td>");
                html.Append("<td>Occupancy</td>");
                html.Append("<td>Property Type</td>");
                html.Append("<td>Rate Type</td>");
                html.Append("<td>Transaction Type</td>");
                html.Append("<td>Appraised Value</td>");
                html.Append("<td>Credit Score</td>");
                html.Append("<td>Warehouse Principal</td>");
                html.Append("<td>Funds to Send Amount</td>");
                html.Append("<td>Investor Name</td>");
            html.Append("</tr>");
            html.Append("</thead>");
            #endregion

            #region Body
            html.Append("<tbody>");

                foreach (var item in list)
                {
                    html.Append("<tr>");
                    html.AppendFormat("<td>{0}</td>", item.OriginatorLoanNumber);
                    html.AppendFormat("<td>{0}</td>", item.MortgageeLoanName);
                    html.AppendFormat("<td>{0}</td>", item.OriginatorProductCode);
                    html.AppendFormat("<td>{0}</td>", item.RequestedDate);
                    html.AppendFormat("<td>{0}</td>", item.NoteAmount);
                    html.AppendFormat("<td>{0}</td>", item.NoteDate);
                    html.AppendFormat("<td>{0}</td>", item.NoteTerm);
                    html.AppendFormat("<td>{0}</td>", item.NoteRate);
                    html.AppendFormat("<td>{0}</td>", item.Borrower1FirstName);
                    html.AppendFormat("<td>{0}</td>", item.Borrower1LastName);
                    html.AppendFormat("<td>{0}</td>", item.Borrower1SSN);
                    html.AppendFormat("<td>{0}</td>", item.Borrower2FirstName);
                    html.AppendFormat("<td>{0}</td>", item.Borrower2LastName);
                    html.AppendFormat("<td>{0}</td>", item.Borrower2SSN);
                    //html.AppendFormat("<td>{0}</td>", item.Borrower3FirstName);
                    //html.AppendFormat("<td>{0}</td>", item.Borrower3LastName);
                    //html.AppendFormat("<td>{0}</td>", item.Borrower3SSN);
                    html.AppendFormat("<td>{0}</td>", item.Address);
                    html.AppendFormat("<td>{0}</td>", item.City);
                    html.AppendFormat("<td>{0}</td>", item.USState);
                    html.AppendFormat("<td>{0}</td>", item.Zip);
                    html.AppendFormat("<td>{0}</td>", item.County);
                    html.AppendFormat("<td>{0}</td>", item.ClosingAgentPrimaryBankABARoutingNo);
                    html.AppendFormat("<td>{0}</td>", item.ClosingAgentFinalBankAccountNumber);
                    html.AppendFormat("<td>{0}</td>", item.ClosingAgentOrderNumber);
                    html.AppendFormat("<td>{0}</td>", item.AmortizationType);
                    html.AppendFormat("<td>{0}</td>", item.DocumentationType);
                    html.AppendFormat("<td>{0}</td>", item.LoanType);
                    html.AppendFormat("<td>{0}</td>", item.MERSNumber);
                    html.AppendFormat("<td>{0}</td>", item.LienPosition);
                    html.AppendFormat("<td>{0}</td>", item.Occupancy);
                    html.AppendFormat("<td>{0}</td>", item.PropertyType);
                    html.AppendFormat("<td>{0}</td>", item.RateType);
                    html.AppendFormat("<td>{0}</td>", item.TransactionType);
                    html.AppendFormat("<td>{0}</td>", item.AppraisedValue);
                    html.AppendFormat("<td>{0}</td>", item.CreditScore);
                    html.AppendFormat("<td>{0}</td>", item.WarehousePrincipal);
                    html.AppendFormat("<td>{0}</td>", item.FundToSendAmount);
                    html.AppendFormat("<td>{0}</td>", item.InvestorName);
                    html.Append("</tr>");
                }

            html.Append("</tbody>");

            #endregion

            html.Append("</table>");

            return html.ToString();

        }


    }
}
