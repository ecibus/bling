using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Extension;

namespace Bling.Domain.Accounting
{
    public class MWE3Data
    {
        public virtual string ApplicationNumber { get; set; }
        public virtual string LoanNumber { get; set; }
        public virtual string DMILoanNumber { get; set; }
        public virtual DateTime ? ClosedDate { get; set; }
        public virtual string LoanOfficer { get; set; }
        public virtual decimal ? AdjustedLoanAmount { get; set; }
        public virtual float ? InterestRate { get; set; }
        public virtual int ? LoanTerm { get; set; }
        public virtual bool InterestOnly { get; set; }
        public virtual string BorrowerFirstName { get; set; }
        public virtual string BorrowerLastName { get; set; }
        public virtual string BranchName { get; set; }
        public virtual string LoanProgramDescription { get; set; }
        public virtual string InvestorName { get; set; }
        public virtual DateTime ? CSPurchaseDate { get; set; }
        public virtual float ? BranchPrice { get; set; }
        public virtual float ? BranchSRP { get; set; }
        public virtual float ? LOPrice { get; set; }
        public virtual float ? CommitSRP { get; set; }
        public virtual decimal ? H5125_SHB { get; set; }
        public virtual decimal ? UnpaidPrincipalBalance { get; set; }
        public virtual DateTime ? LockInDate { get; set; }
        public virtual decimal ? H827_ReverseMortgage { get; set; }
        public virtual decimal ? Origination { get; set; }
        public virtual DateTime ? PurchasedDate { get; set; }
        public virtual DateTime ? PaidoffDate { get; set; }
        public virtual decimal ? H803_AppraisalFee { get; set; }
        public virtual decimal ? H1304_ReinspectionFee { get; set; }
        public virtual decimal ? H208_AppraisalCredit { get; set; }
        public virtual string Channel { get; set; }
        public virtual decimal ? FloodFee { get; set; }
        public virtual decimal ? H804_CreditReportFee { get; set; }
        public virtual decimal ? H209_CreditReportCredit { get; set; }
        public virtual DateTime ? FirstPaymentDate { get; set; }
        public virtual string DBSource { get; set; }
        public virtual decimal ? H902_MI { get; set; }
        public virtual decimal ? H809_VAFundingFee { get; set; }
        public virtual decimal ? H213_MICredit { get; set; }
        public virtual decimal ? InitPMI { get; set; }
        public virtual decimal ? ImpoundsAtClose { get; set; }
        public virtual decimal ? HelocDrawFee { get; set; }
        public virtual decimal ? H1001_HazardInsurance { get; set; }
        public virtual decimal ? H1002_MI { get; set; }
        public virtual decimal ? H1004_CountyTax { get; set; }
        public virtual decimal ? H1006_FloodInsurance { get; set; }
        public virtual decimal ? H1007_Other { get; set; }
        public virtual decimal ? H818_EEM { get; set; }
        public virtual decimal ? H816_DocFee { get; set; }
        public virtual decimal ? H817_WireFee { get; set; }
        public virtual decimal ? H815_TaxServiceFee { get; set; }
        public virtual decimal ? H813_ProcessingFee { get; set; }
        public virtual decimal ? H814_UnderwritingFee { get; set; }
        public virtual int ? OddDaysInterest { get; set; }
        public virtual decimal ? H901_DaysInterest { get; set; }
        public virtual int ? PerDiemDays { get; set; }
        public virtual decimal? H810_BuyDown { get; set; }
        public virtual string WarehouseLine { get; set; }
        public virtual decimal ? H1305_MiscFee { get; set; }
        public virtual decimal ? H808_BrokerOrigFee { get; set; }
        public virtual decimal ? H805_AdminFee { get; set; }
        public virtual decimal ? H1113_ClosingFee { get; set; }
        public virtual decimal ? InvestorWireAmount { get; set; }
        public virtual decimal ? DiscountOverage { get; set; }
        public virtual decimal ? InvestorBuyPrice { get; set; }
        public virtual decimal ? MarketDiscount { get; set; }
        public virtual decimal ? AdjustedNoteAmount { get; set; }
        public virtual decimal ? SecSRP { get; set; }
        public virtual string LoanProgramCode { get; set; }
        public virtual decimal? H1303_WarehouseFee { get; set; }
        public virtual decimal ? H805_LenderInspectionFee { get; set; }
        
                                                      
        public virtual string BorrowerLastFirstName  
        {
            get { return String.Format("{0}, {1}", BorrowerLastName, BorrowerFirstName); }
        }

        public virtual DateTime ? PurchasedPaidoffDate 
        { 
            get
            {
                if (PurchasedDate.HasValue) return PurchasedDate;
                return PaidoffDate;
            }
        }
        
