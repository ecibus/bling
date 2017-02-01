using System;
using System.Text;
using System.Collections.Generic;

namespace Bling.Domain.HR
{
    public class ExpiredLicense
    {
        public virtual string EmployeeId { get; set; }
        public virtual string EmployeeName { get; set; }        
        public virtual string Branch { get; set; }
        public virtual string BranchNo { get; set; }
        public virtual string EMail { get; set; }
        public virtual string BranchEMail { get; set; }        
        public virtual DateTime ExpirationDate { get; set; }
        public virtual DateTime HireDate { get; set; }
        
        public static string ToHTMLTable(List<ExpiredLicense> list)
        {
            StringBuilder table = new StringBuilder();
            table.Append("<table>");
            table.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>",
                "Branch No", "Employee Id", "Employee Name", "Branch", "Expiration Date", "Hire Date"
                );
            
            list.ForEach(x => table.Append(x.ToHTMLRow()));
            table.Append("</table>");
            return table.ToString();
        }
        
        public virtual string ToHTMLRow()
        {
            return String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>", 
                BranchNo, EmployeeId, EmployeeName, Branch,
                ExpirationDate.ToString("MM/dd/yyyy") == "01/01/1900" ? "" : ExpirationDate.ToString("MM/dd/yyyy"), 
                HireDate.ToString("MM/dd/yyyy"));
        }
    }
}
