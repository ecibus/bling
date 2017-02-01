using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Funding
{
    public class CustomerBankData
    {
        public virtual string OriginatorLoanNumber { get; set; }
        public virtual string MortgageeLoanName { get; set; }
        public virtual string OriginatorProductCode { get; set; }
        public virtual string RequestedDate { get; set; }
        public virtual string NoteAmount { get; set; }
        public virtual string NoteDate { get; set; }
        public virtual string NoteTerm { get; set; }
        public virtual string NoteRate { get; set; }
        public virtual string Borrower1FirstName { get; set; }
        public virtual string Borrower1LastName { get; set; }
        public virtual string Borrower1SSN { get; set; }
        public virtual string Borrower2FirstName { get; set; }
        public virtual string Borrower2LastName { get; set; }
        public virtual string Borrower2SSN { get; set; }
        public virtual string Borrower3FirstName { get; set; }
        public virtual string Borrower3LastName { get; set; }
        public virtual string Borrower3SSN { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string USState { get; set; }
        public virtual string Zip { get; set; }
        public virtual string County { get; set; }
        public virtual string ClosingAgentPrimaryBankABARoutingNo { get; set; }
        public virtual string ClosingAgentFinalBankAccountNumber { get; set; }
        public virtual string ClosingAgentOrderNumber { get; set; }
        public virtual string AmortizationType { get; set; }
        public virtual string DocumentationType { get; set; }
        public virtual string LoanType { get; set; }
        public virtual string MERSNumber { get; set; }
        public virtual string LienPosition { get; set; }
        public virtual string Occupancy { get; set; }
        public virtual string PropertyType { get; set; }
        public virtual string RateType { get; set; }
        public virtual string TransactionType { get; set; }
        public virtual string AppraisedValue { get; set; }
        public virtual string CreditScore {get;set;}
        public virtual string WarehousePrincipal { get; set; }
        public virtual string FundToSendAmount { get; set; }
        public virtual string InvestorName { get; set; }



    }
}
