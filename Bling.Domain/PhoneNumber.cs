using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain
{
    public class PhoneNumber
    {
        public virtual string AreaCode { get; set; }
        public virtual string Line { get; set; }

        public override string ToString()
        {
            return String.Format("({0}) {1}", AreaCode, Line);
        }
    }
}
