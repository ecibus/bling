using System;
using System.Text;
using System.Collections.Generic;
using Bling.Domain.Extension;

namespace Bling.Domain.HR
{
    public class Job
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Qualification { get; set; }
        public virtual string Description { get; set; }
        public virtual string Salary { get; set; }
        public virtual string Hourly { get; set; }
        public virtual string Benefits { get; set; }
        public virtual string Education { get; set; }
        public virtual string Skills { get; set; }
        public virtual DateTime ? CloseDate { get; set; }
        public virtual DateTime ? PostDate { get; set; }        
        public virtual DateTime ? FillDate { get; set; }
        public virtual DateTime ? StartDate { get; set; }
        public virtual string StartDateText { get; set; }
        public virtual string LocationCity { get; set; }
        public virtual string LocationBranch { get; set; }
        public virtual string FileName { get; set; }
        public virtual string Attachment { get; set; }

        public static string ToSelectHTML(List<Job> jobs)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<select id='Jobs'>");
            html.Append("<option value='0'>-- Create New Position --</option>");
            jobs.ForEach(job => html.AppendFormat("<option value='{0}'>{1}</option>", job.Id, job.Title));
            html.Append("</select>");

            return html.ToString();
        }

        public virtual string ToJson()
        {
            StringBuilder json = new StringBuilder();
            json.Append("data = { ");
            json.AppendFormat("\"Title\" : \"{0}\", ", Title);
            json.AppendFormat("\"Qualification\" : \"{0}\", ", Qualification);
            json.AppendFormat("\"Description\" : \"{0}\", ", Description);
            json.AppendFormat("\"Skills\" : \"{0}\", ", Skills);
            json.AppendFormat("\"Education\" : \"{0}\", ", Education);
            json.AppendFormat("\"Filename\" : \"{0}\", ", FileName);
            json.AppendFormat("\"LocationCity\" : \"{0}\", ", LocationCity);
            json.AppendFormat("\"LocationBranch\" : \"{0}\", ", LocationBranch);
            json.AppendFormat("\"Salary\" : \"{0}\", ", Salary);
            json.AppendFormat("\"Hourly\" : \"{0}\", ", Hourly);
            json.AppendFormat("\"Benefits\" : \"{0}\",", Benefits);
            json.AppendFormat("\"CloseDate\" : \"{0}\", ", CloseDate.ToDate());
            json.AppendFormat("\"PostDate\" : \"{0}\", ", PostDate.ToDate());
            json.AppendFormat("\"FillDate\" : \"{0}\", ", FillDate.ToDate());
            json.AppendFormat("\"StartDate\" : \"{0}\", ", StartDate.ToDate());
            json.AppendFormat("\"StartDateText\" : \"{0}\", ", StartDateText);
            json.AppendFormat("\"Attachment\" : \"{0}\" ", Attachment);

            json.Append(" }");

            return json.ToString().Replace("\n", "").Replace("\r", "");
        }
    }
}
