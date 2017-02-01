using System;

namespace Bling.Domain.Extension
{
    public static class DecimalExtension
    {
        public static string To4d(this decimal ? d)
        {
            return d.HasValue ? d.Value.ToString("0.0000") : "";            
        }

        public static string ToCurrency(this decimal ? d)
        {
            return d.HasValue ?  d.Value.ToString("$ #,0.00;($ #,0.00)") : "";
        }

        public static string ToCurrency(this decimal d)
        {
            return d.ToString("$ #,0.00;($ #,0.00)");
        }

        public static decimal ToValue (this decimal ? d)
        {
            return d ?? 0m;
        }       
        
    }
}
