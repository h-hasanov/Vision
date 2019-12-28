using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HH.Presentation.Attributes;

namespace HH.Extensions.Enums
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum en)
        {
            var type = en.GetType();
            var memInfo = type.GetRuntimeField(en.ToString());
            if (memInfo == null) return en.ToString();
            var attrs = memInfo.GetCustomAttributes(typeof(LocalizedDescriptionAttribute), false);
            return attrs.Any() ? ((LocalizedDescriptionAttribute)attrs.First()).Description : en.ToString();
        }

        public static IEnumerable<T> GetValues<T>() where T : struct, IComparable, IFormattable
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static TEnum Parse<TEnum>(string value) where TEnum : struct, IComparable, IFormattable
        {
            return (TEnum) Enum.Parse(typeof (TEnum), value);
        }
    }
}
