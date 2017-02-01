using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Bling.Domain.HR
{
    public class InsuranceEmployeeInfo
    {
        public virtual string EmpId { get; set; }        
        public virtual string EmployeeName { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual string BranchNo { get; set; }        

        public static string ToOptionHtml(IList<InsuranceEmployeeInfo> list)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<select id='optEmpName'>");
            html.Append("<option value=''></option>");
            list.ToList().ForEach(emp => html.AppendFormat("<option value='{0}'>{1}</option>", emp.BirthDate.ToString("MM/dd/yyyy"), emp.EmployeeName));
            html.Append("</select>");
            return html.ToString();
        }
    }
}
