using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Extension;

namespace Bling.Domain.Accounting
{
    public class IncomeBreakdownHtml
    {
        private IncomeBreakdown m_IB;

        public IncomeBreakdownHtml(IncomeBreakdown iB)
        {
            m_IB = iB;
        }

        public string ToHtmlTable()
        {
            StringBuilder html = new StringBuilder();

            html.Append(CreateFirstTable());
            html.AppendFormat("{0}{0}", "<br />");
            html.Append(CreateSecondTable());
            html.Append(CreateInvestorInterest());
            html.Append(CreateThirdTable());

            return html.ToString();
        }

        private string CreateFirstTable()
        {
            StringBuilder table = new StringBuilder();
            table.AppendFormat("<table width='60%' class='t1'>");

            table.AppendFormat("<tr class='yellow'>");
            table.AppendFormat("<td valign='top'>Input Date</td>");
            table.AppendFormat("<td valign='top'>Input Time</td>");
            table.AppendFormat("<td valign='top'>App Number</td>");
            table.AppendFormat("<td valign='top'>MINs#</td>");
            table.AppendFormat("<td valign='top'>DMI Loan #</td>");
            table.AppendFormat("<td valign='top' colspan='2'>Funding Date</td>");
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", DateTime.Now.ToShortDateString());
            table.AppendFormat("<td>{0}</td>", DateTime.Now.ToShortTimeString());
            table.AppendFormat("<td>{0}</td>", m_IB.ApplicationNumber);
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.LoanNumber);
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.DMILoanNumber);
            table.AppendFormat("<td colspan='2'>{0}</td>", m_IB.MWE3.ClosedDate.ToDate());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr class='yellow'>");
            table.AppendFormat("<td valign='top'>Loan Officer</td>");
            table.AppendFormat("<td valign='top'>Loan Amount</td>");
            table.AppendFormat("<td valign='top'>Interest Rate</td>");
            table.AppendFormat("<td valign='top'>Term</td>");
            table.AppendFormat("<td valign='top'>Int Only?</td>");
            table.AppendFormat("<td valign='top' colspan='2'>Unfunded Date</td>");
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.LoanOfficer);
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.AdjustedLoanAmount.ToCurrency());
            table.AppendFormat("<td>{0}</td>", (m_IB.MWE3.InterestRate.Value / 100).ToString("#.##0 %"));
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.LoanTerm);
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.InterestOnly ? "Yes" : "No");
            table.AppendFormat("<td colspan='2'>{0}</td>", "");
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr class='yellow'>");
            table.AppendFormat("<td>{0}</td>", "Borrower(s)");
            table.AppendFormat("<td colspan='2'>{0}</td>", "Branch");
            table.AppendFormat("<td colspan='2'>{0}</td>", "Loan Program");
            table.AppendFormat("<td colspan='2'>{0}</td>", "Cal Nova");
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.BorrowerLastFirstName);
            table.AppendFormat("<td colspan='2'>{0}</td>", m_IB.MWE3.BranchName);
            table.AppendFormat("<td colspan='2'>{0}</td>", m_IB.MWE3.LoanProgramDescription);
            table.AppendFormat("<td colspan='2'>{0}</td>", "");
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr class='yellow'>");
            table.AppendFormat("<td>{0}</td>", "Investor");
            table.AppendFormat("<td>{0}</td>", "EPP Date");
            table.AppendFormat("<td>{0}</td>", "Branch Price");
            table.AppendFormat("<td>{0}</td>", "Branch SRP");
            table.AppendFormat("<td>{0}</td>", "L O Price");
            table.AppendFormat("<td>{0}</td>", "Commit Price");
            table.AppendFormat("<td>{0}</td>", "Commit SRP");
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.InvestorName);
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.CSPurchaseDate.ToDate());
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.BranchPrice.To4d());
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.BranchSRP.To4d());
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.LOPrice.To4d());
            table.AppendFormat("<td>{0}</td>", m_IB.CommitPrice.To4d());
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.CommitSRP.To4d());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr class='yellow'>");
            table.AppendFormat("<td>{0}</td>", "Investor Price");
            table.AppendFormat("<td>{0}</td>", "Investor SRP");
            table.AppendFormat("<td>{0}</td>", "Incentive%");
            table.AppendFormat("<td>{0}</td>", "Price Diff.");
            table.AppendFormat("<td>{0}</td>", "Price Adj.");
            table.AppendFormat("<td>{0}</td>", "SHB");
            table.AppendFormat("<td>{0}</td>", "Lock-In Date");
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", m_IB.InvestorPrice.To4d());
            table.AppendFormat("<td>{0}</td>", m_IB.InvestorSRP.To4d());
            table.AppendFormat("<td>{0}</td>", m_IB.InvestorIncentive.To4d());
            table.AppendFormat("<td>{0}</td>", m_IB.PriceDifference.To4d());
            table.AppendFormat("<td>{0}</td>", m_IB.PriceAdjustment.ToCurrency());
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.H5125_SHB.To4d());
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.LockInDate.ToDate());
            table.AppendFormat("</tr>");

            table.AppendFormat("</table>");
            return table.ToString();

        }

        private string CreateSecondTable()
        {
            StringBuilder table = new StringBuilder();
            table.AppendFormat("<table width='60%' id='table2' class='t1'>");

            table.AppendFormat("<tr class='yellow'>");
            table.AppendFormat("<td width='15%' valign='top'>{0}</td>", "Journal Entries");
            table.AppendFormat("<td width='15%' valign='top'>{0}</td>", "Funding Income");
            table.AppendFormat("<td width='15%' valign='top'>{0}</td>", "Purchase Adj");
            table.AppendFormat("<td width='20%' valign='top'>{0}</td>", "Purchase Information");
            table.AppendFormat("<td width='15%' valign='top'>{0}</td>", "&nbsp;");
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "3044-Rev Mtg Set Aside:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.H827_ReverseMortgage.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", "Loan Balance at Purchase:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.UnpaidPrincipalBalance.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "5000-Origination");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.Origination.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", "Purchase Date:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.PurchasedPaidoffDate.ToDate());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "5070-Appraisal Fee:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.AppraisalFee.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", "8110-RFC Pkg./Wire Fee:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.RFCPackageWireFee.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "8325-Flood Fee:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.FloodFee);
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", "Inv. Int Pd To Date:");
            table.AppendFormat("<td>{0}</td>", m_IB.InvestorInterestPaidToDate.ToDate());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "5020-Credit Reports:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.CreditReport.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", "Payments to Gem:");
            table.AppendFormat("<td>{0}</td>", m_IB.MonthsDifference);
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "3750-PMI/MIP/VAFF:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.PMI_MIP_VAFF.ToCurrency());
            table.AppendFormat("<td>{0}</td>", m_IB.PMI_MIP_VAFF2.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "Impounds At Close:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.ImpoundsAtClose.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "3052-HELOC Draw Fee:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.HelocDrawFee.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", "Impounds Owed At Sale:");
            table.AppendFormat("<td>{0}</td>", m_IB.ImpoundsOwedAtSale.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "3042-EEM/Hud Repair:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.H818_EEM.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", "Imp. Ded. by Investor:");
            table.AppendFormat("<td>{0}</td>", m_IB.ImpoundDeductedByInvestor.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "5010-Doc Fee:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.H816_DocFee.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", "3044-Rev Mtg Deduction:");
            table.AppendFormat("<td>{0}</td>", m_IB.ReverseMortgagePayable.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "8110-Wire Fee:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.H817_WireFee.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", "3760-Impounds Adj.:");
            table.AppendFormat("<td>{0}</td>", m_IB.ImpoundsAdjustment.ToCurrency());            
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "8700-Tax Service Fee:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.TaxServiceFee.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", "3790-Buydown Payable:");
            table.AppendFormat("<td>{0}</td>", m_IB.GuildByDownFunds.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "5010-Processing Fee:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.H813_ProcessingFee.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", "3052: Prin. & Escrow");
            table.AppendFormat("<td>{0}</td>", m_IB.PrincipalAndEscrow.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "5107-Underwriting Fee:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.UnderwritingFee.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", "1260-SRP Receivable:");
            table.AppendFormat("<td>{0}</td>", m_IB.SRPReceivable.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}  <b>{1}</b></td>", "5100-045-Days Interest:", m_IB.MWE3.OddDaysInterest.ToValue());
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.H901_DaysInterest.ToCurrency());
            table.AppendFormat("<td>{0}</td>", m_IB.DaysInterest2.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "1225-Purch Adj Rcvble:");
            table.AppendFormat("<td>{0}</td>", m_IB.Option1Receivable.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}  <b>{1}</b></td>", "5100-Investor Interest:", m_IB.InvestorInterest);
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", m_IB.InvestorInterest2.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "1270-Interest Receivable");
            table.AppendFormat("<td>{0}</td>", m_IB.InterestReceivable.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "8381-Charge By Investor:");
            table.AppendFormat("<td>{0}</td>", m_IB.ChargeByInvestor.ToCurrency());
            table.AppendFormat("<td>{0}</td>", m_IB.ChargeByInvestor2.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "MIP");
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "8700-Tax Svc Chg By Inv:");
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", m_IB.TaxServiceCharge.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "Impound Adj Requestor:");
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "3750-MIP Credit:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.H213_MICredit.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0} <b>{1}</b></td>", "Purchase", m_IB.MWE3.WarehouseBank);
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "5110-Misc. Income:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.MiscIncome.ToCurrency());
            table.AppendFormat("<td>{0}</td>", m_IB.MiscIncome2.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "Inv. Purchase:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.InvestorWireAmount.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "5040-Discount Overage:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.DiscountOverage.ToCurrency());
            table.AppendFormat("<td>{0}</td>", m_IB.DiscountOverage2.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "Advance:");
            table.AppendFormat("<td>{0}</td>", m_IB.RFCAdvance.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0} <b>{1}</b></td>", "5050-Marketing Gain", m_IB.MWE3.MarketingGain2.ToCurrency());
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.MarketingGain.ToCurrency());
            table.AppendFormat("<td>{0}</td>", m_IB.MarketingGain3.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "CW Transfer Date:");
            table.AppendFormat("<td>{0}</td>", m_IB.CDRConfirmed.ToDate());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0} <b>{1} {2}</b></td>", "5090-SRP", m_IB.SRPFundingAdjustment.ToCurrency(), m_IB.MWE3.SRP3.ToCurrency());
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.SRP2.ToCurrency());
            table.AppendFormat("<td>{0}</td>", m_IB.SRP4.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "W/H Date");
            table.AppendFormat("<td>{0}</td>", m_IB.RFCWarehouseDate.ToDate());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "3790-Buydown:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.H810_BuyDown.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", "3210-Wire To Escrow");
            table.AppendFormat("<td>{0}</td>", m_IB.WireToEscrow.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "8110-Warehouse:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.H1303_WarehouseFee.ToCurrency());
            table.AppendFormat("<td>{0}</td>", m_IB.WarehouseFee2.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "1200-A/R:");
            table.AppendFormat("<td>{0}</td>", m_IB.AccountReceivable.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "3034-CW Settlement:");
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", m_IB.InterfirstPercent2.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "1230-PMI/MIP/VAFF:");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.PMI_MIP_VAFF.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "5125-Secondary Adj:");
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", m_IB.SecondaryAdjustment.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "Income Booked: 1265");
            table.AppendFormat("<td>{0}</td>", m_IB.IncomeTotal.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "5126-Hedge Gain/Loss");
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", m_IB.HedgeGainLoss.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "Income Received: 3040");
            table.AppendFormat("<td>{0}</td>", m_IB.IncomeReceived.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "5125 Secondary HB:");
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", m_IB.MWE3.SHBAmount.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "Income Adjustment");
            table.AppendFormat("<td>{0}</td>", m_IB.IncomeAdjustment.ToCurrency());
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr class='total'>");
            table.AppendFormat("<td>{0}</td>", "Income Total");
            table.AppendFormat("<td>{0}</td>", m_IB.IncomeTotal.ToCurrency());
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("<td>{0}</td>", "");
            table.AppendFormat("</tr>");

            table.AppendFormat("</table>");


            return table.ToString();
        }

        private string CreateInvestorInterest ()
        {
            StringBuilder html = new StringBuilder();

            html.AppendFormat("<fieldset class='investor_interest'>");
            html.AppendFormat("<legend>Investor Interest</legend>");
            html.AppendFormat("<table id='table3'>");

            html.AppendFormat("<tr>");
            html.AppendFormat("<td>{0}</td>", m_IB.InvInt.ToCurrency());
            html.AppendFormat("<td>{0}</td>", "Inv. Int.");
            html.AppendFormat("<td>{0}</td>", "");
            html.AppendFormat("</tr>");

            html.AppendFormat("<tr>");
            html.AppendFormat("<td>{0}</td>", m_IB.BuydownInterest.ToCurrency());
            html.AppendFormat("<td>{0}</td>", "Buydown");
            html.AppendFormat("<td>{0}</td>", "");
            html.AppendFormat("</tr>");

            html.AppendFormat("<tr>");
            html.AppendFormat("<td>{0}</td>", m_IB.Tax.ToCurrency());
            html.AppendFormat("<td>{0}</td>", "Tax");
            html.AppendFormat("<td>{0}</td>", "");
            html.AppendFormat("</tr>");

            html.AppendFormat("<tr>");
            html.AppendFormat("<td>{0}</td>", m_IB.MMI.ToCurrency());
            html.AppendFormat("<td>{0}</td>", "MMI");
            html.AppendFormat("<td>{0}</td>", "");
            html.AppendFormat("</tr>");

            html.AppendFormat("<tr>");
            html.AppendFormat("<td>{0}</td>", m_IB.MWE3.PrincipalReduction.ToCurrency());
            html.AppendFormat("<td>{0}</td>", "Prin. Red.");
            html.AppendFormat("<td>{0}</td>", "");
            html.AppendFormat("</tr>");

            html.AppendFormat("<tr>");
            html.AppendFormat("<td>{0}</td>", m_IB.Insurance.ToCurrency());
            html.AppendFormat("<td>{0}</td>", "Ins.");
            html.AppendFormat("<td>{0}</td>", "");
            html.AppendFormat("</tr>");

            html.AppendFormat("<tr>");
            html.AppendFormat("<td>{0}</td>", m_IB.Other.ToCurrency());
            html.AppendFormat("<td>{0}</td>", "Other");
            html.AppendFormat("<td>{0}</td>", "");
            html.AppendFormat("</tr>");

            html.AppendFormat("<tr>");
            html.AppendFormat("<td>{0}</td>", "-------------");
            html.AppendFormat("<td>{0}</td>", "");
            html.AppendFormat("<td>{0}</td>", "");
            html.AppendFormat("</tr>");

            html.AppendFormat("<tr>");
            html.AppendFormat("<td>{0}</td>", m_IB.InvIntTotal.ToCurrency());
            html.AppendFormat("<td>{0}</td>", "");
            html.AppendFormat("<td>{0}</td>", "");
            html.AppendFormat("</tr>");

            html.AppendFormat("<tr>");
            html.AppendFormat("<td>{0}</td>", "");
            html.AppendFormat("<td>{0}</td>", "");
            html.AppendFormat("<td>{0}</td>", "");
            html.AppendFormat("</tr>");

            html.Append("</table>");

            html.AppendFormat("<span class='perdiem'>{0}</span>", "Per Diem");
            html.AppendFormat("<span class='perdiemvalue'>{0}</span>", m_IB.MWE3.PerDiem.ToCurrency());
            html.AppendFormat("<span class='interestcalc'>{0}</span>", "Interest Calc");
            html.AppendFormat("<span class='interestcalcvalue'>{0}</span>", m_IB.MWE3.PerDiemDays);


            html.Append("</fieldset>");

            return html.ToString();

        }

        private string CreateThirdTable()
        {
            StringBuilder table = new StringBuilder();
            table.Append("<table id='table4' width='20%'>");
            
            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "Out of Balance:");
            table.AppendFormat("<td>{0}</td>", m_IB.OutOfBalance.ToCurrency());
            table.AppendFormat("<td></td>");
            table.AppendFormat("</tr>");

            table.AppendFormat("<tr>");
            table.AppendFormat("<td>{0}</td>", "A/R Difference:");
            table.AppendFormat("<td>{0}</td>", m_IB.AccountReceivableDifference.ToCurrency());
            table.AppendFormat("<td></td>");
            table.AppendFormat("</tr>");

            table.Append("</table>");

            table.Append("<br/>Comments-Secondary:<br />");
            table.AppendFormat("<textarea rows=3' cols='35'>{0}</textarea><br />", m_IB.SecondaryComment);

            table.Append("<br/>Comments-Branch:<br />");
            table.AppendFormat("<textarea rows=3' cols='35'>{0}</textarea><br />", m_IB.BranchComment);

            table.Append("<br/>Comments-Accounting:<br />");
            table.AppendFormat("<textarea rows=3' cols='35'>{0}</textarea><br />", m_IB.AccountingComment);

            table.AppendFormat("<div id='clearfloat'>Linked Loan: <strong>{0}</strong></div>", "");
            return table.ToString();
        }
    }
}
