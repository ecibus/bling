using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.HR
{
    public class LOAdjustment
    {
        public virtual int Id { get; set; }
        public virtual string LOCode { get; set; }
        public virtual string Description { get; set; }
        public virtual string LoanNumber { get; set; }
        public virtual DateTime DateToPay { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual string Comment { get; set; }

        public static string ToHTMLTable(IList<LOAdjustment> list)
        {
            

            StringBuilder table = new StringBuilder();
            table.Append("<table class='t1'>");
            table.Append("<tr class='yellow'><td>Description</td><td>Loan Number</td><td>Pay Period Ending</td><td class='number'>Amount</td><td>Comment</td><td></td></tr>");

            
            table.Append("");
            table.Append("");

            table.Append("<tr>");
            table.Append("    <td>");
            table.Append("    <select id='optDescription'>");
            table.Append("        <option>Appraisal/Inspection</option>");
            table.Append("        <option>BytePro</option>");
            table.Append("        <option>Credit Report</option>");
            table.Append("        <option>Other</option>");
            table.Append("        <option>ChgOff Appr/Insp</option>");
            table.Append("        <option>ChgOff CrdRpt</option>");
            table.Append("        <option>Insurance</option>");
            table.Append("        <option>Early Commission Payout</option>");
            table.Append("        <option>In House Lead</option>");
            table.Append("    </select>");
            table.Append("    </td>");
            table.Append("    <td><input type='text' class='span-3' id='txtLoanNumber' /></td>");
            table.Append("    <td><input type='text' class='span-3' id='txtPayDate' /></td>");
            table.Append("    <td class='number'><input type='text' class='span-2' id='txtAmount' /></td>");
            table.Append("    <td><input type='text' class='span-5' id='txtComment' /></td>");
            table.Append("    <td><input type='button' id='btnAdd' value='Add' /></td>");
            table.Append("</tr> ");
            /*
            
            
            */ 
            
            list.ToList().ForEach(a => table.Append(a.ToRow()));

            table.Append("</table>");

            return table.ToString();
        }

        public virtual string ToRow()
        {
            return String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td class='number'>{3}</td><td>{4}</td>" +
                "<td><img id='{5}' class='delete-loadjust' alt='Delete' src='/Images/Trash.gif' /></td></tr>",
                Description, LoanNumber, DateToPay.ToShortDateString(), Amount.ToString("C2"), Comment, 
                Id);
        }
    }
}
