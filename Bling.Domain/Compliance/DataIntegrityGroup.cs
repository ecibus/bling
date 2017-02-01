using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;

namespace Bling.Domain.Compliance
{
    public class DataIntegrityGroup
    {
        public virtual int Id { get; set; }
        public virtual string GroupName { get; set; }
        public virtual int OrderBy { get; set; }
        public virtual IList<DataIntegrityField> Fields { get; set; }
        public virtual bool Extra { get; set; }

        public virtual string ToHTML(IList<DIRWData> datum, IList<DIRWDropDown> dropdown, string keyId)
        {
            if (Fields.Count(x => x.Include) == 0)
                return "";

            StringBuilder html = new StringBuilder();            
            html.AppendFormat("<div class=\"fieldgroup\">");
            html.AppendFormat("<h2>{0}</h2>", GroupName);
            foreach (var field in Fields.ToList().Where(x => x.Include))
            //foreach (var field in Fields.ToList())
            {
                if (!String.IsNullOrEmpty(keyId) && field.Id == 48)
                    continue;

                html.AppendFormat(field.ToHTML(datum.Where(y => y.Id == field.Id && y.KeyId == keyId).SingleOrDefault(), DIRWDropDown.GetLookUp(dropdown, field.Id)));
            }
            html.AppendFormat("</div>");
            
            if (Extra)
            {
                html.AppendFormat("<div id=\"Extra_{0}\"></div>", Id);
            }
            return html.ToString();
        }
    }
}
