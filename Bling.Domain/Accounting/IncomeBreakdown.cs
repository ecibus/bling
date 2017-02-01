using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Extension;

namespace Bling.Domain.Accounting
{
    public class IncomeBreakdown
    {
        public virtual string ApplicationNumber { get; set; }
        public virtual decimal ? CommitPrice  { get; set; }
        public virtual decimal ? InvestorPrice { get; set; }
        public virtual decimal ? InvestorSRP { get; set; }
        public virtual float ? InvestorIncentive { get; set; }
        public virtual DateTime ? InvestorInterestPaidToDate { get; set; }
        public virtual decimal ? PMI_MIP_VAFF2 { get; set; }
        public virtual decimal ? ImpoundDeductedByInvestor { get; set; }
        public virtual decimal ? ReverseMortgagePayable { get; set; }
        public virtual decimal ? ImpoundsAdjustment { get; set; }
        public virtual decimal ? GuildByDownFunds { get; set; }
        public virtual decimal ? SRPReceivable { get; set; }
        public virtual decimal ? DaysInterest2 { get; set; }
        public virtual decimal ? Option1Receivable { get; set; }
        public virtual decimal ? InterestReceivable { get; set; }
        public virtual decimal ? ChargeByInvestor { get; set; }
        public virtual decimal ? ChargeByInvestor2 { get; set; }
        public virtual decimal ? TaxServiceCharge { get; set; }
        public virtual decimal ? MiscIncome2 { get; set; }
        public virtual decimal ? DiscountOverage2 { get; set; }
        public virtual decimal ? RFCAdvance { get; set; }
        public virtual decimal ? MarketingGain3 { get; set; }
        public virtual DateTime ? CDRConfirmed { get; set; }
        public virtual decimal ? SRPFundingAdjustment { get; set; }
        public virtual decimal ? SRP4 { get; set; }
        public virtual DateTime ? RFCWarehouseDate { get; set; }
        public virtual decimal ? WireToEscrow { get; set; }
        public virtual decimal ? WarehouseFee2 { get; set; }
        public virtual decimal ? InterfirstPercent2 { get; set; }
        public virtual decimal ? SecondaryAdjustment { get; set; }
        public virtual decimal ? AccountReceivableAtFunding { get; set; }
        public virtual string SecondaryComment { get; set; }
        public virtual string BranchComment { get; set; }
        public virtual string AccountingComment { get; set; }
        
        
        public virtual MWE3Data MWE3 { get; set; }

        public virtual decimal? PriceDifference
        {
            get
            {
                decimal val;

                if (CommitPrice.ToValue() == 0)
                    val = MWE3.H5125_SHB.ToValue() + MWE3.BranchPrice.ToDecimal() + MWE3.BranchSRP.ToDecimal() 
                        + InvestorIncentive.ToDecimal();
                else
                    val = MWE3.H5125_SHB.ToValue() + CommitPrice.ToValue() + MWE3.CommitSRP.ToDecimal()
                        + InvestorIncentive.ToDecimal();

                return InvestorPrice.ToValue() + InvestorSRP.ToValue() - val;                
            }
        }

        public virtual decimal ? PriceAdjustment
        {
            get
            {
                return MWE3.UnpaidPrincipalBalance.ToValue() * PriceDifference.ToValue() / 100;
            }
        }
        
        public virtual int MonthsDifference
        {
            get
            {
                if (MWE3.UnpaidPrincipalBalance.ToValue() == 0)
                    return 0;

                if (InvestorInterestPaidToDate == null)
                    return 0;

                int months = 12 * (InvestorInterestPaidToDate.Value.Year - MWE3.FirstPaymentDate.Value.Year) +
                    InvestorInterestPaidToDate.Value.Month - MWE3.FirstPaymentDate.Value.Month;

                return months + 1;
            }
        }

         public virtual decimal? ImpoundsOwedAtSale
        {
            get
            {
                return MWE3.ImpoundsAtClose.ToValue() + (
                    (MWE3.H1001_HazardInsurance.ToValue() + MWE3.H1002_MI.ToValue() + MWE3.H1004_CountyTax.ToValue()
                    + MWE3.H1006_FloodInsurance.ToValue() + MWE3.H1007_Other.ToValue())
                    * MonthsDifference
                    );
            }
        }

