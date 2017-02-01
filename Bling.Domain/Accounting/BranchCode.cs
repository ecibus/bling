using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Accounting
{
    public class BranchCode
    {
        public virtual int Code { get; set; }
        public virtual string BranchName { get; set; }
        public virtual bool MarketingGain { get; set; }
        public virtual int MarketingGainBranchCode { get; set; }
        public virtual bool Active { get; set; }
    }
}
