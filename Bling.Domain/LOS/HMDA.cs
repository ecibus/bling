using System;
using System.Collections.Generic;
using System.IO;

namespace Bling.Domain.LOS
{
    public class HMDA
    {
        public virtual int Id { get; set; }
        public virtual string LoanNumber { get; set; }
        public virtual string SubmissionDate { get; set; }
        public virtual string ProgramName { get; set; }
        public virtual string LoanType { get; set; }
        public virtual string PropertyType { get; set; }
        public virtual string HMDALoanPurpose { get; set; }
        public virtual string OwnerOccupied { get; set; }
        public virtual string LoanAmount { get; set; }
        public virtual string PreApproval { get; set; }
        public virtual string ActionType { get; set; }
        public virtual string ActionDate { get; set; }
        public virtual string PropertyStreetNo { get; set; }
        public virtual string PropertyStreet { get; set; }
        public virtual string PropertyCity { get; set; }
        public virtual string County { get; set; }
        public virtual string PropertyState { get; set; }
        public virtual string PropertyZipCode { get; set; }
        public virtual string CensusTract { get; set; }
        public virtual string BorrowerRace { get; set; }
        public virtual string CoborrowerRace { get; set; }
        public virtual string BorrowerSex { get; set; }
        public virtual string CoborrowerSex { get; set; }
        public virtual string BorrowerEthnicity { get; set; }
        public virtual string SpouseEthnicity { get; set; }
        public virtual string BorrowerIncome { get; set; }
        public virtual string CoborrowerIncome { get; set; }
        public virtual string TypeOfPurchaser { get; set; }
        public virtual string HmdaDenialReason { get; set; }
        public virtual string RateSpread { get; set; }
        public virtual string TreasuryYield { get; set; }
        public virtual string APRFromTIL { get; set; }
        public virtual string LoanTerm { get; set; }
        public virtual string Lien { get; set; }
        public virtual string LockDateTime { get; set; }
        public virtual string HOPEA { get; set; }
        public virtual string Investor { get; set; }
        public virtual string Underwriter { get; set; }
        public virtual string APRDenialRace { get; set; }
        public virtual string LoanTermType { get; set; }
        public virtual string OriginalAPRTerm { get; set; }
        public virtual string BorrowerFirstName { get; set; }
        public virtual string BorrowerMiddleName { get; set; }
        public virtual string BorrowerLastName { get; set; }
        public virtual string CoBorrowerFirstName { get; set; }
        public virtual string CoBorrowerMiddleName { get; set; }
        public virtual string CoBorrowerLastName { get; set; }

        public static void SaveAsCSV(List<HMDA> data, string filename)
        {
            using (var writer = new StreamWriter(filename))
            {
                writer.WriteLine("Loan Number,Submission Date,PROG_NAME,Loan Type,Property Type,HMDA Loan Purpose,Owner Occupied,Loan Amount,PreApproval,Action Type,Action Date,Property Street No,Property Street,Property City,County,Property State,Property Zip Code,Census Tract,Borrower Race,Coborrower Race,Borrower Sex,Coborrower Sex,Borrower Ethnicity,Spouse Ethnicity,Borrower Income,Coborrower Income,Type of Purchaser,HMDA Denial Reason,Rate Spread,Treasury Yield,APR From TIL,Loan Term,lien,Lock Date/Time,HOPEA,investor,underwriter,Loan Term Type,Original APR Term, Borrower First Name, Borrower Middle Name, Borrower Last Name, Co-Borrower First Name, Co-Borrower Middle Name, Co-Borrower Last Name");
                data.ForEach(hmda => writer.WriteLine(hmda.ToString()));
            }
        }

        public static string ValidateDataAgainstRule(List<HMDA> list)
        {
            HMDAVerify verify = new HMDAVerify(list);

            verify.RegisterRule(new HOPEARule());
            verify.RegisterRule(new LoanCountRule());
            
            return verify.GetWarningMessage();
        }


        public override string ToString()
        {
            return
                String.Format(
                "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}," +
                "{10},{11},{12},{13},{14},{15},{16},{17},{18},{19}," +
                "{20},{21},{22},{23},{24},{25},{26},{27},{28},{29}," +
                "{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44}",
                LoanNumber, SubmissionDate, ProgramName, LoanType, PropertyType,
                HMDALoanPurpose, OwnerOccupied, LoanAmount, PreApproval, ActionType,
                ActionDate, PropertyStreetNo, PropertyStreet, PropertyCity, County,
                PropertyState, PropertyZipCode, CensusTract, BorrowerRace, CoborrowerRace,
                BorrowerSex, CoborrowerSex, BorrowerEthnicity, SpouseEthnicity, BorrowerIncome,
                CoborrowerIncome, TypeOfPurchaser, HmdaDenialReason, RateSpread, TreasuryYield,
                APRFromTIL, LoanTerm, Lien, LockDateTime, HOPEA, 
                Investor,Underwriter, LoanTermType, OriginalAPRTerm,BorrowerFirstName,BorrowerMiddleName,BorrowerLastName,
                CoBorrowerFirstName, CoBorrowerMiddleName, CoBorrowerLastName
                );                
        }
    }
}
