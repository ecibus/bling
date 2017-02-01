using System;

namespace Bling.Domain.Accounting
{
    public class RankingBranch : IBranchId  
    {
        public virtual int Id { get; set; }
        public virtual string BranchId { get; set; }
        public virtual string CreatedBy { get; set; }        
    }
}
