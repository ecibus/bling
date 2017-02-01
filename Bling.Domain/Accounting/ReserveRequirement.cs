using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Accounting
{
    public class ReserveRequirement
    {
        public virtual int Id { get; set; }
        public virtual string CostCenter { get; set; }
        public virtual string Recipient { get; set; }
        public virtual decimal ? ReserveMinimum { get; set; }
        public virtual decimal ? FixedReserve { get; set; }
    }
}
