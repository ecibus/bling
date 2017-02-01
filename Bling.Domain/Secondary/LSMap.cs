using System;
using System.Collections.Generic;
using System.Text;

namespace Bling.Domain.Secondary
{
    public class LSMap
    {
        public virtual int Id { get; set; }
        public virtual string LS_InvestorCode { get; set; }
        public virtual string LS_LoanCode { get; set; }
        public virtual string LS_LoanDescription { get; set; }
        public virtual bool Exclude { get; set; }
        public virtual DateTime ? UpdatedOn { get; set; }
        public virtual string UpdatedBy { get; set; }        
        
        public virtual string ToHtmlCheckBox()
        {
            return String.Format("<input id='{0}' type='checkbox' {1}/>", Id, Exclude ? "checked " : "");
        }

        public virtual string ToHtmlRow()
        {
            return String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", 
                LS_InvestorCode, LS_LoanCode, LS_LoanDescription, ToHtmlCheckBox());
        }

        public static string ToHtmlTable(List<LSMap> list)
        {
            StringBuilder table = new StringBuilder();
            table.Append("<table class='t1'>");
            table.Append("<tr class='yellow'><td>Investor</td><td>Code</td><td>Description</td><td>Hide?</td></tr>");
            list.ForEach(x => table.Append(x.ToHtmlRow()));
            table.Append("</table>");
            return table.ToString();
        }

        public static string BuildInvestorDropdownHtml(List<string> investors)
        {
            StringBuilder dropdown = new StringBuilder();
            dropdown.Append("<select name='ddlInvestor' id='ddlInvestor' class='s1'>");
            dropdown.Append("<option value=''>-- Select Investor to Show or Hide --</option>");
            investors.ForEach(i => dropdown.AppendFormat("<option value='{0}'>{0}</option>", i));
            dropdown.Append("</select>");

            return dropdown.ToString();
        }

        public static string BuildLoanCodeDropdownHtml(List<string> loancode)
        {
            StringBuilder dropdown = new StringBuilder();
            dropdown.Append("<select name='ddlLoanCode' id='ddlLoanCode' class='s1'>");
            dropdown.Append("<option value=''>-- Select Program Code --</option>");
            loancode.ForEach(i => dropdown.AppendFormat("<option value='{0}'>{0}</option>", i));
            dropdown.Append("</select>");

            return dropdown.ToString();
        }
    }
}
