using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Bling.Domain.Extension;

namespace Bling.Domain.HR
{
    public class InsuranceEnrollment
    {
        public virtual int Id { get; set; }
        public virtual string YearMonth { get; set; }
        public virtual string Location { get; set; }
        public virtual string EmployeeName { get; set; }
        public virtual string BranchNo { get; set; }
        public virtual DateTime ? BirthDate { get; set; }
        public virtual decimal ? Ins1 { get; set; }
        public virtual decimal ? Ins2 { get; set; }
        public virtual decimal ? Ins3 { get; set; }
        public virtual decimal ? Ins4 { get; set; }
        public virtual decimal ? Ins5 { get; set; }
        public virtual decimal ? Ins6 { get; set; }
        public virtual decimal ? Ins7 { get; set; }
        public virtual decimal ? Ins9 { get; set; }
        public virtual decimal ? Ins10 { get; set; }
        public virtual decimal ? Ins11 { get; set; }
        public virtual decimal? Ins12 { get; set; }
        public virtual string Data { get; set; }
        public virtual decimal ? EmployeeCost { get; set; }
        public virtual bool IsLO { get; set; }

        public static string ToTableRow(IList<InsuranceEnrollment> data)
        {
            StringBuilder html = new StringBuilder();

            data.ToList().ForEach(enroll => html.AppendFormat(
                "<tr>" +
                "<td><img id='{16}' empname='{1}' alt='Delete' src='/Images/Trash.gif' /></td>" +
                "<td>{0}</td>" +
                "<td>{1}</td>" +
                "<td><input id='lo_{16}' class='islo' type='checkbox' {2} /></td>" +
                "<td>{3}</td>" +
                "<td class='center insdata' field='data' recid='{16}'>{4}</td>" +
                "<td class='number insrate' field='Ins1' recid='{16}'>{5:N}</td>" +
                "<td class='number insrate' field='Ins3' recid='{16}'>{6:N}</td>" +
                "<td class='number insrate' field='Ins4' recid='{16}'>{7:N}</td>" +
                "<td class='number insrate' field='Ins5' recid='{16}'>{8:N}</td>" +
                "<td class='number insrate' field='Ins6' recid='{16}'>{9:N}</td>" +
                "<td class='number insrate' field='Ins7' recid='{16}'>{10:N}</td>" +
                "<td class='number insrate' field='Ins9' recid='{16}'>{11:N}</td>" +
                "<td class='number insrate' field='Ins10' recid='{16}'>{12:N}</td>" +
                "<td class='number insrate' field='Ins11' recid='{16}'>{17:N}</td>" +
                "<td class='number insrate' field='Ins12' recid='{16}'>{18:N}</td>" +
                "<td align='right'>{13:N}</td>" +
                "<td align='right' recid='{16}' class='empcost'>{14:N}</td>" +
                "<td align='right'>{15:N}</td>" +
                
                "</tr>", 
                enroll.BranchNo,
                enroll.EmployeeName,
                enroll.IsLO ? "checked" : "",
                enroll.BirthDate.ToDate(),
                enroll.Data,
                enroll.Ins1,
                enroll.Ins3,
                enroll.Ins4,
                enroll.Ins5,
                enroll.Ins6,
                enroll.Ins7,
                enroll.Ins9,
                enroll.Ins10,

                enroll.Ins1 + enroll.Ins3 + enroll.Ins4 + enroll.Ins5 + enroll.Ins6 + enroll.Ins7 + enroll.Ins9 + enroll.Ins10 + enroll.Ins11 + enroll.Ins12, 
                enroll.EmployeeCost,
                enroll.Ins1 + enroll.Ins3 + enroll.Ins4 + enroll.Ins5 + enroll.Ins6 + enroll.Ins7 + enroll.Ins9 + enroll.Ins10 + enroll.Ins11 + enroll.Ins12 - enroll.EmployeeCost,
                enroll.Id,
                enroll.Ins11,
                enroll.Ins12
                ));

            html.AppendFormat("<tr><td colspan='5'><a href='#' id='AddEnrolee'>Add</a></td></tr>");
            return html.ToString();
        }
        
    }
}
