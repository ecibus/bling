using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain
{
    public class Translog
    {
        public virtual int Id { get; set; }
        public virtual string FileId { get; set; }
        public virtual string ActorId { get; set; }
        public virtual string Field { get; set; }
        public virtual string OldValue { get; set; }
        public virtual string NewValue { get; set; }
        public virtual DateTime ChangeDate { get; set; }
    }
}
