using System;

namespace Bling.Domain.Extension
{
    public static class DateTimeExtension
    {
        public static string ToDate(this DateTime ? dt)
        {
            return dt.HasValue ? dt.Value.ToString("MM/dd/yyyy") : "";
        }
    }
}
