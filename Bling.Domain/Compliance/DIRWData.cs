using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Compliance
{
    public class DIRWData
    {
        public virtual int Id { get; set; }
        public virtual string CurrentData { get; set; }
        public virtual string OldData { get; set; }
        public virtual bool Elevated { get; set; }
        public virtual string YN { get; set; }
        public virtual string KeyId { get; set; }
    }
}
