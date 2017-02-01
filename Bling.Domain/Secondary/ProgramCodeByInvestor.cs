using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Secondary
{
    public class ProgramCodeByInvestor
    {
        public virtual string Investor { get; set; }
        public virtual int Total { get; set; }
        public virtual int Displayed { get; set; }
        public virtual int Hidden { get; set; }
        
        public virtual string ToHtmlRow()
        {
            return String.Format("<tr><td>{0}</td><td>{1:n0}</td><td>{2:n0}</td><td>{3:n0}</td>", Investor, Total, Hidden, Displayed);
        }

        public static string ToHtmlTable(List<ProgramCodeByInvestor> list)
        {
            StringBuilder table = new StringBuilder();
            table.Append("<table class='t1'>");
            table.Append("<tr class='yellow'><td>Investor</td><td>Total Program Code</td><td>Hidden</td><td>Displayed</td>");
            list.ForEach(x => table.Append(x.ToHtmlRow()));
            table.Append("</table>");

            return table.ToString();
        } 
    }
}
