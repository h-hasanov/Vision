using System;
using System.Reflection;

namespace HH.Extensions.Types
{
    public static class TypeExtensions
    {
        public static readonly Type BoolType = typeof(bool);
        public static readonly Type Int32Type = typeof(int);
        public static readonly Type DoubleType = typeof(double);
        public static readonly Type DateTimeType = typeof(DateTime);
        public static readonly Type StringType = typeof(string);

        public static object GetDefault(this Type type)
        {
            return Activator.CreateInstance(type);
        }

        public static bool IsInstanceOfType(this Type type, object obj)
        {
            return obj != null && type.GetTypeInfo().IsAssignableFrom(obj.GetType().GetTypeInfo());
        }

        public static bool CanConvertFromString(this Type type, string value)
        {
            try
            {
                var result = Convert.ChangeType(value, type);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static T ConvertFromString<T>(string value, IFormatProvider formatProvider)
        {
            return (T)Convert.ChangeType(value, typeof (T), formatProvider);
        }
    }
}
