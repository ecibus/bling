using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Extension
{
    public static class FloatExtension
    {
        public static string To4d(this float ? d)
        {
            return d.HasValue ? d.Value.ToString("0.0000") : "";
        }

        public static decimal ToDecimal(this float ? f)
        {
            return Convert.ToDecimal(f ?? 0);
        }
    }
}
