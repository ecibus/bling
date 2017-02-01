using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain
{
    public class Program
    {
        public virtual string Id { get; set; }
        public virtual string ProgramName { get; set; }
        public virtual string ShortDescription { get; set; }
        public virtual string LongDescription { get; set; }
        public virtual string ProgramType { get; set; }
        public virtual string LoanType { get; set; }
        
    }
}
