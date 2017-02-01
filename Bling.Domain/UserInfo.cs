using System;
using Bling.Domain.Extension;

namespace Bling.Domain
{
    public class UserInfo
    {
        public virtual string EmployId { get; set; }
        public virtual string ActorId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string EMail { get; set; }
        public virtual string NMLSNo { get; set; }
        public virtual bool IsUnderwriter { get; set; }
        public virtual bool IsFunder { get; set; }
        public virtual bool Exclude { get; set; }
        public virtual bool IsLicensedUser { get; set; }        
        public virtual DateTime ? HireDate { get; set; }
        public virtual DateTime ? TerminationDate { get; set; }        

        public virtual Actor Actor { get; set; }
        public virtual Broker Broker { get; set; }
                
        public virtual string FullName
        {
            get {return String.Format("{0} {1}", FirstName.Capitalize(), LastName.Capitalize());}            
        }

        public static implicit operator ReportUser(UserInfo userinfo)
        {
            return new ReportUser
            {
                EmployId = userinfo.EmployId,
                FirstName = userinfo.FirstName,
                LastName = userinfo.LastName,
                IsCorp = true,
                IsFunder = false,
                IsUnderwriter = false
            };
        }
    }
}
