using System;

namespace Bling.Domain.Accounting
{
    public class TrustAccountBackup
    {
        public virtual int Id { get; set; }
        public virtual string ApplicationNumber { get; set; }
        public virtual string Category { get; set; }
        public virtual string Type { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual DateTime ActivityDate { get; set; }
        public virtual double Amount { get; set; }
        public virtual string Notes { get; set; }
        public virtual string CreatedBy { get; set; }
            
    }
}
