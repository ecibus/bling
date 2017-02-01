using System.Collections.Generic;

namespace Bling.Domain
{
    public class GEMGroup
    {
        public virtual int Id { get; set; }
        public virtual string GroupName { get; set; }
        public virtual IList<GEMApplication> Applications { get; set; }
    }
}
