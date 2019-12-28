using System;
using System.Collections.Generic;
using System.Linq;
using HH.Data.Validation.Enums;
using HH.Data.Validation.Interfaces;

namespace HH.Data.Validation.Implementations
{
    public sealed class ValidationMessageFactory : IValidationMessageFactory
    {
        public IValidationMessage CreateValidationMessage(string displayMessage, MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Error:
                    return CreateErrorValidationMessage(displayMessage);
                case MessageType.Warning:
                    return new WarningValidationMessage(displayMessage);
                case MessageType.Information:
                    return new InformationValidationMessage(displayMessage);
            }
            throw new NotImplementedException();
        }

        public IValidationMessage CreateErrorValidationMessage(string displayMessage)
        {
            return new ErrorValidationMessage(displayMessage);
        }

        public IReadOnlyCollection<IValidationMessage> CreateErrorValidationMessages(IEnumerable<string> errorDisplayMessages)
        {
            return errorDisplayMessages.Select(CreateErrorValidationMessage).ToArray();
        }
    }
}
