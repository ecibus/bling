using System;
using System.Text.RegularExpressions;

namespace Bling.Domain.Extension
{
    public static class StringExtension
    {
        public static string RemoveHTMLTag(this string s)
        {
            Regex regex = new Regex("<.*?>");
            return regex.Replace(s, "");
        }

        public static bool ToBoolean(this string s)
        {
            if (s == "1")
                return true;
            return false;
        }

        public static int ToInteger(this string s)
        {
            return Convert.ToInt32(s);
        }

        public static decimal ToDecimal(this string s)
        {
            return Convert.ToDecimal(s);
        }

        public static double ToDouble(this string s)
        {
            return Convert.ToDouble(s);
        }

        public static string Capitalize(this string s)
        {
            if (s.Length == 0)
                return s;

            return s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
        }

        public static DateTime ToDateTime(this string s)
        {
            return Convert.ToDateTime(s);
        }

        public static DateTime ? ToNullDateTime(this string s)
        {
            if (s.Trim() == String.Empty)
                return null;

            return Convert.ToDateTime(s);
        }

        public static string Escape(this string s)
        {
            if (s == null)
                return "";
            return s.Replace("\\", "\\\\")
                .Replace("'", "\\'")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                ;
        }

        public static string R(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            string pattern = "[,\"]";
            var regex = new Regex(pattern);
            return regex.Replace(str, "");
        }
    }
}
