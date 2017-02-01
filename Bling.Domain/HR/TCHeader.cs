using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.HR
{
    public class TCHeader
    {
        public virtual int Id { get; set; }
        public virtual string EmpId { get; set; }
        public virtual string EmpName { get; set; }
        public virtual string Location { get; set; }
        public virtual string Submitted { get; set; }
    }
}
