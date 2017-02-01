using System;
using System.Text;

namespace Bling.Domain.Underwriting
{
    public class ScoreCardDescription
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual float Score { get; set; }
        public virtual int Ordering { get; set; }
        public virtual bool Include { get; set; }
        public virtual string ToLIHtml()
        {
            StringBuilder html = new StringBuilder();

            string score = String.Format("<span id='score_{0}' class='score'>{1:0.00}</span>", Id, Score);
            

            html.AppendFormat(
                "<li>{0} <input id='chk_{1}' type='checkbox' /> <span id='ScoreText_{1}' class='ScoreText'>{2}</span></li>", 
                score, Id, Name);

            return html.ToString();
        }
    }
}
