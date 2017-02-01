using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Accounting
{
    public class Branch
    {
        public virtual int BranchCode { get; set; }
        public virtual string BranchName { get; set; }
        public virtual bool Active { get; set; }
                
        public static string ToHtmlOptionList(string listId, List<Branch> branches)
        {
            StringBuilder html = new StringBuilder();
            html.AppendFormat("<select id='{0}' name='{0}' size='10'>", listId);
            branches.ForEach(branch => html.AppendFormat("<option value='{0}'>({0}) {1}</option>", branch.BranchCode, branch.BranchName));
            html.Append("</select>");
            return html.ToString();
        }
    }
}
