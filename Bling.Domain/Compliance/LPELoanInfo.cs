using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Extension;

namespace Bling.Domain.Compliance
{
    public class LPELoanInfo
    {
        public virtual string LoanNumber { get; set; }
        public virtual string LinkedLoan { get; set; }
        public virtual string Borrower { get; set; }
        public virtual string LoanType { get; set; }
        public virtual double LoanAmount { get; set; }
        public virtual double LoanAmountFirstLien { get; set; }
        public virtual double GEMLoanFeeCharged { get; set; }
        public virtual double LoanOriginationFeeCharged { get; set; }
        public virtual double LoanOfficerPrice { get; set; }
        public virtual double BorrowerPaidDiscount { get; set; }
        public virtual double LenderCredit { get; set; }
        public virtual string FICOScore { get; set; }
        public virtual DateTime ApplicationDate { get; set; }
        public virtual DateTime LockedDate { get; set; }
        public virtual int NoOfBorrower { get; set; }
        public virtual string ProgramType { get; set; }
        public virtual string TransactionType { get; set; }
        public virtual string Reason { get; set; }
        public virtual string Comment { get; set; }
        public virtual bool ReadyForDocs { get; set; }
        public virtual string ReadyForDocsBy { get; set; }
        public virtual bool InitialReview { get; set; }
        public virtual DateTime InitialReviewOn { get; set; }
        public virtual string InitialReviewBy { get; set; }
        public virtual double NetPricePoint
        {
            get
            {
                float loanFee = LoanType.ToUpper() == "CONV" ? 2000 : 2250;

                double netPriceAmount = GEMLoanFeeCharged - loanFee +
                    LoanOriginationFeeCharged +
                    (LoanAmountFirstLien * (LoanOfficerPrice - 100) / 100) +
                    BorrowerPaidDiscount -
                    (LoanAmount / 100) +
                    LenderCredit;

                return netPriceAmount * 100 / LoanAmount;
            }
        }

        public virtual string Message
        {
            get
            {
                if (NetPricePoint < -.5)
                    return "<span class='box_notice'>Underage - Requires Documentation and narrative summary.</span>";
                
                if (NetPricePoint <= 0.5)
                    return "<span class='box_success'>Documentation not required.</span>";

                if (NetPricePoint > 0.5 && NetPricePoint <= 1.0)
                    return "<span class='box_notice'>Overage - Requires Documentation and narrative summary.</span>";

                return "<span class='box_error'>Overage - Exceeds maximum ceiling price.</span>";
            }
        }
        public static string ToTable(IList<LPELoanInfo> list)
        {
            StringBuilder table = new StringBuilder();

            table.Append("<table class='t1'>");

            table.AppendFormat("<tr class='yellow'><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td width='200'>{5}</td></tr>",
                "Loan Number", "Borrower", "Loan Type", "Loan Amount", "Reason", "Comment"
                );

            list.ToList().ForEach(x => table.Append(x.ToRow()));

            table.Append("</table>");
            return table.ToString();
        }

        public virtual string ToRow()
        {
            StringBuilder row = new StringBuilder();

            row.AppendFormat("<tr {6}><td>{0}</td><td>{1}</td><td>{2}</td><td class='number'>{3}</td><td>{4}</td><td width='200'>{5}</td></tr>",
                LoanNumber, Borrower, LoanType, LoanAmount.ToCurrency(), Reason, Comment,
                (NetPricePoint >= -0.5 && NetPricePoint <= 0.5) ? "class='success'" : ""
                );

            return row.ToString();
        }

        //public virtual string ToJson(IList<LookUp> reasons)
        public virtual string ToJson(IList<LPEReason> reasons)
        {
            StringBuilder json = new StringBuilder();

            json.AppendFormat(" {{ ");

            json.AppendFormat(" \"LoanNumber\" : \"{0}\", ", LoanNumber);
            json.AppendFormat(" \"LinkedLoan\" : \"{0}\", ", LinkedLoan);
            json.AppendFormat(" \"Borrower\" : \"{0}\", ", Borrower);
            json.AppendFormat(" \"LoanType\" : \"{0}\", ", LoanType);
            json.AppendFormat(" \"LoanAmount\" : \"{0}\", ", LoanAmount.ToCurrency());
            json.AppendFormat(" \"GEMLoanFeeCharged\" : \"{0}\", ", GEMLoanFeeCharged.ToCurrency());
            json.AppendFormat(" \"LoanOriginationFeeCharged\" : \"{0}\", ", LoanOriginationFeeCharged.ToCurrency());
            json.AppendFormat(" \"LoanOfficerPrice\" : \"{0:00.0000}\", ", LoanOfficerPrice);
            json.AppendFormat(" \"BorrowerPaidDiscount\" : \"{0}\", ", BorrowerPaidDiscount.ToCurrency());
            json.AppendFormat(" \"LenderCredit\" : \"{0}\", ", LenderCredit.ToCurrency());
            json.AppendFormat(" \"FICOScore\" : \"{0}\", ", FICOScore);
            json.AppendFormat(" \"ApplicationDate\" : \"{0}\", ", ApplicationDate.ToShortDateString());
            json.AppendFormat(" \"LockedDate\" : \"{0}\", ", LockedDate.ToShortDateString());
            json.AppendFormat(" \"NoOfBorrower\" : \"{0}\", ", NoOfBorrower);
            json.AppendFormat(" \"ProgramType\" : \"{0}\", ", ProgramType);
            json.AppendFormat(" \"TransactionType\" : \"{0}\", ", TransactionType);
            json.AppendFormat(" \"FinalNetPricePoint\" : \"{0:0.000}\", ", NetPricePoint);
            json.AppendFormat(" \"EvaluatorMessage\" : \"{0}\", ", Message);


            if ((NetPricePoint) < -.5 || (NetPricePoint > 0.5 && NetPricePoint <= 1.0))
            {

                string reason = String.Format("<label>Reason</label><br />{0}<br />" +
                    "<label>Comment</label><br /><textarea id='Comment'>{1}</textarea><br />" +
                    "<input id='btnSave' type='button' value='Save Reason and Comment' /><br />",
                    LookUp.ToHTMLDropDown(LPEReason.ToLookUp(
                    reasons.Where(x => x.Type == (Message.Contains("Overage") ? "O" : "U")).ToList()
                        ).ToList(), "ddlReason", Reason, "").Replace("\"", "'"),
                    Comment
                    );

                json.AppendFormat(" \"Reason\" : \"{0}\", ", reason);
            }

            json.AppendFormat(" \"ReviewComplete\" : \"{0}\" ", InitialReviewHTML());
            
            json.AppendFormat(" }} ");
            
            return json.ToString();
        }

