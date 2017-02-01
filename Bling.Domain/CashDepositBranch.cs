using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Bling.Domain
{
    public class CashDepositBranch
    {
        public virtual string BranchNo { get; set; }
        public virtual string BranchName { get; set; }

        public static string ToSelectHTML(IList<CashDepositBranch> branches)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<select id='Branch'>");
            html.AppendFormat("<option value='{0}'>{0}</option>", "");
            branches.OrderBy(x => x.BranchNo).ToList().ForEach(branch => html.AppendFormat("<option value='{0}'>{0}</option>", branch.BranchNo));
            html.Append("</select>");

            return html.ToString();
        }
    }
}
