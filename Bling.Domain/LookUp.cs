using System;
using System.Text;
using System.Collections.Generic;

namespace Bling.Domain
{
    public class LookUp
    {
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }     
   
        public static string ToHTMLDropDown(List<LookUp> data, string id)
        {
            if (data.Count == 0)
                return "";

            StringBuilder dropdown = new StringBuilder();
            dropdown.AppendFormat("<select id='{0}'>", id);
            dropdown.AppendFormat("<option value=\"\">-- Please Select -- </option>");

            data.ForEach(u => dropdown.AppendFormat("<option value=\"{0}\">{1}</option>",
                u.Value.Trim(), u.Name.Trim()));
            dropdown.Append("</select>");

            
            return dropdown.ToString();
        }

        public static string ToHTMLDropDown(List<LookUp> data, string id, string selected, string c)
        {
            if (data.Count == 0)
                return "";

            if (String.IsNullOrEmpty(selected))
                selected = "";

            StringBuilder dropdown = new StringBuilder();
            dropdown.AppendFormat("<select id='{0}' class='{1}'>", id, c);
            dropdown.AppendFormat("<option value=\"\">-- Please Select -- </option>");

            data.ForEach(u => dropdown.AppendFormat("<option value=\"{0}\" {2}>{1}</option>",
                u.Value, u.Name, u.Name.ToLower() == selected.ToLower() ? "selected=\"selected\"" : ""));
            dropdown.Append("</select>");


            return dropdown.ToString();
        }
    }
}
