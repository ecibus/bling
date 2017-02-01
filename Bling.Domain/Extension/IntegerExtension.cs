using System;

namespace Bling.Domain.Extension
{
    public static class IntegerExtension
    {
        public static int ToValue(this int ? i)
        {
            return i ?? 0;
        }
    }
}
