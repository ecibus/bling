using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Compliance
{
    public class AuditScoreCardItem
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
        public virtual float Score { get; set; }
        public virtual int Ordering { get; set; }
        public virtual bool Include { get; set; }
        public virtual bool CommentBox { get; set; }
        public virtual string ItemType { get; set; }
        public virtual bool AlwaysZero { get; set; }

        public virtual string ToLIHtml()
        {
            StringBuilder html = new StringBuilder();

            string score = String.Format("<span id='score_{0}' class='score {2}'>{1:0.00}</span>", Id, Score, Score == 0 && !AlwaysZero ? "GetScore" : "");

            string commentTextArea = CommentBox ? String.Format("<div class='span-24'><b>Comment:</b><br /><textarea rows='4' cols='40' class='span-10 Comment' id='comment_{0}'></textarea></div>", Id) : "";
            string itemTypeDropDown = ItemType == "" ? String.Format("<span class='span-24'><b>Type:</b>&nbsp;<select id='itemtype_{0}' class='ItemTypeDropDown'><option></option><option>PTD</option><option>PTF</option><option>FYI</option></select></span>", Id) : "";
            string saveButton = (CommentBox || ItemType == "") ? String.Format("<span class='span-24'><input type='button' value='Save' class='SaveCommentAndType' id='saveCommentAndType_{0}'/></span>", Id) : "";
            html.AppendFormat(
                "<li>{0} <input id='chk_{1}' type='checkbox' class='CheckItem {3} {4}' /> <span id='ScoreCardItem_{1}' class='ScoreItem'>{2}</span>{5}{6}{7}</li>",
                score, Id, Description, CommentBox ? "CommentBox" : "", ItemType == "" ? "ItemType" : "",
                commentTextArea, itemTypeDropDown, saveButton);

            return html.ToString();
        }
    }
}
