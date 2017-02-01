using System;

namespace Bling.Domain.Secondary
{
    public class LoanLockDetail
    {
        public virtual string FileId { get; set; }
        public virtual string Investor { get; set; }
        public virtual string Description { get; set; }        
    }
}
