using System.Collections.Generic;
using Rhino.Mocks;

namespace HH.TestUtils
{
    public static class Extensions
    {
        public static void StubEnumerator<T>(this IEnumerable<T> collection)
        {
            collection.Stub(c => c.GetEnumerator()).Return(new List<T>().GetEnumerator());
        }
        
        public static void StubEnumerator<T>(this IEnumerable<T> collection, IEnumerable<T> values)
        {
            collection.Stub(c => c.GetEnumerator()).Return(new List<T>(values).GetEnumerator());
        }
    }
}
