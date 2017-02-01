using System;

namespace Bling.Domain.Extension
{
    public static class DoubleExtension
    {
        public static string ToCurrency(this double d)
        {
            return d.ToString("$ #,0.00;($ #,0.00)");
        }
    }
}
