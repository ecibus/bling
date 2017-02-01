using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Accounting
{
    public class MCREnding
    {
        public virtual int Id { get; set; }
        public virtual string Year { get; set; }
        public virtual string Quarter { get; set; }
        public virtual double Amount { get; set; }
        public virtual int Count { get; set; }
        public virtual double Average { get; set; }
    }
}