        public virtual decimal ? Tax
         {
             get
             {
                 if (MWE3.UnpaidPrincipalBalance.ToValue() == 0)
                    return 0;
                 return MWE3.H1004_CountyTax.ToValue() * MonthsDifference * -1;
             }
         }

        public virtual decimal ? MMI
        {
            get
            {
                if (MWE3.UnpaidPrincipalBalance.ToValue() == 0)
                    return 0;
                return MWE3.H1002_MI.ToValue() * MonthsDifference * -1;
            }
        }

        public virtual decimal ? Insurance 
        {
            get
            {
                if (MWE3.UnpaidPrincipalBalance.ToValue() == 0)
                    return 0;
                return MWE3.H1001_HazardInsurance.ToValue() * MonthsDifference * -1;
            }
        }

        public virtual decimal ? Other
        {
            get
            {
                if (MWE3.UnpaidPrincipalBalance.ToValue() == 0)
                    return 0;
                return (MWE3.H1006_FloodInsurance.ToValue() + MWE3.H1007_Other.ToValue()) * MonthsDifference * -1;
            }
        }
        
        public virtual decimal ? PrincipalAndEscrow
        {
            get { return Tax + MMI + MWE3.PrincipalReduction + Insurance + Other; }
        }

        public virtual int InvestorInterest
        {
            get
            {
                if (!MWE3.PurchasedDate.HasValue)
                    return 0;

                if (!InvestorInterestPaidToDate.HasValue)
                    return 0;

                TimeSpan difference = MWE3.PurchasedDate.Value.Subtract(InvestorInterestPaidToDate.Value);

                if (MWE3.PurchasedDate.Value > InvestorInterestPaidToDate.Value)
                    return difference.Days;

                return difference.Days + 1;
            }
        }

        public virtual decimal InvInt
        {
            get
            {
                if (MWE3.UnpaidPrincipalBalance.ToValue() == 0)
                    return 0;

                if (!InvestorInterestPaidToDate.HasValue)
                    return 0;

                decimal someValue = MWE3.UnpaidPrincipalBalance.ToValue() * MWE3.InterestRate.ToDecimal() 
                    / MWE3.PerDiemDays.ToValue() / 100;

                int dateDifference = MWE3.PurchasedPaidoffDate.Value.Subtract(InvestorInterestPaidToDate.Value).Days;

                if (MWE3.PurchasedPaidoffDate.Value > InvestorInterestPaidToDate.Value)
                    return someValue * dateDifference;

                return someValue * (dateDifference + 1);
            }
        }

        public virtual decimal BuydownInterest
        {
            get { return MWE3.H810_BuyDown.ToValue() + MonthsDifference; }
        }

        public virtual decimal ? InvestorInterest2
        {
            get { return InvInt + BuydownInterest; }
        }

        public virtual decimal IncomeTotal
        {
            get
            {
                decimal subtotal = MWE3.H817_WireFee.ToValue() + MWE3.H1303_WarehouseFee.ToValue() + MWE3.FloodFee.ToValue() +
                    MWE3.Origination.ToValue() + MWE3.AppraisalFee.ToValue() + MWE3.CreditReport.ToValue() +
                    MWE3.H816_DocFee.ToValue() + MWE3.TaxServiceFee.ToValue() + MWE3.H813_ProcessingFee.ToValue() +
                    MWE3.UnderwritingFee.ToValue() + MWE3.MiscIncome + MWE3.DiscountOverage.ToValue() +
                    MWE3.MarketingGain + MWE3.SRP2 + MWE3.H827_ReverseMortgage.ToValue();

                if (MWE3.Channel.ToLower() == "brokered")
                    return subtotal;

                return subtotal + MWE3.H901_DaysInterest.ToValue() - ChargeByInvestor.ToValue() +
                    MWE3.H818_EEM.ToValue() + MWE3.HelocDrawFee.ToValue();
               	
            }
        }

