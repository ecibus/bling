using System;
using System.Collections.Generic;
using System.Text;

namespace Bling.Domain
{
    public class Broker
    {
        public virtual string Id { get; set; }
        public virtual string CostCenter { get; set; }
        public virtual string DBA { get; set; }
        public virtual string Status { get; set; }
        public virtual bool InCorp { get; set; }
        public virtual string BranchType { get; set; }
        public virtual string IDNum { get; set; }

        public static string ToHtmlOptionList(string listId, List<Broker> brokers)
        {
            //StringBuilder html = new StringBuilder();
            //html.AppendFormat("<select id='{0}' name='{0}' size='10'>", listId);
            //brokers.ForEach(broker => html.AppendFormat("<option value='{0}'>({0}) {1}</option>", broker.CostCenter, broker.DBA));
            //html.Append("</select>");
            //return html.ToString();

            return ToHtmlOptionList(listId, brokers, "10");
        }

        public static string ToHtmlOptionList(string listId, List<Broker> brokers, string size)
        {
            StringBuilder html = new StringBuilder();
            html.AppendFormat("<select id='{0}' name='{0}' size='{1}'>", listId, size);
            brokers.ForEach(broker => html.AppendFormat("<option value='{0}'>{0} - {1}</option>", broker.IDNum, broker.DBA));
            html.Append("</select>");
            return html.ToString();
        }

        public static string ToHtmlOptionListWithAll(string listId, List<Broker> brokers, string size)
        {
            StringBuilder html = new StringBuilder();
            html.AppendFormat("<select id='{0}' name='{0}' size='{1}'>", listId, size);
            html.AppendFormat("<option value='{0}'>{0}</option>", "ALL");
            brokers.ForEach(broker => html.AppendFormat("<option value='{0}'>{0}</option>", broker.IDNum));
            html.Append("</select>");
            return html.ToString();
        }

        public static string ToHtmlOptionListWithAll(string listId, List<string> brokers, string size)
        {
            StringBuilder html = new StringBuilder();
            html.AppendFormat("<select id='{0}' name='{0}' size='{1}'>", listId, size);
            html.AppendFormat("<option value='{0}'>{0}</option>", "ALL");
            brokers.ForEach(broker => html.AppendFormat("<option value='{0}'>{0}</option>", broker));
            html.Append("</select>");
            return html.ToString();
        }

    }
}
