using System;

namespace Bling.Domain.Processing
{
    public class OrderAppraisal
    {
        public virtual string LoanNumber { get; set; }
        public virtual string Appraiser { get; set; }
        public virtual string TicketNo { get; set; }
        public virtual string OrderedBy { get; set; }
        public virtual string OrderedDate { get; set; }
    }
}
