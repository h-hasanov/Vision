using HH.Data.Validation.Enums;
using HH.Data.Validation.Interfaces;

namespace HH.Data.Validation.Implementations
{
    internal sealed class WarningValidationMessage : ValidationMessageBase, IWarningValidationMessage
    {
        public WarningValidationMessage(string text)
            : base(text)
        {
        }

        public override MessageType MessageType
        {
            get { return MessageType.Warning; }
        }
    }
}
