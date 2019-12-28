using System;
using System.Threading.Tasks;

namespace HH.TestUtils
{
    public static class TaskExtensions
    {
        public static Task<T> TaskResult<T>(this T item)
        {
            return Task.FromResult(item);
        }

        public static Task TaskFromException(this Exception exception)
        {
            return Task.FromException(exception);
        }
    }
}
