using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain
{
    public class LUPopup
    {
        public virtual int Id { get; set; }
        public virtual string Alias { get; set; }
        public virtual string Description { get; set; }
        public virtual string Type { get; set; }
        public virtual int ColumnOrder { get; set; }
    }
}
