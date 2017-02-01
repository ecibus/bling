using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.HR
{
    public class LOMaster
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }

        public static string ToHTMLDropDown(IList<LOMaster> list)
        {
            StringBuilder dropdown = new StringBuilder();

            dropdown.Append("<select id='ddLOMaster'>");

            list.ToList()
                .ForEach(lo => dropdown.AppendFormat("<option value=\"{0}\" >{0} {1}</option>",
                lo.Code.Trim(), lo.Name.Trim().Length == 0 ? "" : "(" + lo.Name + ")"));

            dropdown.Append("</select>");

            return dropdown.ToString();
        }
    }
}
