using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Bling.Domain.CustomerService;
using Bling.Repository.CustomerService;

namespace Bling.Presenter.CustomerService
{
    public interface ISecurityConnectionView
    {
        string SourceFileName { get; }
        string SecurityConnectionData { set; }
    }

    public class SecurityConnectionPresenter : Presenter
    {
        ISecurityConnectionView m_view;
        ISecurityConnectionDao m_dao;

        private List<SecurityConnectionShipDateInfo> m_list;

        public SecurityConnectionPresenter(ISecurityConnectionView view) 
            : this(view, new SecurityConnectionDao(DMDDataSession()))
        {
            m_view = view;
        }

        public SecurityConnectionPresenter(ISecurityConnectionView view, ISecurityConnectionDao dao)
        {
            m_view = view;
            m_dao = dao;
        }

        public List<SecurityConnectionShipDateInfo> LoadData()
        {
            m_list = new List<SecurityConnectionShipDateInfo>();
            using (TextReader reader = File.OpenText(m_view.SourceFileName))
            {
                string header = reader.ReadLine();
                //if (header != "Project	Client Reference Number	Subclient	Loan Funding Date	Loan Unfunding Date	Cancellation Effective Date	Borrower Last Name	Investor Name	Investor Loan Identifier	Commitment Reference Identifier	Loan Sale Funding Date	Investor Code Identifier	Doc Type	Received	Shipped	Tracking #	Ship Batch Name	Destination	Attention	Ship Address")
                if (header != "Project	Client Reference Number	Subclient	Loan Funding Date	Cancellation Effective Date	Borrower Last Name	Investor Name	Investor Loan Identifier	Commitment Reference Identifier	Loan Sale Funding Date	Investor Code Identifier	Doc Type	Received	Shipped	Tracking #	Ship Batch Name	Destination	Attention	Ship Address")                    
                {
                    throw new ApplicationException("The format of the file you are trying to upload is not valid.");
                }

                StringBuilder sb = new StringBuilder("<table class='t1'>");

                sb.AppendFormat("<tr class='yellow'><td>Loan Number</td><td>Document Type</td><td>Shipped Date</td></tr>");
                while (reader.Peek() != -1)
                {
                    SecurityConnectionShipDateInfo sc = new SecurityConnectionShipDateInfo(reader.ReadLine());

                    if (sc.LoanNumber == "" || sc.DocumentType == "" || sc.ShippedDate == "")
                        continue;

                    m_list.Add(sc);

                    sb.Append(sc.ToString());
                }
                reader.Close();

                sb.Append("</table>");
                m_view.SecurityConnectionData = sb.ToString();
            }
            return m_list;
        }

        public void SaveData(List<SecurityConnectionShipDateInfo> data)
        {
            data.ForEach(sc => m_dao.UpdateShippedDateInDataTrac(sc));
        }
    }    
}
