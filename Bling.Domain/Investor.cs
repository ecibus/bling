using System;
using System.Collections.Generic;
using System.Text;

namespace Bling.Domain
{
    public class Investor
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Inv { get; set; }        
        public virtual bool Exclude { get; set; }

        public virtual string ToOptionHtml(string selected)
        {
            return String.Format("<option value='{0}'{3}>{1} ({2})</option>", 
                Id, Inv, Name, Id.ToLower() == selected.ToLower() ? " selected" : "");
        }

        public static string ToSelectHtml(List<Investor> investors, string id, string selected)
        {
            StringBuilder select = new StringBuilder();
            select.AppendFormat("<select id='{0}' class='s1'>", id);
            select.AppendFormat("<option value='{0}'>{1}</option>", "", " -- Please Select --");
            investors.ForEach(i => select.Append(i.ToOptionHtml(selected)));
            select.Append("</select>");

            return select.ToString();
        }
    }
}
