using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.HR
{
    public class CommissionAnalysis
    {
        public virtual string LoanNumber { get; set; }
        public virtual string Borrower { get; set; }
        public virtual string LoanOfficer { get; set; }
        public virtual string ApprovedLO { get; set; }
        public virtual decimal LoanAmount { get; set; }
        public virtual string ApplicationDate { get; set; }
        public virtual string FundedDate { get; set; }
        public virtual string CommissionStatus { get; set; }
        public virtual string CommissionStatusDate { get; set; }
        public virtual string Comment { get; set; }
        public virtual string LoginName { get; set; }
        public virtual string ReleasedDate { get; set; }
        public virtual string HoldDate { get; set; }
        public virtual string BrokeredLoan { get; set; }
        public virtual decimal? Commission { get; set; }

        public virtual string ToRow()
        {
            return String.Format("<tr><td><a href='CommissionAnalysisForm.aspx?ln={0}'>{0}</a></td><td>{1}</td><td>{2}</td><td class='number'>{3:0,0.00}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td class='paydate'>{9}</td></tr>",
                LoanNumber, Borrower, LoanOfficer, LoanAmount, ApplicationDate, FundedDate, HoldDate, ReleasedDate, CommissionStatus, CommissionStatusDate);
        }

        public static string ToHTMLTable(IList<CommissionAnalysis> list)
        {
            StringBuilder table = new StringBuilder();

            table.Append("<table class='t1'>");
            table.Append(String.Format("<tr class='yellow'><td>{0}</td><td>{1}</td><td>{2}</td><td class='number'>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td></tr>",
                "Loan Number", "Borrower", "Loan Officer", "Loan Amount", "Application Date", "Funded Date", "Hold Date", "Released Date", "Status", "Pay Date"));

            list.ToList().ForEach(x => table.Append(x.ToRow()));

            table.Append(String.Format("<tr class='total'><td colspan='3'>No of Loans: {0}</td></tr>", list.Count));
            table.Append("</table>");

            return table.ToString();
        }

        public virtual string ToJson()
        {
            StringBuilder json = new StringBuilder();

            json.AppendFormat(" {{ ");

            json.AppendFormat(" \"LoanNumber\" : \"{0}\", ", LoanNumber);
            json.AppendFormat(" \"Borrower\" : \"{0}\", ", Borrower);
            json.AppendFormat(" \"LoanOfficer\" : \"{0}\", ", LoanOfficer);
            json.AppendFormat(" \"LoginName\" : \"{0}\", ", LoginName);
            json.AppendFormat(" \"ApprovedLO\" : \"{0}\", ", ApprovedLO);
            json.AppendFormat(" \"LoanAmount\" : \"{0:0,0.00}\", ", LoanAmount);
            json.AppendFormat(" \"ApplicationDate\" : \"{0}\", ", ApplicationDate);
            json.AppendFormat(" \"FundedDate\" : \"{0}\", ", FundedDate);
            json.AppendFormat(" \"HoldDate\" : \"{0}\", ", HoldDate);
            json.AppendFormat(" \"ReleasedDate\" : \"{0}\", ", ReleasedDate);
            json.AppendFormat(" \"CommissionStatus\" : \"{0}\", ", CommissionStatus);
            json.AppendFormat(" \"CommissionStatusDate\" : \"{0}\", ", CommissionStatusDate);
            json.AppendFormat(" \"BrokeredLoan\" : \"{0}\", ", BrokeredLoan);
            json.AppendFormat(" \"Commission\" : \"{0:0,0.00}\", ", Commission);
            json.AppendFormat(" \"Comment\" : \"{0}\" ", Comment);


            json.AppendFormat(" }} ");

            return json.ToString();
        }


    }
}
