using System;
using System.Collections.Generic;

namespace Bling.Domain
{
    public class GEMUser
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string ActorId { get; set; }
        public virtual string EmployId { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual IList<GEMGroup> Groups { get; set; }
    }
}
