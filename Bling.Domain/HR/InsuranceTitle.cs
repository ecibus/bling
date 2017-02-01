using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Bling.Domain.HR
{
    public class InsuranceTitle
    {
        public virtual string YearMonth { get; set; }
        public virtual string Location { get; set; }        
        public virtual string Title1 { get; set; }
        public virtual string Title2 { get; set; }
        public virtual string Title3 { get; set; }
        public virtual string Title4 { get; set; }
        public virtual string Title5 { get; set; }
        public virtual string Title6 { get; set; }
        public virtual string Title7 { get; set; }
        public virtual string Title8 { get; set; }
        public virtual string Title9 { get; set; }
        public virtual string Title10 { get; set; }
        public virtual string Title11 { get; set; }
        public virtual string Title12 { get; set; }

        public static string ToSelectHTML(IList<InsuranceTitle> titles)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<select id='YearMonth'>");
            html.AppendFormat("<option value='{0}'>{0}</option>", "");
            titles.ToList().ForEach(title => html.AppendFormat("<option value='{0}'>{0}</option>", title.YearMonth));
            html.Append("</select>");

            return html.ToString();
        }
    }
}
