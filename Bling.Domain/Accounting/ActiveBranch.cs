using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Accounting
{
    public class ActiveBranch
    {
        public virtual int Id { get; set; }
        public virtual string MonthEnd { get; set; }
        public virtual int CurrentMonth { get; set; }
        public virtual int CurrentMonthMinus1 { get; set; }
        public virtual int CurrentMonthMinus2 { get; set; }
        public virtual int FYTD { get; set; }
    }
}
