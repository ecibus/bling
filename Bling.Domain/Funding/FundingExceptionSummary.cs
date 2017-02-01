using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Funding
{
    public class FundingExceptionSummary
    {
        public virtual string BrokerId { get; set; }
        public virtual string Branch { get; set; }
        public virtual int Month { get; set; }
        public virtual int Year { get; set; }
        public virtual int ExceptionCount { get; set; }
        public virtual string Comment { get; set; }

        public static string ToTable(IList<FundingExceptionSummary> list)
        {
            StringBuilder table = new StringBuilder();

            table.Append("<table class='t1'>");

            table.AppendFormat("<tr class='yellow'><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>",
                "Branch", "Month", "Year", "Exception Count"
                );

            list.ToList().ForEach(x => table.Append(x.ToRow()));

            table.Append("</table>");
            return table.ToString();
        }

        public virtual string ToRow()
        {
            StringBuilder row = new StringBuilder();

            row.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>",
                Branch, Month, Year, ExceptionCount
                );
            row.AppendFormat("<tr><td colspan='3'><textarea id='{1}'>{0}</textarea></td><td valign='bottom'>" +
                "<input class='saveComment' type='button' value='Save' id='{1}_{2}_{3}' /></td></tr>", Comment, BrokerId, Month, Year);
            return row.ToString();
        }
    }
}
