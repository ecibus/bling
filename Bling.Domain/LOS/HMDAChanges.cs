using System;
using System.Collections.Generic;
using System.Text;
using Bling.Domain;
using Bling.Domain.Extension;

namespace Bling.Domain.LOS
{
    public class HMDAChanges
    {
        public virtual int Id { get; set; }
        public virtual string ReportYear { get; set; }
        public virtual string LoanNumber { get; set; }
        public virtual string FieldName { get; set; }
        public virtual string NewData { get; set; }
                
        public virtual DateTime CreatedOn { get; set; }
        public virtual GEMUser GEMUser { get; set; }

        //public virtual string CreatedBy { get; set; }
        //public virtual int GEMUserId { get; set; }
        //public virtual Actor Actor { get; set; }

        public virtual string ToRow()
        {
            return String.Format("<tr id='hmda{0}'><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td><img id='{0}' alt='Delete' src='/Images/Trash.gif' /></td></tr>",
                Id, CreatedOn, LoanNumber, FieldName, NewData, GEMUser == null ? "" : GEMUser.UserInfo.FirstName.Capitalize());

        }
        public static string ConvertListToTable(List<HMDAChanges> changes)
        {
            if (changes == null || changes.Count == 0)
                return "";

            StringBuilder table = new StringBuilder();
            table.Append("<table>");
            table.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>&nbsp;</td></tr>",
                "Date", "Loan Number", "Field Name", "New Value", "Added By");
            changes.ForEach(hmda => table.Append(hmda.ToRow()));
            table.Append("</table>");            
            return table.ToString();
        }
    }
}
