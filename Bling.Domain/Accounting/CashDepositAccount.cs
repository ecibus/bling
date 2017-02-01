using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Accounting
{
    public class CashDepositAccount
    {
        public virtual string AccountNo { get; set; }
        public virtual string AccountDescription { get; set; }
    }
}
