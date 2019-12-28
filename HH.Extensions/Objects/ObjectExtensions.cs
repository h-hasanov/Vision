using System;
using HH.Extensions.Types;

namespace HH.Extensions.Objects
{
    public static class ObjectExtensions
    {
        public static bool IsDefault(this object value, Type t)
        {
            var defaultValue = t.GetDefault();
            var areEqual = Equals(value, defaultValue);
            return areEqual;
        }
    }
}
