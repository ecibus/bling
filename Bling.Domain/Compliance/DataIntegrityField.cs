using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Compliance
{
    public class DataIntegrityField
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
        public virtual string Notes { get; set; }
        public virtual string TargetTable { get; set; }
        public virtual string TargetField { get; set; }
        public virtual string DisplayAs { get; set; }
        public virtual string LinkTable { get; set; }
        public virtual string LinkField { get; set; }
        public virtual string LinkId { get; set; }
        public virtual string LinkCriteria { get; set; }
        public virtual string Field { get; set; }
        public virtual bool Include { get; set; }
        public virtual string ExtraUpdateSP { get; set; }
        public virtual string ExtraTable { get; set; }
        public virtual string ExtraField { get; set; }
        public virtual string ExtraId { get; set; }
        public virtual string ExtraCriteria { get; set; }
        //public virtual DataIntegrityGroup Group { get; set; }

        public virtual string ToHTML(DIRWData data, IList<LookUp> lookup)
        {
            StringBuilder html = new StringBuilder();

            string newValueHtml = GetValueHtml(lookup, data.CurrentData);

            html.AppendFormat("<div class=\"field\">");
            html.AppendFormat("    <input type=\"radio\" name=\"radio_{0}_{3}\" value=\"y\" {1} {2} />", Id, Disabled(data.YN), CheckIfYes(data.YN), data.KeyId);
            html.AppendFormat("    <input type=\"radio\" name=\"radio_{0}_{3}\" value=\"n\" {1} {2} />", Id, Disabled(data.YN), CheckIfNo(data.YN), data.KeyId);

            html.AppendFormat("    <span class=\"fieldcontent\">");
            if (String.IsNullOrEmpty(TargetTable))
            {
                html.AppendFormat("        {0} : <span class=\"currentdata\" {2}>{1}</span>",
                    Description, data == null ? "" : data.CurrentData, MakeId(Id));
            }
            else
            {
                html.AppendFormat("        {0} : <span class=\"currentdata\" id=\"currentdata_{3}\">{1}</span> <span class=\"olddata\" id=\"olddata_{3}\">{2}</span>",
                    Description, data == null ? "" : data.CurrentData, data == null ? "" : data.YN.ToLower() == "n" ? "(" + data.OldData + ")" : "", Id);
            }
            html.AppendFormat("    </span>");

            html.AppendFormat("    <br/ ><span class=\"notes\">{0}", Notes);
            html.AppendFormat("    </span>");
            html.AppendFormat("    <div class=\"fieldchange\">");
            if (newValueHtml != String.Empty)
            {
                html.AppendFormat("        New {0} :<br />", Description);
                html.AppendFormat("        {0}<br />", newValueHtml);
            }
            html.AppendFormat("        <input type=\"checkbox\" {0}/>Elevated <br />", data.Elevated ? "checked=\"checked\"" : "");
            html.AppendFormat("        <a href=\"#\" fieldid=\"{0}\" keyid=\"{1}\" class=\"save\">Save</a>", Id, data.KeyId);
            html.AppendFormat("    </div>");
            //html.AppendFormat("    <div class=\"saveerror msg_{0}_{1} \">", Id, data.KeyId);
            html.AppendFormat("    <div class=\"saveerror msg_{0}\">", Id);
            html.AppendFormat("    </div>");
            html.AppendFormat("</div>");

            return html.ToString();
        }

        private string MakeId(int id)
        {
            string idString = "";
            switch (id)
            {
                case 16:
                    idString = "id=\"CountyCode\"";
                    break;

                case 17:
                    idString = "id=\"MSACode\"";
                    break;

                case 19:
                    idString = "id=\"StateCode\"";
                    break;

                default:
                    break;
            }
            return idString;
        }

        private string Disabled(string yn)
        {
            return yn.ToLower() == "n" ? "disabled=\"disabled\"" : "";
        }

        private string CheckIfYes(string yn)
        {
            return yn.ToLower() == "y" ? "checked=\"checked\"" : "";
        }

        private string CheckIfNo(string yn)
        {
            return yn.ToLower() == "n" ? "checked=\"checked\"" : "";
        }

        private string GetValueHtml(IList<LookUp> lookup, string currentValue)
        {
            string html = "";

            switch (DisplayAs.ToLower())
            {
                case "dropdown":
                    html = LookUp.ToHTMLDropDown(lookup.ToList(), "newvalue_" + Id, currentValue, "newvalue");
                    break;

                case "calendar":
                    html = String.Format("<input type='text' class='calendar newvalue' id='newvalue_{0}' value='{1}' />", Id, currentValue);
                    break;

                case "nothing":
                    html = "";
                    break;

                default:
                    html = String.Format("<input type='text' id='newvalue_{0}' class='newvalue' value='{1}' />", Id, currentValue);
                    break;
            }

            return html;

        }
    }
}
