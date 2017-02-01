using System;
using System.Text;
using Bling.Domain;
using Bling.Domain.Secondary;
using Bling.Domain.Extension;

namespace Bling.Domain
{
    public class Gen
    {
        public virtual string FileId { get; set; }
        public virtual string LoanNumber { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual decimal LoanAmount { get; set; }
        public virtual Program Program { get; set; }
        public virtual string Stage { get; set; }
        public virtual UserInfo LoanOfficer { get; set; }
        public virtual DateTime LockExpiration { get; set; }
        public virtual LoanLockDetail GEMLock { get; set; }

        public virtual string ToHtml()
        {
            StringBuilder html = new StringBuilder();

            html.AppendFormat("<table>");
            html.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", "Loan Number", LoanNumber);
            html.AppendFormat("<tr><td>{0}</td><td>{1} {2}</td></tr>", "Borrower", FirstName, LastName);
            html.AppendFormat("<tr><td>{0}</td><td>{1:c}</td></tr>", "Loan Amount", LoanAmount);
            html.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", "Program", Program.ProgramName);
            html.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", "Stage", Stage);
            html.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", "Lock Expiration", LockExpiration.ToShortDateString());
            html.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", "Loan Officer", LoanOfficer.FullName);
            html.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", "Current GEMLock Investor", GEMLock.Investor);
            html.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", "Current GEMLock Description", GEMLock.Description);
            html.AppendFormat("</table>");


            return html.ToString();
        }

        public virtual string ToJson()
        {
            StringBuilder json = new StringBuilder();
            json.Append("data = { ");
            json.AppendFormat("\"LoanNumber\" : \"{0}\", ", LoanNumber);
            json.AppendFormat("\"Borrower\" : \"{0} {1}\", ", FirstName.Capitalize(), LastName.Capitalize());
            json.AppendFormat("\"LoanAmount\" : \"{0:c}\", ", LoanAmount);
            json.AppendFormat("\"Program\" : \"{0}\", ", Program.ProgramName);
            json.AppendFormat("\"ProgramId\" : \"{0}\", ", Program.Id);
            json.AppendFormat("\"LoanType\" : \"{0}\", ", Program.LoanType);            
            json.AppendFormat("\"LockExpiration\" : \"{0}\", ", LockExpiration.ToShortDateString());
            json.AppendFormat("\"LoanOfficer\" : \"{0}\", ", LoanOfficer.FullName);
            if (GEMLock != null)
            {
                json.AppendFormat("\"Investor\" : \"{0}\", ", GEMLock.Investor);
                json.AppendFormat("\"Description\" : \"{0}\", ", GEMLock.Description);
            }
            json.AppendFormat("\"Stage\" : \"{0}\" ", Stage);
            
            json.Append(" }");

            return json.ToString();
        }
    }

}
