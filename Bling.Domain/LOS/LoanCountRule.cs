using System;
using System.Collections.Generic;

namespace Bling.Domain.LOS
{
    public class LoanCountRule : IHMDARule
    {
        public string Check(List<HMDA> list)
        {
            if (list.Count < 20001)
                return "";

            return String.Format("<li>Loan contains {0:0,000}.  The APR and Denial Workbook only support up to 20000 loans.</li>", list.Count);
        }
    }
}
