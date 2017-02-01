using System;
using System.Collections.Generic;
using System.Text;
using Bling.Domain.Extension;

namespace Bling.Domain.Accounting
{
    public class DocTrustRunHistory
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual string TransferDate { get; set; }
        public virtual string AsOf { get; set; }
        public virtual string CreatedBy { get; set; }

        public virtual string ToRow()
        {
            return String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", 
                CreatedOn.ToString(), TransferDate, AsOf, CreatedBy.Capitalize());            
        }

        public static string ToHtmlTable(List<DocTrustRunHistory> lists)
        {
            if (lists == null || lists.Count == 0)
                return "";

            StringBuilder table = new StringBuilder();
            table.Append("<table>");
            table.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>",
                "Date", "Transfer Date", "As Of", "Run By");
            lists.ForEach(history => table.Append(history.ToRow()));
            table.Append("</table>");
            return table.ToString();
        }
    }
}
