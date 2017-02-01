using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Extension;

namespace Bling.Domain.Compliance
{
    public class ChaseFundingFormLoanInfo
    {
        public virtual string FileId { get; set; }
        public virtual string LoanNumber { get; set; }
        public virtual string Underwriter { get; set; }
        public virtual string UnderwriterEmail { get; set; }
        public virtual string CorrespondentName { get; set; }
        public virtual string InvestorLoanNumber { get; set; }
        public virtual string CommitmentNo { get; set; }
        public virtual string UnderwritingPhoneNo { get; set; }
        public virtual string ProductDescription { get; set; }
        public virtual string ApplicationDate { get; set; }
        public virtual string PurchaseProperty { get; set; }
        public virtual string Item2 { get; set; }
        public virtual string OccInvestmentYes { get; set; }
        public virtual string OccInvestmentNo { get; set; }
        public virtual string OccInvestmentNA { get; set; }
        public virtual string ARMIndex { get; set; }
        public virtual string ARMMargin { get; set; }
        public virtual string APORPcnt { get; set; }
        public virtual string Item7AYes { get; set; }
        public virtual string Item7ANo { get; set; }
        public virtual string Item7BYes { get; set; }
        public virtual string Item7BNo { get; set; }
        public virtual string Item8Yes { get; set; }
        public virtual string Item8No { get; set; }
        public virtual string IsHPMLQM { get; set; }
        public virtual string IsHPMLAppraisal { get; set; }
        public virtual string PrepaymentPenalty { get; set; }
        public virtual string QMSafeHarbor { get; set; }
        public virtual string QMRebuttablePresumption { get; set; }
        public virtual string NonQM { get; set; }
        public virtual string QMNotApplicable { get; set; }
        public virtual string HoepaAPR { get; set; }
        public virtual string PointsExcluded { get; set; }
        public virtual string FeesImposed { get; set; }
        public virtual string Item15Percent { get; set; }
        public virtual string Item15Amount { get; set; }
        public virtual string HOEPAQMPcnt { get; set; }
        public virtual string HOEPAQMAmount { get; set; }
        public virtual string StatePcnt { get; set; }
        public virtual string StateAmount { get; set; }
        public virtual string SFFannieMae { get; set; }
        public virtual string SFFreddieMac { get; set; }
        public virtual string MIMonthlyPremium { get; set; }
        public virtual string MISinglePremium { get; set; }
        
        public virtual string ToJson()
        {
            StringBuilder json = new StringBuilder();

            json.AppendFormat(" {{ ");

            json.AppendFormat(" \"FileId\" : \"{0}\", ", FileId.Escape());
            json.AppendFormat(" \"LoanNumber\" : \"{0}\", ", LoanNumber);
            json.AppendFormat(" \"Underwriter\" : \"{0}\", ", Underwriter);
            json.AppendFormat(" \"UnderwriterEmail\" : \"{0}\", ", UnderwriterEmail);
            json.AppendFormat(" \"CorrespondentName\" : \"{0}\", ", CorrespondentName);
            json.AppendFormat(" \"InvestorLoanNumber\" : \"{0}\", ", InvestorLoanNumber);
            json.AppendFormat(" \"CommitmentNo\" : \"{0}\", ", CommitmentNo);
            json.AppendFormat(" \"UnderwritingPhoneNo\" : \"{0}\", ", UnderwritingPhoneNo);
            json.AppendFormat(" \"ProductDescription\" : \"{0}\", ", ProductDescription);
            json.AppendFormat(" \"ApplicationDate\" : \"{0}\", ", ApplicationDate);
            json.AppendFormat(" \"PurchaseProperty\" : \"{0}\", ", PurchaseProperty);
            json.AppendFormat(" \"Item2\" : \"{0}\", ", Item2);
            json.AppendFormat(" \"OccInvestmentYes\" : \"{0}\", ", OccInvestmentYes);
            json.AppendFormat(" \"OccInvestmentNo\" : \"{0}\", ", OccInvestmentNo);
            json.AppendFormat(" \"OccInvestmentNA\" : \"{0}\", ", OccInvestmentNA);
            json.AppendFormat(" \"ARMIndex\" : \"{0}\", ", ARMIndex);
            json.AppendFormat(" \"ARMMargin\" : \"{0}\", ", ARMMargin);
            json.AppendFormat(" \"APORPcnt\" : \"{0}\", ", APORPcnt);
            json.AppendFormat(" \"Item7AYes\" : \"{0}\", ", Item7AYes);
            json.AppendFormat(" \"Item7ANo\" : \"{0}\", ", Item7ANo);
            json.AppendFormat(" \"Item7BYes\" : \"{0}\", ", Item7BYes);
            json.AppendFormat(" \"Item7BNo\" : \"{0}\", ", Item7BNo);
            json.AppendFormat(" \"Item8Yes\" : \"{0}\", ", Item8Yes);
            json.AppendFormat(" \"Item8No\" : \"{0}\", ", Item8No);
            json.AppendFormat(" \"IsHPMLQM\" : \"{0}\", ", IsHPMLQM);
            json.AppendFormat(" \"IsHPMLAppraisal\" : \"{0}\", ", IsHPMLAppraisal);
            json.AppendFormat(" \"PrepaymentPenalty\" : \"{0}\", ", PrepaymentPenalty);
            json.AppendFormat(" \"QMSafeHarbor\" : \"{0}\", ", QMSafeHarbor);
            json.AppendFormat(" \"QMRebuttablePresumption\" : \"{0}\", ", QMRebuttablePresumption);
            json.AppendFormat(" \"NonQM\" : \"{0}\", ", NonQM);
            json.AppendFormat(" \"QMNotApplicable\" : \"{0}\", ", QMNotApplicable);
            json.AppendFormat(" \"HoepaAPR\" : \"{0}\", ", HoepaAPR);
            json.AppendFormat(" \"PointsExcluded\" : \"{0}\", ", PointsExcluded);
            json.AppendFormat(" \"FeesImposed\" : \"{0}\", ", FeesImposed);
            json.AppendFormat(" \"Item15Percent\" : \"{0}\", ", Item15Percent);
            json.AppendFormat(" \"Item15Amount\" : \"{0}\", ", Item15Amount);
            json.AppendFormat(" \"HOEPAQMPcnt\" : \"{0}\", ", HOEPAQMPcnt);
            json.AppendFormat(" \"HOEPAQMAmount\" : \"{0}\", ", HOEPAQMAmount);
            json.AppendFormat(" \"StatePcnt\" : \"{0}\", ", StatePcnt);
            json.AppendFormat(" \"StateAmount\" : \"{0}\", ", StateAmount);
            json.AppendFormat(" \"SFFannieMae\" : \"{0}\", ", SFFannieMae);
            json.AppendFormat(" \"SFFreddieMac\" : \"{0}\", ", SFFreddieMac);
            json.AppendFormat(" \"MIMonthlyPremium\" : \"{0}\", ", MIMonthlyPremium);
            json.AppendFormat(" \"MISinglePremium\" : \"{0}\" ", MISinglePremium);
            


            json.AppendFormat(" }} ");

            return json.ToString();
        }
    }
}
