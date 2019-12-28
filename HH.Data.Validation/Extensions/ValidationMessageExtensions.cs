using System;
using System.Collections.Generic;
using System.Linq;
using HH.Data.Validation.Enums;
using HH.Data.Validation.Exceptions;
using HH.Data.Validation.Interfaces;
using HH.Extensions.Collections.Generic;

namespace HH.Data.Validation.Extensions
{
    public static class ValidationMessageExtensions
    {
        /// <summary>
        /// Throws validation exception with the aggregate of the validation messages if messages exist.
        /// </summary>
        /// <param name="validationMessages"></param>
        public static void ThrowIfError(this IReadOnlyCollection<IValidationMessage> validationMessages)
        {
            var errors = validationMessages.Where(c => c.MessageType == MessageType.Error).ToArray();
            if (errors.Length <= 0) return;
            Throw(validationMessages);
        }

        /// <summary>
        /// Throws validation exception with the aggregate of the validation messages.
        /// </summary>
        /// <param name="validationMessages"></param>
        private static void Throw(this IEnumerable<IValidationMessage> validationMessages)
        {
            var aggregateMessage = validationMessages.Select(c=>c.Text).Flatten(Environment.NewLine);
            throw new ValidationException(aggregateMessage);
        }
    }
}
