using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Extension;

namespace Bling.Domain.Compliance
{
    public class DIRWLoanInfo
    {
        public virtual string FileId { get; set; }
        public virtual string LoanNumber { get; set; }
        public virtual string Borrower { get; set; }
        public virtual string Status { get; set; }
        public virtual string LoanProgram { get; set; }
        public virtual string Underwriter { get; set; }
        public virtual string Funder { get; set; }
        public virtual string Processor { get; set; }
        public virtual string Reviewer { get; set; }
        public virtual string DateReviewed { get; set; }
        public virtual string FundedDate { get; set; }
        public virtual string State { get; set; }
        public virtual string DeniedDate { get; set; }
        public virtual string CancelledDate { get; set; }

        public virtual string ToJson()
        {
            StringBuilder json = new StringBuilder();
            json.Append("{ ");
            json.AppendFormat("FileId : '{0}', ", FileId.Escape());
            json.AppendFormat("LoanNumber : '{0}', ", LoanNumber);
            json.AppendFormat("Borrower : \"{0}\", ", Borrower.Escape());
            json.AppendFormat("DateReviewed : '{0}', ", DateReviewed);
            json.AppendFormat("Funder : \"{0}\", ", Funder.Escape());
            json.AppendFormat("LoanProgram : '{0}', ", LoanProgram);
            json.AppendFormat("Processor : \"{0}\", ", Processor.Escape());
            json.AppendFormat("ReviewType : \"{0}\", ", ReviewType());
            json.AppendFormat("Reviewer : \"{0}\", ", Reviewer.Escape());
            json.AppendFormat("Status : '{0}', ", Status);
            json.AppendFormat("State : '{0}', ", State);
            json.AppendFormat("Underwriter : \"{0}\" ", Underwriter.Escape());
            json.Append("}");
            return json.ToString();
        }

        public virtual string ReviewType()
        {
            if (!String.IsNullOrEmpty(FundedDate))
            {
                return "Closed Loan";
            }

            if (!String.IsNullOrEmpty(DeniedDate))
            {
                return "Denied Loan";
            }

            if (!String.IsNullOrEmpty(CancelledDate))
            {
                return "Cancelled Loan";
            }

            return "";
        }
    }
}
