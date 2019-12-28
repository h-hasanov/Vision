using System;
using System.Diagnostics;

namespace HH.Math.Optimization.Implementations
{
    [DebuggerNonUserCode]
    public class NonConvergenceException : Exception
    {
        public NonConvergenceException() : base("Failed to converge")
        {
        }

        public NonConvergenceException(string message) : base(message)
        {
        }
    }
}