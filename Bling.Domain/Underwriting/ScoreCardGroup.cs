using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Underwriting
{
    public class ScoreCardGroup
    {
        public virtual int Id { get; set; }
        public virtual string GroupName { get; set; }
        public virtual int Ordering { get; set; }
        public virtual bool Include { get; set; }
        public virtual IList<ScoreCardDescription> Description { get; set; }

        public virtual string ToHtml()
        {
            StringBuilder html = new StringBuilder();

            //html.AppendFormat("<li>{0}", GroupName);
            html.AppendFormat("<li>{0} (<input type='checkbox' id='chkNF_{1}' /> No Findings) ", GroupName, Id);

            html.Append("<ul>");

            Description
                .Where(x => x.Include)
                .OrderBy(x => x.Ordering)
                .ToList()
                .ForEach(desc => html.Append(desc.ToLIHtml()));
            
            html.AppendFormat("</ul></li><li id='score'><span id='total_{0}'>0</span></li>", Id.ToString());
            html.AppendFormat("<li><span class='Comment'>Comment:<br /><textarea id='comment_{0}' cols='50' rows='4'></textarea><br /><input id='btnSave_{0}' type='button' value='Save' /></span></li>", Id.ToString());

            return html.ToString();
        }
    }
}
