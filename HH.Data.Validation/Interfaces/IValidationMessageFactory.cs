using System.Collections.Generic;
using HH.Data.Validation.Enums;

namespace HH.Data.Validation.Interfaces
{
    public interface IValidationMessageFactory
    {
        IValidationMessage CreateValidationMessage(string displayMessage, MessageType messageType);
        IValidationMessage CreateErrorValidationMessage(string displayMessage);
        IReadOnlyCollection<IValidationMessage> CreateErrorValidationMessages(IEnumerable<string> errorDisplayMessages);
    }
}
