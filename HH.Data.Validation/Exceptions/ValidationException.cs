using System;

namespace HH.Data.Validation.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {

        }
    }
}
