using HH.Data.Validation.Enums;
using HH.Data.Validation.Interfaces;

namespace HH.Data.Validation.Implementations
{
    internal sealed class InformationValidationMessage : ValidationMessageBase, IInformationValidationMessage
    {
        public InformationValidationMessage(string text)
            : base(text)
        {
        }

        public override MessageType MessageType
        {
            get { return MessageType.Information; }
        }
    }
}
