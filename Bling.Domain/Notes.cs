using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain
{
    public class Notes
    {
        public virtual int Id { get; set; }
        public virtual string FileId { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual string GroupId { get; set; }
        public virtual string ActorId { get; set; }
        public virtual string Note { get; set; }
        public virtual bool ShareWithWebTrac { get; set; }
    }
}
