using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Extension;

namespace Bling.Domain.Compliance
{
    public class AuditScoreCardLoanInfo
    {
        public virtual string LoanNumber { get; set; }
        public virtual string LinkedLoanNumber { get; set; }
        public virtual string Borrower { get; set; }
        public virtual string LORep { get; set; }
        public virtual string Processor { get; set; }
        public virtual string Status { get; set; }
        public virtual string Program { get; set; }
        public virtual string LoanAmount { get; set; }
        public virtual string InterestRate { get; set; }
        public virtual string Locked { get; set; }
        public virtual string Expires { get; set; }
        public virtual string Days { get; set; }
        public virtual string InitialAuditor { get; set; }
        public virtual string AuditDate { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string FileId { get; set; }
        public virtual string InitialAuditorValue { get; set; }
        public virtual IList<int> ScoreIds { get; set; }

        public virtual string ToJson(string jsonSubTotal, string jsonNoFindings, string jsonOtherScore, string jsonComment, string jsonItemType)
        {
            StringBuilder json = new StringBuilder();

            StringBuilder scoreIds = new StringBuilder();
            scoreIds.Append("[ ");
            ScoreIds.ToList()
                .ForEach(i => scoreIds.AppendFormat("'{0}',", i));
            if (ScoreIds.Count > 0)
                scoreIds.Remove(scoreIds.Length - 1, 1);
            scoreIds.Append(" ]");

            json.AppendFormat(" {{ ");

            json.AppendFormat(" \"FileId\" : \"{0}\", ", FileId.Escape());
            json.AppendFormat(" \"LoanNumber\" : \"{0}\", ", LoanNumber);
            json.AppendFormat(" \"LinkedLoanNumber\" : \"{0}\", ", LinkedLoanNumber);
            json.AppendFormat(" \"Borrower\" : \"{0}\", ", Borrower);
            json.AppendFormat(" \"LORep\" : \"{0}\", ", LORep);
            json.AppendFormat(" \"Processor\" : \"{0}\", ", Processor);
            json.AppendFormat(" \"Status\" : \"{0}\", ", Status);
            json.AppendFormat(" \"Program\" : \"{0}\", ", Program);
            json.AppendFormat(" \"LoanAmount\" : \"{0:0,0.00}\", ", Convert.ToDouble(LoanAmount));
            json.AppendFormat(" \"InterestRate\" : \"{0:0.0000}\", ", Convert.ToDouble(InterestRate));
            json.AppendFormat(" \"Locked\" : \"{0}\", ", Locked ?? "&nbsp;");
            json.AppendFormat(" \"Expires\" : \"{0}\", ", Expires);
            json.AppendFormat(" \"Days\" : \"{0}\", ", Days);
            json.AppendFormat(" \"InitialAuditor\" : \"{0}\", ", InitialAuditor);
            json.AppendFormat(" \"AuditDate\" : \"{0}\", ", AuditDate);
            json.AppendFormat(" \"SubmittedDate\" : \"{0}\", ", SubmittedDate);
            json.AppendFormat(" \"ScoreIds\" : {0}, ", scoreIds.ToString());
            json.AppendFormat(" \"SubTotal\" : {0}, ", jsonSubTotal);
            json.AppendFormat(" \"NoFindings\" : {0}, ", jsonNoFindings);
            json.AppendFormat(" \"Other\" : {0}, ", jsonOtherScore);
            json.AppendFormat(" \"Comments\" : {0}, ", jsonComment);
            json.AppendFormat(" \"ItemTypes\" : {0} ", jsonItemType);

            json.AppendFormat(" }} ");

            return json.ToString();
        }

    }
}
