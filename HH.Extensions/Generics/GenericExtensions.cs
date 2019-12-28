using System;
using System.Collections.Generic;

namespace HH.Extensions.Generics
{
    public static class GenericExtensions
    {
        public static TR Parse<T, TR>(this T obj)
        {
            var typeOfTr = typeof(TR);
            if (typeOfTr == typeof(double))
                return (TR)(object)(Convert.ToDouble(obj));
            if (typeOfTr == typeof(int))
                return (TR)(object)(Convert.ToInt32(obj));
            if (typeOfTr == typeof(string))
                return (TR)(object)(obj.ToString());
            throw new NotImplementedException();
        }

        public static bool AreEqual<T>(T reference, T value)
        {
            return EqualityComparer<T>.Default.Equals(reference, value);
        }
    }
}
