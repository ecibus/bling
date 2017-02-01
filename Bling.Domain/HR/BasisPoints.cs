using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.HR
{
    public class BasisPoints
    {
        public virtual int Id { get; set; }        
        public virtual DateTime CreatedOn {get; set;}
        //public virtual string CreatedBy { get; set; }
        //public virtual string EmployeeId { get; set; }
        public virtual DateTime EffectiveDate { get; set; }
        public virtual decimal BaseCommission { get; set; }
        public virtual decimal Minimum { get; set; }
        public virtual decimal Maximum { get; set; }
        public virtual decimal Tier1 { get; set; }
        public virtual decimal Tier2 { get; set; }
        public virtual decimal Tier3 { get; set; }
        public virtual decimal Tier4 { get; set; }
        public virtual decimal Tier5 { get; set; }
        public virtual decimal Tier6 { get; set; }
        public virtual decimal BranchOverride { get; set; }
        public virtual decimal BrokeredLoans { get; set; }
        public virtual bool InsideSalesRep { get; set; }
        public virtual bool Manager { get; set; }
        public virtual bool Weekly { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual string DisabledBy { get; set; }
        public virtual DateTime ? DisabledOn { get; set; }

        public virtual UserInfo LoanOfficer { get; set; }
        public virtual UserInfo CreatedBy { get; set; }
        public virtual Broker Broker { get; set; }
    }
}
