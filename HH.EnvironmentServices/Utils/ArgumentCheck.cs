using System;
using System.Diagnostics;

namespace HH.EnvironmentServices.Utils
{
    public static class ArgumentCheck
    {
        /// <summary>
        /// Checks the argument for null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument">The argument.</param>
        /// <param name="argName">Name of the argument.</param>
        /// <param name="exceptionMessage">The exception message.</param>
        /// <returns>The argument</returns>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public static T ArgumentNullCheck<T>(this T argument, string argName, string exceptionMessage = null) where T : class
        {
            if (argument == null)
            {
                throw string.IsNullOrWhiteSpace(exceptionMessage) ? new ArgumentNullException(argName) : new ArgumentNullException(argName, exceptionMessage);
            }

            return argument;
        }

        /// <summary>
        /// Checks the argument for null.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="argName">Name of the argument.</param>
        /// <param name="exceptionMessage">The exception message.</param>
        /// <returns>The argument.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public static string ArgumentNullOrWhitespaceCheck(this string argument, string argName, string exceptionMessage = null)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw string.IsNullOrWhiteSpace(exceptionMessage) ? new ArgumentNullException(argName) : new ArgumentNullException(argName, exceptionMessage);
            }

            return argument;
        }
    }
}
