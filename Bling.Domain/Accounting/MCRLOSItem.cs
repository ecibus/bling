using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Accounting
{
    public class MCRLOSItem
    {
        public virtual string EmployeeId { get; set; }
        public virtual string NMLSId { get; set; }
        public virtual int NoOfLoans { get; set; }
        public virtual int LoanAmount { get; set; }
    }
}
