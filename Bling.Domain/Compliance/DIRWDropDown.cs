using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Compliance
{
    public class DIRWDropDown
    {
        public virtual int Id { get; set; }
        public virtual string Key { get; set; }
        public virtual string Value { get; set; }

        public static IList<LookUp> GetLookUp(IList<DIRWDropDown> set, int id)
        {
            List<LookUp> list = new List<LookUp>();
            set.ToList().Where(x => x.Id == id).OrderBy(x => x.Value)
                .ToList().ForEach(x => list.Add(new LookUp { Value = x.Key, Name = x.Value }));
            return list;
        }
    }
}
