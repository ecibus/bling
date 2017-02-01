using System;
using System.Collections.Generic;
using System.Text;

namespace Bling.Domain.Secondary
{
    public class LSDTInvestorMapping
    {
        public virtual int Id { get; set; }
        public virtual string LoanSolutionInvestor { get; set; }
        public virtual string DataTracInvestor { get; set; }

        public virtual string ToHtmlRow()
        {
            return String.Format("<tr><td>{0}</td><td>{1}</td></tr>", LoanSolutionInvestor, DataTracInvestor);
        }

        public static string ToHtmlTable(List<LSDTInvestorMapping> data)
        {
            if (data.Count == 0)
                return "";
            
            StringBuilder table = new StringBuilder();

            table.Append("<table>");
            table.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", "Loan Solution Investor", "Data Trac Investor");
            data.ForEach(x => table.Append(x.ToHtmlRow()));
            table.Append("</table>");
            return table.ToString();
        }

        public static string GetCodeFor(string lsInvestor, List<LSDTInvestorMapping> list)
        {
            LSDTInvestorMapping map = list.Find(x => x.LoanSolutionInvestor.ToLower() == lsInvestor.ToLower());
            if (map == null)
                return "";

            return map.DataTracInvestor;
        }
    }
}
