using System;
using System.Collections.Generic;

namespace Bling.Domain.LOS
{
    public class HOPEARule : IHMDARule
    {
        public string Check(List<HMDA> list)
        {
            int count = list.FindAll(hmda => hmda.HOPEA.ToLower() == "yes").Count;

            if (count == 0)
                return "";

            return String.Format("<li>{0} loan{1} contain 'YES' in Hopea</li>", count, count > 1 ? "s" : "");
        }

    }
}
