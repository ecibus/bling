using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain
{
    public class CSVExport
    {
        public virtual int Id { get; set; }
        public virtual string Type { get; set; }
        public virtual string Key { get; set; }
        public virtual string Value { get; set; }
    }
}
