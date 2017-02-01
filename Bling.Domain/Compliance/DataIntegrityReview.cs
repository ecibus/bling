using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Compliance
{
    public class DataIntegrityReview
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual string FileId { get; set; }
        public virtual string ActorId { get; set; }
        public virtual string Notes { get; set; }
    }
}
