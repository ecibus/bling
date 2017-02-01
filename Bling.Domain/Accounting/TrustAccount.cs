using System;
using System.Collections.Generic;
using System.Text;

namespace Bling.Domain.Accounting
{
    public class TrustAccount
    {
        public virtual int Id { get; set; }
        public virtual string ApplicationNumber { get; set; }
        public virtual string Category { get; set; }
        public virtual string Type { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual DateTime ActivityDate { get; set; }
        public virtual double Amount { get; set; }
        public virtual string Notes { get; set; }

        public virtual string ToRow()
        {
            return String.Format("<tr id='doc{0}'><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td><img id='{0}' alt='Delete' src='/Images/Trash.gif' /></td></tr>",
                Id, Category, Type, Date.ToString("MM/dd/yyyy"), Amount.ToString("$ #,###,##0.00;$ -#,###,##0.00"), Notes);
        }
        public static string ConvertListToTable(List<TrustAccount> logs)
        {
            if (logs == null || logs.Count == 0)
                return "No entries found.";

            StringBuilder table = new StringBuilder();
            double total = 0;

            table.Append("<table>");
            table.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>&nbsp;</td></tr>",
                "Trust Category", "Trust Type", "Trust Date", "Trust Amount", "Trust Notes");
            logs.ForEach(trust => table.Append(trust.ToRow()));
            logs.ForEach(trust => total += trust.Amount);
            table.AppendFormat("<tr><td>Total</td><td></td><td></td><td id='total'>{0}</td><td></td><td></td></tr>", total.ToString("$ #,###,##0.00;$ -#,###,##0.00"));
            table.Append("</table>");
            return table.ToString();
        }

        public static implicit operator TrustAccountBackup (TrustAccount trust)
        {
            return new TrustAccountBackup
            {
                ApplicationNumber = trust.ApplicationNumber,
                Category = trust.Category,
                Type = trust.Type,
                Date = trust.Date,
                ActivityDate = trust.ActivityDate,
                Amount = trust.Amount,
                Notes = trust.Notes
            };
        }
    }
}