        public virtual decimal ? AppraisalFee
        {
            get
            {
                return H803_AppraisalFee.ToValue() + H1304_ReinspectionFee.ToValue() - H208_AppraisalCredit.ToValue()
                    + H805_LenderInspectionFee.ToValue();
            }
        }

        public virtual decimal ? RFCPackageWireFee
        {
            get
            {
                if (Channel.ToLower() == "brokered")
                    return 0;

                if (ClosedDate.HasValue)
                    if (ClosedDate.Value > Convert.ToDateTime("10/1/2007"))
                        return 75;

                return 50;
            }
        }

        public virtual decimal ? CreditReport
        {
            get { return H804_CreditReportFee.ToValue() - H209_CreditReportCredit.ToValue(); }
        }

        public virtual decimal ? PMI_MIP_VAFF
        {
            get
            {
                if (Channel.ToLower() == "brokered")
                    return 0;

                if (DBSource.ToLower() == "m")
                    return H902_MI.ToValue() + H809_VAFundingFee.ToValue() - H213_MICredit.ToValue();

                return InitPMI.ToValue();
            }
        }

        public virtual decimal ? TaxServiceFee
        {
            get
            {
                if (Channel.ToLower() == "brokered")
                    return 0; ;
                return H815_TaxServiceFee.ToValue();
            }
        }
        
        public virtual decimal ? PrincipalReduction
        {
            get
            {
                if (UnpaidPrincipalBalance.ToValue() == 0)
                    return 0;
                return (AdjustedLoanAmount.ToValue() - UnpaidPrincipalBalance.ToValue()) * -1;
            }
        }

        public virtual decimal ? UnderwritingFee
        {
            get
            {
                if (Channel.ToLower() == "brokered")
                    return 0; 
                return H814_UnderwritingFee.ToValue();
            }
        }
        
        public virtual string WarehouseBank
        {
            get
            {
                string whline = WarehouseLine.ToUpper();
                if (ClosedDate.HasValue && ClosedDate.Value < Convert.ToDateTime("4/1/2006") && whline == "BOFA")
                    return "RFC";

                if (whline == "W")
                    return "WAMU";

                if (whline.StartsWith("RFC"))
                    return "RFC";

                return whline;
            }
        }

        public virtual decimal MiscIncome
        {
            get
            {
                return H1305_MiscFee.ToValue() + H808_BrokerOrigFee.ToValue() +
                    H805_AdminFee.ToValue() + H1113_ClosingFee.ToValue();
            }
        }

        public virtual decimal MarketingGain2
        {
            get
            {
                if (UnpaidPrincipalBalance.ToValue() == 0)
                    return 0;

                if (ClosedDate >= "10/1/2007".ToDateTime())
                    return ((InvestorBuyPrice.ToValue() - (100 - MarketDiscount.ToValue())) / 100) * 
                        (UnpaidPrincipalBalance.ToValue() - AdjustedNoteAmount.ToValue());

                return ((InvestorBuyPrice.ToValue() - 100) * UnpaidPrincipalBalance.ToValue() / 100) +
                    (MarketDiscount.ToValue() * AdjustedNoteAmount.ToValue() / 100) -
                    (InvestorBuyPrice.ToValue() - (100 - MarketDiscount.ToValue()) * AdjustedNoteAmount.ToValue() / 100);
            }
        }

        public virtual decimal MarketingGain
        {
            get
            {
                if (Channel.ToLower() == "brokered")
                    return 0;

                return (BranchPrice.ToDecimal() - (100 - MarketDiscount.ToValue())) * (AdjustedNoteAmount.ToValue() / 100);
            }
        }

        public virtual decimal SRP3
        {
            get
            {
                if (UnpaidPrincipalBalance.ToValue() == 0)
                    return 0;

                return (SecSRP.ToValue() * UnpaidPrincipalBalance.ToValue() / 100) -
                    (SecSRP.ToValue() * AdjustedNoteAmount.ToValue() / 100);
            }
        }

        public virtual decimal SRP2
        {
            get
            {
                if (UnpaidPrincipalBalance.ToValue() == 0)
                    return 0;
                if (LoanProgramCode.ToUpper().Substring(0, 3) == "RMH")
                    return SecSRP.ToValue() * (AdjustedNoteAmount.ToValue() - H827_ReverseMortgage.ToValue()) / 100;

                return SecSRP.ToValue() * AdjustedNoteAmount.ToValue() / 100;
            }
        }

        public virtual decimal SHBAmount
        {
            get
            {
                if (H5125_SHB.ToValue() == 0)
                	return 0;

                return H5125_SHB.ToValue() / 100 * AdjustedNoteAmount.ToValue();
            }
        }

        public virtual decimal PerDiem
        {
            get
            {
                return UnpaidPrincipalBalance.ToValue() * InterestRate.ToDecimal() / PerDiemDays.ToValue() / 100;
            }
        }

 
                
    }
}