        public virtual string InitialReviewHTML()
        {
            if (InitialReview)
            {
                return String.Format("<div class='box_success'>Initially Reviewed on {0} by {1}</div>", InitialReviewOn, InitialReviewBy);
            }

            return "<input id='btnReviewComplete' type='button' value='Initial Review Complete' />";

        }

        public virtual string ToForm(IList<LookUp> reasons)
        {
            return "";

            StringBuilder form = new StringBuilder();

            form.AppendFormat("<label>{0}</label><div id='LoanNumber'>{1}</div>", "Loan Number:", LoanNumber);
            form.AppendFormat("<label>{0}</label><div>{1}</div>", "Borrower:", Borrower);
            form.AppendFormat("<label>{0}</label><div>{1}</div>", "Loan Type:", LoanType);
            form.AppendFormat("<label>{0}</label><div>{1}</div>", "Loan Amount:", LoanAmount.ToCurrency());
            form.AppendFormat("<label>{0}</label><div>{1}</div>", "GEM Loan Fee Charged:", GEMLoanFeeCharged.ToCurrency());
            form.AppendFormat("<label>{0}</label><div>{1}</div>", "Loan Origination Fee Charged:", LoanOriginationFeeCharged.ToCurrency());
            form.AppendFormat("<label>{0}</label><div>{1:00.0000}</div>", "Loan Officer Price:", LoanOfficerPrice);
            form.AppendFormat("<label>{0}</label><div>{1}</div>", "Borrower Paid Discount:", BorrowerPaidDiscount.ToCurrency());
            form.AppendFormat("<label>{0}</label><div>{1}</div>", "Lender Credit:", LenderCredit.ToCurrency());
            form.AppendFormat("<label>{0}</label><div>{1}</div>", "FICO Score:", FICOScore );
            form.AppendFormat("<label>{0}</label><div>{1}</div>", "Application Date:", ApplicationDate.ToShortDateString());
            form.AppendFormat("<label>{0}</label><div>{1}</div>", "Locked Date:", LockedDate.ToShortDateString());
            form.AppendFormat("<label>{0}</label><div>{1}</div>", "No of Borrower:", NoOfBorrower);
            form.AppendFormat("<label>{0}</label><div>{1}</div>", "Program Type:", ProgramType);
            form.AppendFormat("<label>{0}</label><div>{1}</div>", "Transaction Type:", TransactionType );
            form.AppendFormat("<label>{0}</label><div>{1}</div>", "Ready for Docs:", 
                String.Format("<input type='checkbox' id='chkReadyForDocs' value='1' {0}/>", ReadyForDocs ? "checked='checked'" : ""));
            form.Append("<br />");
            form.AppendFormat("<label>{0}</label><div>{1:0.000}</div>", "Final Net Price (%):", NetPricePoint);
            form.AppendFormat("<br />{0}<br />", Message);


            if ((NetPricePoint) < -.5 || (NetPricePoint > 0.5 && NetPricePoint <= 1.0))
            {
                form.AppendFormat("<label>Reason</label><br />{0}<br />", 
                    LookUp.ToHTMLDropDown(reasons.ToList(), "ddlReason", Reason, ""));
                form.AppendFormat("<label>Comment</label><br /><textarea id='Comment'>{0}</textarea><br />", 
                    Comment);
                form.AppendFormat("<input id='btnSave' type='button' value='Save Reason and Comment' />");
            }

            form.AppendFormat("<br /><br /><input id='btnReviewComplete' type='button' value='Initial Review Complete' />");

            return form.ToString();
        }
    }
}
