using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Compliance
{
    public class ComplianceEase
    {
        public string RESPAConformingYearType { get; set; }
        public IList<CEField> Field { get; set; }
        public IList<string> Header { get; set; }
        public IList<IList<string>> Data { get; set; }
        public IList<ComplianceEaseFee> Fees { get; set; }

        public ComplianceEase()
        {
            Data = new List<IList<string>>();
            BuildColumn();
            BuildFees();
        }

        public void BuildFees()
        {
            Fees = new List<ComplianceEaseFee>();

            //CompensationTo=Default|F=Default|Section=800:LoanFees|GFEAggregationType=ChosenInterestRateCreditOrCharge
            //|Type=LoanDiscountPoints|Amount=2798.50|PFC=Y::Amount=2798.50|PFC=Y

            //Fees.Add(new ComplianceEaseFee 
            //    { 
            //        CompensationTo = "Default", 
            //        Section = "800:LoanFees", 
            //        GFEAggregationType = "OurOriginationCharge", 
            //        Type = "LoanOriginationFee" });

            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "LoanOriginationFee", "OurOriginationCharge", CEHelper.AddFee("LoanOriginationFee")));
            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "LenderInspectionFeePriorToClosing", "OurOriginationCharge", CEHelper.AddFee("LenderInspectionFeePriorToClosing")));
            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "ProcessingFee", "OurOriginationCharge", CEHelper.AddFee("ProcessingFee")));
            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "UnderwritingFee", "OurOriginationCharge", CEHelper.AddFee("UnderwritingFee")));
            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "AppraisalReviewFee", "OurOriginationCharge", CEHelper.AddFee("AppraisalReviewFee")));
            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "DocumentPreparationFee", "OurOriginationCharge", CEHelper.AddFee("DocumentPreparationFee")));
            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "WireTransferFee", "OurOriginationCharge", CEHelper.AddFee("WireTransferFee")));
            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "Other", "OurOriginationCharge", CEHelper.AddFee("FundingFeeLender"), description: "Funding Fee Lender"));
            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "Other", "OurOriginationCharge", CEHelper.AddFee("HUDRepairHoldbackAdminFee"), description: "HUD Repair Holdback Admin Fee"));

            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "LoanDiscountPoints", "ChosenInterestRateCreditOrCharge", CEHelper.AddFee("LoanDiscount,LoanDiscount-Bonafide")));
            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "AppraisalFee", "None", CEHelper.AddFee("AppraisalFee")));
            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "CreditReportFee", "None", CEHelper.AddFee("CreditReportFee")));
            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "TaxRelatedServiceFee", "None", CEHelper.AddFee("TaxRelatedServiceFee")));
            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "FloodDeterminationLifeOfLoanFee", "None", CEHelper.AddFee("FloodDeterminationLifeOfLoanFee")));
            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "AppraisalReinspectionFee", "None", CEHelper.AddFee("AppraisalReinspectionFee")));

            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "Other", "None", CEHelper.AddFee("203KInspectionFee"), description: "203K Inspection Fee"));
            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "Other", "None", CEHelper.AddFee("AppraisalFeeWaiver"), description: "Appraisal Fee Waiver"));
            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "Other", "None", CEHelper.AddFee("HomeownersAsscCertFee"), description: "Homeowners Assc Cert Fee"));
            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "Other", "None", CEHelper.AddFee("MCCApplicationFee"), description: "MCC Application Fee"));
            Fees.Add(ComplianceEaseFee.Create("800:LoanFees", "Other", "None", CEHelper.AddFee("SubordinationFee"), description: "Subordination Fee"));

            Fees.Add(ComplianceEaseFee.Create("900:LenderRequiredPaidInAdvance", "InterimInterest", "None", CEHelper.AddFee("InterimInterest")));
            Fees.Add(ComplianceEaseFee.Create("900:LenderRequiredPaidInAdvance", "HazardInsurancePremium", "None", CEHelper.AddFee("HazardInsurancePremium")));
            Fees.Add(ComplianceEaseFee.Create("900:LenderRequiredPaidInAdvance", "FloodInsurancePremium", "None", CEHelper.AddFee("FloodInsurancePremium")));
            Fees.Add(ComplianceEaseFee.Create("900:LenderRequiredPaidInAdvance", "Other", "None", CEHelper.AddFee("HurricaneInsurancePremium"), description: "Hurricane Insurance Premium"));

            Fees.Add(ComplianceEaseFee.Create("1000:ReservesDepositedWithLender", "HazardInsuranceReserve", "InitialDepositForYourEscrowAccount", CEHelper.AddFee("HazardInsuranceReserve")));
            Fees.Add(ComplianceEaseFee.Create("1000:ReservesDepositedWithLender", "PropertyTaxesReserve", "InitialDepositForYourEscrowAccount", CEHelper.AddFee("PropertyTaxesReserve")));

            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "SettlementOrClosingFee", "TitleServices", CEHelper.AddFee("SettlementOrClosingFee")));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "LendersCoverage", "LendersTitleInsurance", CEHelper.AddFee("LendersCoverage")));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "AssignmentEndorsementFee", "LendersTitleInsurance", CEHelper.AddFee("AssignmentEndorsementFee")));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "AbstractOrTitleSearchFee", "TitleServices", CEHelper.AddFee("AbstractOrTitleSearchFee")));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "TitleExaminationFee", "TitleServices", CEHelper.AddFee("TitleExaminationFee")));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "TitleDocumentPreparationFee", "TitleServices", CEHelper.AddFee("TitleDocumentPreparationFee")));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "NotaryFee", "TitleServices", CEHelper.AddFee("NotaryFee")));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "SubescrowFee", "TitleServices", CEHelper.AddFee("SubescrowFee")));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "ReconveyanceFee", "TitleServices", CEHelper.AddFee("ReconveyanceFee")));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "TitleCourierFee", "TitleServices", CEHelper.AddFee("TitleCourierFee")));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "FundingWireOrDisbursementFee", "TitleServices", CEHelper.AddFee("FundingWireOrDisbursementFee")));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "TieInFee", "TitleServices", CEHelper.AddFee("TieInFee")));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "DocumentSigningFee", "TitleServices", CEHelper.AddFee("DocumentSigningFee")));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "Other", "TitleServices", CEHelper.AddFee("EscrowProcessingFee"), description: "Escrow Processing Fee"));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "Other", "TitleServices", CEHelper.AddFee("PayoffDemandFee"), description: "Payoff Demand Fee"));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "OwnersCoverage", "OwnersTitleInsurance", CEHelper.AddFee("OwnersCoverage")));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "Other", "OwnersTitleInsurance", CEHelper.AddFee("203KTitleUpdate"), description: "203K Title Update"));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "Other", "OwnersTitleInsurance", CEHelper.AddFee("ArchiveFee"), description: "Archive Fee"));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "Other", "OwnersTitleInsurance", CEHelper.AddFee("AuditFee"), description: "Audit Fee"));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "Other", "OwnersTitleInsurance", CEHelper.AddFee("CopyFaxFee"), description: "Copy Fax Fee"));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "Other", "OwnersTitleInsurance", CEHelper.AddFee("EmailDocFee3rdParty"), description: "Email Doc Fee 3rd Party"));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "Other", "OwnersTitleInsurance", CEHelper.AddFee("ReleaseTrackingFee"), description: "Release Tracking Fee"));

            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "Other", "None", CEHelper.AddFee("Endorsement8P1VAOnlyFee"), description: "Endorsement 8.1 VA Only Fee"));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "Other", "None", CEHelper.AddFee("EscrowSalesTax"), description: "Escrow Sales Tax"));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "Other", "None", CEHelper.AddFee("GovernmentServiceFee"), description: "Government Service Fee"));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "Other", "None", CEHelper.AddFee("TitleSalesTax"), description: "Title Sales Tax"));
            Fees.Add(ComplianceEaseFee.Create("1100:TitleCharges", "Other", "None", CEHelper.AddFee("TranslationFee"), description: "Translation Fee"));

            Fees.Add(ComplianceEaseFee.Create("1200:RecordingAndTransferCharges", "RecordingFee", "GovernmentRecordingCharges", CEHelper.AddFee("RecordingFee")));
            Fees.Add(ComplianceEaseFee.Create("1200:RecordingAndTransferCharges", "RecordingServiceFee", "GovernmentRecordingCharges", CEHelper.AddFee("RecordingServiceFee")));
            Fees.Add(ComplianceEaseFee.Create("1200:RecordingAndTransferCharges", "CityCountyTaxStamps", "TransferTaxes", CEHelper.AddFee("CityCountyTaxStamps")));
            Fees.Add(ComplianceEaseFee.Create("1200:RecordingAndTransferCharges", "StateTaxStamps", "TransferTaxes", CEHelper.AddFee("StateTaxStamps")));

            Fees.Add(ComplianceEaseFee.Create("1300:AdditionalSettlementCharges", "PestInspectionFee", "RequiredServicesYouCanShopFor", CEHelper.AddFee("PestInspectionFee")));
            Fees.Add(ComplianceEaseFee.Create("1300:AdditionalSettlementCharges", "ArchitecturalAndEngineeringFee", "RequiredServicesYouCanShopFor", CEHelper.AddFee("ArchitecturalAndEngineeringFee")));
            Fees.Add(ComplianceEaseFee.Create("1300:AdditionalSettlementCharges", "Other", "RequiredServicesYouCanShopFor", CEHelper.AddFee("RoofCertificationFee"), description: "Roof Certification Fee"));
            Fees.Add(ComplianceEaseFee.Create("1300:AdditionalSettlementCharges", "Other", "RequiredServicesYouCanShopFor", CEHelper.AddFee("SepticCertificationFee"), description: "Septic Certification Fee"));
            Fees.Add(ComplianceEaseFee.Create("1300:AdditionalSettlementCharges", "SellerPaidPointsAndFees", "None", CEHelper.AddFee("SellerPaidPointsAndFees")));

        }

        public void BuildColumn()
        {
            Header = new List<string>();

            // General (Mandatory) - 9
            Header.Add("RESPAConformingYearType");
            Header.Add("AuditType");
            Header.Add("LenderName");
            Header.Add("LoanNumber");
            Header.Add("LicenseType");
            Header.Add("IsDIDMCAExempt"); 
            Header.Add("HUDApprovedLender");
            Header.Add("City");

            Header.Add("County");
            Header.Add("State");
            Header.Add("PostalCode");
            Header.Add("Type");
            Header.Add("OccupancyType");
            Header.Add("NumberofUnits");

            Header.Add("ProgramType");
            Header.Add("LoanPurposeType");
            Header.Add("PurposeOfRefinanceType");
            Header.Add("ConstructionType");
            Header.Add("LTV");
            Header.Add("CLTV");

            Header.Add("LoanType");
            Header.Add("LienType");
            Header.Add("DocumentType");
            Header.Add("LoanAmount");

            Header.Add("InterestRate");
            Header.Add("UndiscountedInterestRatePercent");
            Header.Add("DisclosedAPR");
            Header.Add("DisclosedFinanceCharge");
            Header.Add("MaturityTerm");
            Header.Add("AmortizationTerm");

            Header.Add("LateChargesPercent");
            Header.Add("GracePeriodDays");
            Header.Add("PrepaymentTerm");
            Header.Add("NegativeAmortizationProgramType");

            Header.Add("ApplicationReceivedByCreditorDate");
            Header.Add("GfeDisclosureDate");
            Header.Add("TilDisclosureDate");
            Header.Add("ClosingDate");
            Header.Add("FundingDate");
            Header.Add("RateLockDate");

//General (Recommended) - 15
            Header.Add("ApplicationReceivedByOriginatorDate");

            Header.Add("BorrowerDTIRatio");
            Header.Add("PropertyNotSituatedInJurisdiction");
            Header.Add("IsRefinancingPortfolioLoan");
            Header.Add("IsIrregularPaymentTransaction");
            Header.Add("DiscountPointsGSEBonaFideIndicator");
            Header.Add("DiscountPointsStateBonaFideIndicator");

            Header.Add("NumberOfDaysInterestPrepaid");
            Header.Add("IsAttorneysFeeExcludableByBorrowerChoice");
            Header.Add("IsAttorneysFeeOtherExcludableByBorrowerChoice");

//General (Optional) - 17
            Header.Add("OriginatorName"); 
            Header.Add("MIN");
            Header.Add("CustomField1");

            Header.Add("CustomField2");
            Header.Add("ProductCustomField1");
            Header.Add("ProductCustomField2");
            Header.Add("CompanyNMLSID");
            Header.Add("BranchNMLSID");
            Header.Add("OriginatorNMLSID");
            Header.Add("InvestorName");

            Header.Add("BorrowerFirstName");
            Header.Add("BorrowerLastName");
            Header.Add("BorrowerTotalMonthlyIncome");
            Header.Add("StreetNumber");
            Header.Add("StreetName");
            Header.Add("StreetType");
            Header.Add("StreetDirectionType");
            Header.Add("StreetUnitNumber");
            Header.Add("Section32DisclosureDate");


//Prepayment Penalty (Mandatory) - 19
            Header.Add("PrepaymentProgramName");
            Header.Add("MaxPrepaymentPenalty");

//Construction / Construction To Permanent (Mandatory) - 20
            Header.Add("ConstructionPeriodInterestRatePercent");
            Header.Add("ConstructionLoanInterestEstimationType");
            Header.Add("ConstructionPeriodNumberOfMonthsCount");
            Header.Add("ConstructionLoanInterestReserveAmount");

//ARM (Mandatory) - 21
            Header.Add("Margin");
            Header.Add("Index");
            Header.Add("Ceiling");
            Header.Add("Floor");
            Header.Add("RateFirstAdjustmentCap");
            Header.Add("RateFirstAdjustmentPeriod");
            Header.Add("RateSubsequentAdjustmentCap");
            Header.Add("RateSubsequentAdjustmentPeriod");
            Header.Add("AdjustmentRoundingType");

//Composite Rate (Mandatory) - 23
            Header.Add("ARMIndexType");
            Header.Add("AlternateARMIndexValuePercent");

//Standard Negative Amortization Type (Mandatory) - 24
            Header.Add("PaymentAdjustmentCap");
            Header.Add("PaymentAdjustmentPeriod");
            Header.Add("RecastStart");
            Header.Add("RecastStop");
            Header.Add("RecastPeriod");
            Header.Add("PotentialNegativeAmortizationMaxBalance");

//Standard Negative Amortization Type (Recommended) - 25
            Header.Add("LoanNegativeAmortizationResolutionType");
            Header.Add("MaximumBalanceCanBeExceededByOnePaymentIndicator");
            Header.Add("InterestOnlyTerm");

//Reduced Rate Negative Amortization Type (Mandatory) - 26
            Header.Add("InitialPaymentRatePercent");
            Header.Add("InitialPaymentOptionPeriodMonths");
            Header.Add("PostOptionPeriodInterestOnlyTermMonths");
            Header.Add("RecastStart");
            Header.Add("RecastStop");
            Header.Add("RecastPeriod");
            Header.Add("PotentialNegativeAmortizationMaxBalance");
            Header.Add("PaymentAdjustmentCap");
            Header.Add("PaymentAdjustmentPeriod");

//Reduced Rate Negative Amortization Type (Recommended) - 28
            Header.Add("InterestOnlyDuringOptionPeriodIndicator");
            Header.Add("LoanNegativeAmortizationResolutionType");
            Header.Add("MaximumBalanceCanBeExceededByOnePaymentIndicator");

//Fixed Initial Payment Negative Amortization Type (Mandatory) - 28
            Header.Add("InitialPaymentOptionPeriodMonths");
            Header.Add("InitialPaymentAdjustmentCap");
            Header.Add("PaymentAdjustmentCap");
            Header.Add("PaymentAdjustmentPeriod");
            Header.Add("RecastStart");
            Header.Add("RecastStop");
            Header.Add("RecastPeriod");
            Header.Add("PotentialNegativeAmortizationMaxBalance");

//Fixed Initial Payment Negative Amortization Type (Recommended) - 30
            Header.Add("LoanNegativeAmortizationResolutionType");
            Header.Add("MaximumBalanceCanBeExceededByOnePaymentIndicator");
            Header.Add("InterestOnlyTerm");

//Multiple Payment Discount Negative Amortization Type (Mandatory) - 31
            Header.Add("InitialPaymentDiscountPercent");
            Header.Add("InitialPaymentDiscountPeriodMonths");
            Header.Add("SubsequentPaymentDiscountPercent");
            Header.Add("TotalPaymentDiscountPeriodMonths");
            Header.Add("RecastStart");
            Header.Add("RecastStop");
            Header.Add("RecastPeriod");
            Header.Add("PotentialNegativeAmortizationMaxBalance");

//Multiple Payment Discounts Negative Amortization Type (Recommended) - 32
            Header.Add("LoanNegativeAmortizationResolutionType");
            Header.Add("MaximumBalanceCanBeExceededByOnePaymentIndicator");
            Header.Add("InterestOnlyTerm");

//Adjustable Reduced Rate Negative Amortization Type (Mandatory) - 33
            Header.Add("MinimumPaymentRateMarginPercent");
            Header.Add("MinimumPaymentRateFloorPercent");
            Header.Add("RecastStart");
            Header.Add("RecastStop");
            Header.Add("RecastPeriod");
            Header.Add("PotentialNegativeAmortizationMaxBalance");

//Adjustable Reduced Rate Amortization Type (Recommended) - 34
            Header.Add("InterestOnlyDuringOptionPeriodIndicator");
            Header.Add("LoanNegativeAmortizationResolutionType");
            Header.Add("MaximumBalanceCanBeExceededByOnePaymentIndicator");

//GPM (Mandatory) - 35
            Header.Add("GpmRate");
            Header.Add("GpmTerm");

//Interest Only (Mandatory) - 35
            Header.Add("InterestOnlyTerm");

//Buydown (Mandatory) - 35
            Header.Add("BuydownRate1");
            Header.Add("BuydownRate2");
            Header.Add("BuydownRate3");
            Header.Add("BuydownRate4");
            Header.Add("BuydownRate5");
            Header.Add("BuydownTerm1");
            Header.Add("BuydownTerm2");
            Header.Add("BuydownTerm3");
            Header.Add("BuydownTerm4");
            Header.Add("BuydownTerm5");

//Dual Amortization (Mandatory) - 36
            Header.Add("InitialLoanAmortizationTermMonths");
            Header.Add("InitialLoanAmortizationPeriodMonths");
            Header.Add("SubsequentLoanAmortizationTermMonths");
            Header.Add("SubsequentLoanAmortizationPeriodMonths");

//Conventional PMI (Mandatory) - 37
            Header.Add("PMIUpfrontPremiumRate");
            Header.Add("PMIUpfrontPremiumAmount");
            //Header.Add("FinancedMIAndFundingFeeIsPFC");
            //Header.Add("PMIUpfrontPremiumCashAmount");
            Header.Add("MIPaymentStreamShiftMonths");
            Header.Add("PMIUpfrontPremiumIsPFC");
            Header.Add("PMIInitialMonthlyPremiumAmount");
            Header.Add("PMIInitialMonthlyPremiumRate");
            Header.Add("PMIInitialMonthlyPremiumPeriod");
            Header.Add("PMIRenewMonthlyPremiumAmount");
            Header.Add("PMIRenewMonthlyPremiumRate");
            Header.Add("PMIRenewMonthlyPremiumPeriod");
            Header.Add("PMIMonthlyPremiumLTVCutoff");
            Header.Add("MIRenewalCalculationType");
            Header.Add("MICancelledAtMidpointIndicator");

//FHA MIP (Mandatory) - 41
            Header.Add("MIPUpfrontPremiumRate");
            Header.Add("MIPUpfrontPremiumCashAmount");
            Header.Add("MIPUpfrontPremiumIsPFC");
            Header.Add("MIPAnnualPremiumRate");
            Header.Add("MIPAnnualPremiumPeriod");
            Header.Add("MIPAnnualPremiumLTVCutoff");

//VA FF (Mandatory) - 43
            Header.Add("VAFundingFeePercent");
            Header.Add("VAFundingFeeAmount");
            Header.Add("VAFundingFeeCashAmount");
            Header.Add("VAFundingFeeIsPFC");

            //Header.Add("PMIInitialMonthlyPremiumAmount");

            //Header.Add("Fee1");
            //Header.Add("Fee2");
            //Header.Add("Fee3");
            //Header.Add("Fee4");
            //Header.Add("Fee5");
            //Header.Add("Fee6");
            //Header.Add("Fee7");
            //Header.Add("Fee8");
            //Header.Add("Fee9");
            //Header.Add("Fee10");
            //Header.Add("Fee11");
            //Header.Add("Fee12");
            //Header.Add("Fee13");
            //Header.Add("Fee14");
            //Header.Add("Fee15");
            //Header.Add("Fee16");
            //Header.Add("Fee17");
            //Header.Add("Fee18");
            //Header.Add("Fee19");
            //Header.Add("Fee20");

            //Header.Add("Fee21");
            //Header.Add("Fee22");
            //Header.Add("Fee23");
            //Header.Add("Fee24");
            //Header.Add("Fee25");
            //Header.Add("Fee26");
            //Header.Add("Fee27");
            //Header.Add("Fee28");
            //Header.Add("Fee29");
            //Header.Add("Fee30");

            //Header.Add("Fee31");
            //Header.Add("Fee32");
            //Header.Add("Fee33");
            //Header.Add("Fee34");
            //Header.Add("Fee35");
            //Header.Add("Fee36");
            //Header.Add("Fee37");
            //Header.Add("Fee38");
            //Header.Add("Fee39");
            //Header.Add("Fee40");

            //Header.Add("Fee41");
            //Header.Add("Fee42");
            //Header.Add("Fee43");
            //Header.Add("Fee44");
            //Header.Add("Fee45");
            //Header.Add("Fee46");
            //Header.Add("Fee47");
            //Header.Add("Fee48");
            //Header.Add("Fee49");
            //Header.Add("Fee50");

            //Header.Add("Fee51");
            //Header.Add("Fee52");
            //Header.Add("Fee53");
            //Header.Add("Fee54");
            //Header.Add("Fee55");
            //Header.Add("Fee56");
            //Header.Add("Fee57");
            //Header.Add("Fee58");
            //Header.Add("Fee59");
            //Header.Add("Fee60");

            //Header.Add("Fee61");
            //Header.Add("Fee62");
            //Header.Add("Fee63");
            //Header.Add("Fee64");
            //Header.Add("Fee65");
            //Header.Add("Fee66");
            //Header.Add("Fee67");
            //Header.Add("Fee68");
            //Header.Add("Fee69");
            //Header.Add("Fee70");

            //Header.Add("Fee71");
            //Header.Add("Fee72");
            //Header.Add("Fee73");
            //Header.Add("Fee74");
            //Header.Add("Fee75");
            //Header.Add("Fee76");
            //Header.Add("Fee77");
            //Header.Add("Fee78");
            //Header.Add("Fee79");
            //Header.Add("Fee80");
            //Header.Add("Fee81");
            //Header.Add("Fee82");
            //Header.Add("Fee83");

            //Header.Add("");
        }
    }
}
