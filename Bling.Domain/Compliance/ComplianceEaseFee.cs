using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Compliance
{
    public class ComplianceEaseFee
    {
        public ComplianceEaseFee()
        {
            FeeAmount = new List<ComplianceEaseFeeAmount>();
        }

        public string Section { get; set; }
        public string Type { get; set; }
        public string GFEAggregationType { get; set; }
        public string CompensationTo { get; set; }
        public string Description { get; set; }
        //public string OtherField { get; set; }
        public List<ComplianceEaseFeeAmount> FeeAmount { get; set; }


        public static ComplianceEaseFee Create(string section, string type, string gfeAgrregationType, List<ComplianceEaseFeeAmount> feeAmount, string compensationTo = "Default", string description = "")
        {
            //CompensationTo=Default|F=Default|Section=800:LoanFees
            //|GFEAggregationType=ChosenInterestRateCreditOrCharge
            //|Type=LoanDiscountPoints
            //|Amount=2798.50|PFC=Y::Amount=2798.50|PFC=Y

            return new ComplianceEaseFee
                {
                    Type = type,
                    CompensationTo = compensationTo,
                    FeeAmount = feeAmount,
                    Section = section,
                    GFEAggregationType = gfeAgrregationType,
                    Description = description
                };
        }

        public override string ToString()
        {
            string feeAmount = "";
            foreach (var fa in FeeAmount)
            {
                if (fa.ToString() != "")
                {
                    feeAmount += (feeAmount.Length > 0 ? "::" : "") + fa.ToString();
                }
            }

            if (feeAmount == "")
            {
                return "";
            }

            return String.Format("CompensationTo={0}|Section={1}|GFEAggregationType={2}|Type={3}|{4}{5}",
                CompensationTo, Section, GFEAggregationType, Type, feeAmount
                , Type == "Other" ? "|Description=" + Description : "");
        }
    }

    public class ComplianceEaseFeeAmount
    {
        public string Amount { get; set; }
        public string PFC { get; set; }
        public string FieldName { get; set; }

        public override string ToString()
        {
            var bonafide = "|STATEBONAFIDEINDICATOR=Y|GSEBONAFIDEINDICATOR=Y|FEDERALBONAFIDEINDICATOR=Y";
            if (Amount != "")
            {
                return String.Format("Amount={0}|PFC={1}|F=Default{2}", Amount, PFC,
                    FieldName == "LoanDiscount-Bonafide" ? bonafide : "");
            }

            return "";
        }
    }

    public class CEHelper
    {
        public static List<ComplianceEaseFeeAmount> AddFee(string fees)
        {
            var fa = new List<ComplianceEaseFeeAmount>();
            return fees.Split(',').ToList().ConvertAll(x => new ComplianceEaseFeeAmount { FieldName = x });
        }
    }
}
