using System;
using System.Collections.Generic;

namespace Bling.Domain.Underwriting
{
    public class ScoreCardLoanInfo
    {
        public virtual string LoanNumber { get; set; }
        public virtual string Borrower { get; set; }
        public virtual string Underwriter { get; set; }
        public virtual string LoanOfficer { get; set; }
        public virtual string Processor { get; set; }
        public virtual string FileId { get; set; }        
        public virtual bool Is203K { get; set; }
        public virtual bool IsPerfect { get; set; }        
        public virtual IList<int> ScoreIds { get; set; }
        
    }
}
