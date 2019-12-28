using System;

namespace HH.Math.Functions
{
    public static class FactorialFunction
    {
        /// <summary>
        /// Calculate the factorial of the given integer i.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double Factorial(this double n)
        {
            if (n < 0)
            {
                // Factorial is not defined for negative numbers
                throw new ArgumentException("Argument can not be negative", nameof(n));
            }
            return (n + 1.0).Gamma();
        }

        /// <summary>
        /// Returns log(n!)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double LogFactorial(this double n)
        {
            return (n + 1.0).LogGamma();
        }
    }
}
