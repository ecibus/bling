using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Compliance
{
    public class LPEReason
    {
        public virtual int Id { get; set; }
        public virtual string Type { get; set; }
        public virtual string Reason { get; set; }
        public virtual int OrderBy { get; set; }

        public static IList<LookUp> ToLookUp(IList<LPEReason> set)
        {
            List<LookUp> list = new List<LookUp>();
            set.ToList().ForEach(x => list.Add(new LookUp { Value = x.Id.ToString(), Name = x.Reason }));
            return list;
        }
    }
}
