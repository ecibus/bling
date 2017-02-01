using System;
using Bling.Domain.Extension;

namespace Bling.Domain.Secondary
{
    public class MCMLockedLoan
    {
        public virtual string FileId { get; set; }
        public virtual string LoanNumber { get; set; }
        public virtual string BorrowerName { get; set; }
        public virtual string Program { get; set; }
        public virtual double LoanAmount { get; set; }
        public virtual double Rate { get; set; }
        public virtual double Discount { get; set; }
        public virtual string Status { get; set; }
        public virtual string Source { get; set; }
        public virtual double Margin { get; set; }
        public virtual double LifeCap { get; set; }
        public virtual string Purpose { get; set; }
        public virtual double LTV { get; set; }
        public virtual string LockDate { get; set; }
        public virtual string LockExpirationDate { get; set; }
        public virtual string Intent { get; set; }
        public virtual string PropertyType { get; set; }
        public virtual string State { get; set; }
        public virtual string Documentation { get; set; }
        public virtual double Fico { get; set; }
        public virtual double BuyDown { get; set; }
        public virtual double CLTV { get; set; }
        public virtual string Foreign { get; set; }
        public virtual string Impounds { get; set; }
        public virtual DateTime ApplicationDate { get; set; }
        public virtual double FrontRatio { get; set; }
        public virtual double BackRatio { get; set; }
        public virtual string MI { get; set; }
        public virtual string Branch { get; set; }
        public virtual string LoanOfficer { get; set; }
        public virtual string Broker { get; set; }
        public virtual string BuySideInvestor { get; set; }

        public virtual double TotalAdjustment { get; set; } //net_branch
        public virtual double BranchPriceAdjustment { get; set; } //branch_price_adj
        public virtual bool IsMarketingGain { get; set; }

        public static string Header()
        {
            return "loan_no,borrower_name,program,loan_amt,rate,discount,status,source,margin,life_cap,purpose,ltv,lock_date,lock_exp_date,intent,property_type,state,documentation,fico,buydown,cltv,foreign,impounds,app_date,front_ratio,back_ratio,mi,branch,loan_officer,broker,buyside_investor";
        }

        public override string ToString()
        {
            return String.Format(
                "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}," +
                "{10},{11},{12},{13},{14},{15},{16},{17},{18},{19}," +
                "{20},{21},{22},{23},{24},{25},{26},{27},{28},{29}," +
                "{30}",
                LoanNumber.R(), BorrowerName.R(), Program.R(), LoanAmount, Rate,
                IsMarketingGain ? Discount - BranchPriceAdjustment : Discount - TotalAdjustment,

                Status == null ? "NULL" : Status.Trim().R(), Source.R(), Margin, LifeCap,
                Purpose.R(), LTV, LockDate, LockExpirationDate, Intent.R(), PropertyType == null ? "NULL" : PropertyType.R(), State.R(), Documentation == null ? "NULL" : Documentation.R(), Fico, BuyDown,
                CLTV, Foreign.R(), Impounds.R(), ApplicationDate.ToShortDateString(), FrontRatio, BackRatio, MI.R(), Branch, LoanOfficer.R(), Broker.R(),
                BuySideInvestor
                );
        }
    }
}
