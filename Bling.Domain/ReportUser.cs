using System;
using Bling.Domain.Extension;

namespace Bling.Domain
{
    public class ReportUser
    {
        public virtual string EmployId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual bool IsFunder { get; set; }
        public virtual bool IsUnderwriter { get; set; }
        public virtual bool IsCorp { get; set; }        
        
        public virtual string FullName
        {
            get { return String.Format("{0} {1}", FirstName.Capitalize(), LastName.Capitalize()); }
        }

        
    }
}