        public virtual decimal AccountReceivable
        {
            get { return WireToEscrow.ToValue() + IncomeTotal; }
        }

        public virtual decimal HedgeGainLoss
        {
            get
            {
                if (CommitPrice.ToValue() == 0)
                    return 0;
                
                return (CommitPrice.ToValue() +
                    (MWE3.CommitSRP.ToDecimal() - MWE3.BranchPrice.ToDecimal() - MWE3.SecSRP.ToValue())) *
                    MWE3.AdjustedNoteAmount.ToValue() / 100;
            }
        }

        public virtual decimal IncomeReceived
        {
            get
            {
                if (MWE3.UnpaidPrincipalBalance.ToValue() == 0)
                    return 0;

                return MWE3.InvestorWireAmount.ToValue() - WireToEscrow.ToValue() -
                    MWE3.PMI_MIP_VAFF.ToValue();
            }
        }

        public virtual decimal IncomeAdjustment
        {
            get
            {
                if (MWE3.UnpaidPrincipalBalance.ToValue() == 0)
                    return 0;

                return SecondaryAdjustment.ToValue() + WarehouseFee2.ToValue() + PMI_MIP_VAFF2.ToValue() +
                    DaysInterest2.ToValue() + InvIntTotal + BuydownInterest - ReverseMortgagePayable.ToValue() -
                    ChargeByInvestor2.ToValue() - TaxServiceCharge.ToValue() + MiscIncome2.ToValue() +
                    DiscountOverage2.ToValue() + MWE3.MarketingGain2 + MWE3.SRP3 + ImpoundsAdjustment.ToValue() +
                    MarketingGain3.ToValue() + SRP4.ToValue() + GuildByDownFunds.ToValue() + InterfirstPercent2.ToValue() +
                    HedgeGainLoss + (MWE3.H5125_SHB.ToValue() / 100 * MWE3.AdjustedNoteAmount.ToValue()) -
                    SRPReceivable.ToValue() - Option1Receivable.ToValue() - InterestReceivable.ToValue();

            }
        }

        public virtual decimal InvIntTotal1
        {
            get
            {
                int dateDifference = MWE3.PurchasedPaidoffDate.Value.Subtract(InvestorInterestPaidToDate.Value).Days;
                decimal someValue = MWE3.UnpaidPrincipalBalance.ToValue() * MWE3.InterestRate.ToDecimal() / 
                    MWE3.PerDiemDays.ToValue() / 100;
                decimal someValue2;

                if (MWE3.PurchasedPaidoffDate.Value > InvestorInterestPaidToDate.Value)
                    someValue2 = someValue * dateDifference;
                else
                    someValue2 = someValue * (dateDifference + 1);

                decimal someValue3 = (MWE3.H1004_CountyTax.ToValue() + MWE3.H1002_MI.ToValue() +
                    MWE3.H1001_HazardInsurance.ToValue() + MWE3.H1006_FloodInsurance.ToValue() +
                    MWE3.H1007_Other.ToValue()) * MonthsDifference * -1;

                decimal someValue4 = 0;

                if (MWE3.UnpaidPrincipalBalance.ToValue() != 0)                
                    someValue4 = (MWE3.AdjustedNoteAmount.ToValue() - MWE3.UnpaidPrincipalBalance.ToValue()) * -1;

                return someValue2 + someValue3 + someValue4;
            }
        }

        public virtual decimal InvIntTotal
        {
            get
            {
                if (MWE3.UnpaidPrincipalBalance.ToValue() == 0)
                    return 0;

                return InvIntTotal1;
            }
        }

        public virtual decimal OutOfBalance
        {
            get
            {
                if (MWE3.UnpaidPrincipalBalance.ToValue() == 0)
                    return 0;

                return IncomeReceived - IncomeTotal - IncomeAdjustment;
            }
        }

        public virtual decimal AccountReceivableDifference
        {
            get
            {
                return AccountReceivableAtFunding.ToValue() - AccountReceivable ;
            }
        }

        public virtual string ToHtmlTable()
        {
            return new IncomeBreakdownHtml(this).ToHtmlTable();
        }
    }
}
