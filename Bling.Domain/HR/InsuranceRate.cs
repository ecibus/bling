using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Bling.Domain.HR
{
    public class InsuranceRate
    {
        public virtual int Id { get; set; }
        public virtual string Location { get; set; }
        public virtual string InsuranceType { get; set; }
        public virtual decimal Rate { get; set; }
        public virtual string Data { get; set; }

        public static string ToSelectHTML(IList<InsuranceRate> rates, decimal currentValue)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<select id='InsuranceRates'>");
            html.AppendFormat("<option value='{0}'>{1}</option>", "0", "0.00");
            rates.ToList().ForEach(rate => html.AppendFormat("<option value='{0:N}' {1}>{0:N}</option>", rate.Rate, rate.Rate == currentValue ? "selected" : ""));
            html.Append("</select>");

            return html.ToString();
        }

        public static string ToSelectHTML(IList<string> status, string currentValue)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<select id='EEStatus'>");
            html.AppendFormat("<option value='{0}'>{1}</option>", "", "");
            status.ToList().ForEach(s => html.AppendFormat("<option value='{0}' {1}='{1}'>{0}</option>", s, s == currentValue ? "selected" : ""));
            html.Append("</select>");

            return html.ToString();
        }
        
    }
}
