using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.HR
{
    public class LOCommission
    {
        public virtual int Id { get; set; }
        public virtual string EmployId { get; set; }
        public virtual string LoanNumber { get; set; }
        public virtual DateTime ApplicationDate { get; set; }
        public virtual DateTime FundedDate { get; set; }
        public virtual Decimal LoanAmount { get; set; }
        public virtual bool IsBrokeredLoan { get; set; }
        public virtual DateTime ? PayDate { get; set; }
        public virtual Decimal Volume { get; set; }
        public virtual int BasisPointId { get; set; }
        public virtual int TierId { get; set; }
        public virtual Decimal Rate { get; set; }                
        public virtual Decimal Commission { get; set; }
        public virtual bool ReCompute { get; set; }
        public virtual bool IsManager { get; set; }
        public virtual bool IsOverride { get; set; }
        public virtual bool IsWeekly { get; set; }
    }
}
