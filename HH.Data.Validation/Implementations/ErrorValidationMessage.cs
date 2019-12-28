using HH.Data.Validation.Enums;
using HH.Data.Validation.Interfaces;

namespace HH.Data.Validation.Implementations
{
    internal sealed class ErrorValidationMessage : ValidationMessageBase, IErrorValidationMessage
    {
        public ErrorValidationMessage(string text)
            : base(text)
        {
        }

        public override MessageType MessageType { get { return MessageType.Error; } }
    }
}
