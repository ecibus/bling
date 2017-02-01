using System;
using Bling.Domain.Extension;

namespace Bling.Domain.Secondary
{
    public class MCMFallOutLoan
    {
        public virtual string FileId { get; set; }
        public virtual string LoanNumber { get; set; }
        public virtual double LoanAmount { get; set; }
        public virtual string OrigProgram { get; set; }
        public virtual double OrigRate { get; set; }
        public virtual double OrigDiscount { get; set; }
        public virtual double OrigMargin { get; set; }
        public virtual double OrigLifeCap { get; set; }
        public virtual string OrigLockDate { get; set; }
        public virtual string OrigLockExpirationDate { get; set; }
        public virtual string Program { get; set; }
        public virtual double Rate { get; set; }
        public virtual double Discount { get; set; }
        public virtual double Margin { get; set; }
        public virtual double LifeCap { get; set; }
        public virtual string LockDate { get; set; }
        public virtual string LockExpirationDate { get; set; }
        public virtual string Purpose { get; set; }
        public virtual double LTV { get; set; }
        public virtual string Intent { get; set; }
        public virtual string Status { get; set; }
        public virtual string Source { get; set; }
        public virtual string PropertyType { get; set; }
        public virtual string State { get; set; }
        public virtual string Documentation { get; set; }
        public virtual double Fico { get; set; }
        public virtual double BuyDown { get; set; }
        public virtual string FirstTimeHomeBuyer { get; set; }
        public virtual double CLTV { get; set; }
        public virtual string ApplicationDate { get; set; }
        public virtual string CancelDate { get; set; }
        public virtual string RejectDate { get; set; }
        public virtual string CloseDate { get; set; }
        public virtual string Branch { get; set; }
        public virtual string ApprovalDate { get; set; }
        public virtual string DocsOutDate { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string LoanOfficer { get; set; }
        public virtual string Broker { get; set; }

        public virtual double TotalAdjustment { get; set; } //net_branch
        public virtual double BranchPriceAdjustment { get; set; } //branch_price_adj
        public virtual bool IsMarketingGain { get; set; }

        public static string Header()
        {
            return "loan_no,loan_amt,orig_program,orate,odiscount,omargin,olifecap,olock_date,olock_exp_date,program,rate,discount,margin,life_cap,lock_date,lock_exp_date,purpose,ltv,intent,status,source,property_type,state,documentation,fico,buydown,first_time_hb,cltv,app_date,cancel_date,reject_date,close_date,branch_id,approval_date,doc_date,sub_date,loan_officer,broker";
        }

        public override string ToString()
        {
            return String.Format(
                "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}," +
                "{10},{11},{12},{13},{14},{15},{16},{17},{18},{19}," +
                "{20},{21},{22},{23},{24},{25},{26},{27},{28},{29}," +
                "{30},{31},{32},{33},{34},{35},{36},{37}"
                ,
                LoanNumber.R(), LoanAmount, OrigProgram.R(), OrigRate,
                IsMarketingGain ? OrigDiscount - BranchPriceAdjustment : OrigDiscount - TotalAdjustment,

                OrigMargin, OrigLifeCap, OrigLockDate, OrigLockExpirationDate,
                Program.R(), Rate,
                IsMarketingGain ? Discount - BranchPriceAdjustment : Discount - TotalAdjustment,
                Margin, LifeCap, LockDate, LockExpirationDate,
                Purpose.R(), LTV, Intent, Status == null ? "NULL" : Status.Trim().R(), Source.R(),
                PropertyType == null ? "NULL" : PropertyType.R(), State.R(), Documentation == null ? "NULL" : Documentation.R(), Fico, BuyDown,
                FirstTimeHomeBuyer.R(), CLTV, ApplicationDate,
                CancelDate,
                RejectDate,
                CloseDate,
                Branch.R(),
                ApprovalDate,
                DocsOutDate,
                SubmittedDate,
                LoanOfficer.R(), Broker.R()
                );
        }

    }
}
