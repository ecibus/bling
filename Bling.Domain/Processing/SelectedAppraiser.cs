using System;

namespace Bling.Domain.Processing
{
    public class SelectedAppraiser
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual string BranchNo { get; set; }
        public virtual string LoanNumber { get; set; }
        public virtual string AppraiserId { get; set; }
        public virtual string AddedBy { get; set; }
    }
}
