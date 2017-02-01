using System;
using System.Collections.Generic;
using System.Text;

namespace Bling.Domain.IT
{
    public class Inventory
    {
        public virtual int Id { get; private set; }
        public virtual int Quantity { get; set; }
        public virtual string Make { get; set; }
        public virtual string Model { get; set; }
        public virtual string SerialNumber { get; set; }
        public virtual string BranchName { get; set; }        
        public virtual DateTime IssuedOn { get; set; }
        public virtual DateTime AddedOn { get; set; }
        public virtual string IssuedTo { get; set; }
        public virtual string AddedBy { get; set; }

        //public virtual UserInfo IssuedTo { get; set; }
        //public virtual UserInfo AddedBy { get; set; }

        public virtual string Validate()
        {
            if (Make == String.Empty)
                return "Make is required.";

            if (Model == String.Empty)
                return "Model is required.";

            if (SerialNumber == String.Empty)
                return "Serial Number is required.";

            if (Quantity <= 0)
                return "Quantity should be greater than 0.";
            
            if (IssuedTo == String.Empty)
                return "Please select user to assign to.";

            if (BranchName == String.Empty)
                return "Please choose an assign to user to display the branch name.";

            return "";
        }

        public virtual string ToHTMLRow()
        {
            return String.Format("<tr id='inv_{8}'><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td><img id='{8}' alt='Delete' src='/Images/Trash.gif' /></td></tr>",
                IssuedTo == null ? "" : IssuedTo, IssuedOn.ToShortDateString(), Make, Model,
                SerialNumber, Quantity, BranchName, AddedBy == null ? "" : AddedBy,
                Id
            );
        }

        public static string ToHTMLTable(List<Inventory> inventories, int currentPage, int count)
        {
            StringBuilder table = new StringBuilder();
            table.Append("<table>");
            table.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>&nbsp;</td></tr>",
                "Issued To", "Issued On", "Make", "Model", "Serial Number", "Quantity", "Branch Name", "Added By");

            inventories.ForEach(inv => table.Append(inv.ToHTMLRow()));

            table.Append("</table><br /><br />");
            table.Append(new Paginator(currentPage, count));

            return table.ToString();
        }
    }
}
