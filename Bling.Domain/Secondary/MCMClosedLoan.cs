using System;
using Bling.Domain.Extension;

namespace Bling.Domain.Secondary
{
    public class MCMClosedLoan
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
        public virtual DateTime LockDate { get; set; }
        public virtual DateTime CloseDate { get; set; }
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

        public virtual double TotalAdjustment { get; set; } //net_branch
        public virtual double BranchPriceAdjustment { get; set; } //branch_price_adj
        public virtual bool IsMarketingGain { get; set; }

        public virtual string HPML { get; set; }
        public virtual string HPMLResult { get; set; }
        public virtual string WarehouseBank { get; set; }
        public virtual string Exception { get; set; }
        public virtual string ExceptionType { get; set; }
        public virtual string AUSType { get; set; }

        public virtual string AmmortTerm { get; set; }
        public virtual string BaseLnAmount { get; set; }
        public virtual string MIP { get; set; }
        public virtual string Addr { get; set; }
        public virtual string City { get; set; }
        public virtual string Zip { get; set; }
        public virtual string County { get; set; }
        public virtual string Income { get; set; }
        public virtual string Units { get; set; }
        public virtual string UWInvestor { get; set; }
        public virtual string SellInvestor { get; set; }
        public virtual string BuyPricedInvestor { get; set; }

        public static string Header()
        {
              return "loan_no,borrower_name,program,loan_amt,rate,discount,status,source,margin,life_cap,purpose,ltv,lock_date,close_date,intent,property_type,state,documentation,fico,buydown,cltv,foreign,impounds,app_date,front_ratio,back_ratio,mi,branch,loan_officer,broker,Ammort term,Base Ln. Amt.,MIP,aus_type,Address,City,Zip,County,Income,Units,UW_Investor,Sell_Investor,hpml,hpml_result,warehouse_bank,exception,exception_type,Buy_Priced_Investor";
            //return "loan_no,borrower_name,program,loan_amt,rate,discount,status,source,margin,life_cap,purpose,ltv,lock_date,close_date,intent,property_type,state,documentation,fico,buydown,cltv,foreign,impounds,app_date,front_ratio,back_ratio,mi,branch,loan_officer,broker,hpml,hpml_result,warehouse_bank,exception,exception_type,aus_type";
        }

        public override string ToString()
        {
            var lockDate = LockDate.ToShortDateString();
            lockDate = lockDate == "1/1/0001" ? "NULL" : lockDate;

            var closeDate = CloseDate.ToShortDateString();
            closeDate = closeDate == "1/1/0001" ? "NULL" : closeDate;

            /*
            return 
              "loan_no,borrower_name,program,loan_amt,rate,discount,status,source,margin,life_cap,
             * purpose,ltv,lock_date,close_date,intent,property_type,state,documentation,fico,buydown,
             * cltv,foreign,impounds,app_date,front_ratio,back_ratio,mi,branch,loan_officer,broker,
             * Ammort term,Base Ln. Amt.,MIP,aus_type,Address,City,Zip,County,Income,Units,
             * hpml,hpml_result,warehouse_bank,exception,exception_type";
            */
            return String.Format(
                "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}," +
                "{10},{11},{12},{13},{14},{15},{16},{17},{18},{19}," +
                "{20},{21},{22},{23},{24},{25},{26},{27},{28},{29}," +
                "{30},{31},{32},{33},{34},{35},{36},{37},{38},{39}," +
                "{40},{41},{42},{43},{44},{45},{46},{47}"
                ,
                //Discount, IsMarketingGain, NetBranchPrice, BranchPriceAdjustment,
                LoanNumber.R(), BorrowerName.R(), Program.R(), LoanAmount, Rate,
                IsMarketingGain ? Discount - BranchPriceAdjustment : Discount - TotalAdjustment,
                Status == null ? "NULL" : Status.Trim().R(), Source.R(), Margin, LifeCap,

                Purpose.R(), LTV, lockDate, closeDate, Intent.R(), PropertyType == null ? "NULL" : PropertyType.R(),
                State.R(), Documentation == null ? "NULL" : Documentation.R(), Fico, BuyDown,

                CLTV, Foreign.R(), Impounds.R(), ApplicationDate.ToShortDateString(), FrontRatio, BackRatio, MI.R(),
                Branch, LoanOfficer.R(), Broker.R(),

                AmmortTerm, BaseLnAmount, MIP, AUSType.R(), Addr.R(), City.R(), Zip.R(), County.R(), Income, Units,
                UWInvestor.R(), SellInvestor.R(),
                HPML.R(), HPMLResult.R(), WarehouseBank.R(), Exception.R(), ExceptionType.R(), BuyPricedInvestor.R()
                );


        }
    }
}
