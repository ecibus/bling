using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Compliance
{
    public class AuditScoreCardGroup
    {
        public virtual int Id { get; set; }
        public virtual string GroupName { get; set; }
        public virtual int Ordering { get; set; }
        public virtual bool Include { get; set; }
        public virtual IList<AuditScoreCardItem> Item { get; set; }

        public virtual string ToHtml()
        {
            StringBuilder html = new StringBuilder();

            //html.AppendFormat("<li>{0}", GroupName);
            //html.AppendFormat("<li>{0} (<input type='checkbox' id='chkNF_{1}' /> No Findings) ", GroupName, Id);
            html.AppendFormat("<li><span>{0}</span> {1} ", GroupName, String.IsNullOrEmpty(GroupName) ? "" :
                String.Format("(<input type='checkbox' class='NoFindings' id='chkNF_{0}' /> No Findings)", Id)
                );

            html.Append("<ul>");

            Item
                .Where(x => x.Include)
                .OrderBy(x => x.Ordering)
                .ToList()
                .ForEach(desc => html.Append(desc.ToLIHtml()));

            html.AppendFormat("</ul></li><li class='GroupScore'><span id='total_{0}'>0.00</span></li>", Id.ToString());
            //html.AppendFormat("<li><span class='Comment'>Comment:<br /><textarea id='comment_{0}' cols='50' rows='4'></textarea><br /><input id='btnSave_{0}' type='button' value='Save' /></span></li>", Id.ToString());

            return html.ToString();
        }
    }
}
