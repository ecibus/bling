using System;
using Bling.Domain.Extension;

namespace Bling.Domain
{
    public class GEMApplication
    {
        public virtual int Id { get; set; }
        public virtual int Parent { get; set; }
        public virtual string ApplicationName { get; set; }
        public virtual string Image { get; set; }
        public virtual string Link { get; set; }
        public virtual bool Include { get; set; }        

        public virtual string GetApplicationAsListItem()
        {
            return String.Format("<li id='App{0}' href='{3}'><img src='{1}' alt='{2}' /><br />{2}</li>",
                    Id, Image, ApplicationName, Link);
        }

        public virtual string GetApplicationAsALink()
        {
            return String.Format("<a href='{0}?a={1}'>{2}</a>", Link, Id, ApplicationName.RemoveHTMLTag());
        }
    }
}
