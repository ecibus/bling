using System;
using System.Collections.Generic;
using System.Text;
using Bling.Domain.Extension;

namespace Bling.Domain.IT
{
    public class InventoryUser
    {
        public virtual string EmployId { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Branch { get; set; }

        public static string ToHTMLDropDown(List<InventoryUser> data)        
        {
            if (data.Count == 0)
                return "";

            StringBuilder dropdown = new StringBuilder();
            dropdown.Append("<select id='ddUser'>");
            dropdown.AppendFormat("<option value=\"|\">-- Please Select -- </option>");
            data.ForEach(u => dropdown.AppendFormat("<option value=\"{0}|{3}\">{1} {2} </option>", 
                u.EmployId, u.FirstName.Capitalize(), u.LastName.Capitalize(), u.Branch));
            dropdown.Append("</select>");

            return dropdown.ToString();
        }
    }
}
