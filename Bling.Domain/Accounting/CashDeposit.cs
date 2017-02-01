using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Accounting
{
    public class CashDeposit
    {
        public virtual int Id { get; set; }
        public virtual string AppNum { get; set; }
        public virtual string Branch { get; set; }
        public virtual string AccountNo { get; set; }
        public virtual DateTime InputDate { get; set; }
        public virtual decimal DollarAmount { get; set; }
        public virtual string BankAcct { get; set; }

    }
}
