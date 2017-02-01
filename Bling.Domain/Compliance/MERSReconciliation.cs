using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Compliance
{
    public class MERSReconciliation
    {
        public virtual string LoanNumber { get; set; }
        public virtual string PurchasedDate { get; set; }
        public virtual string MersNo { get; set; }
    }
}
