using HH.Data.Validation.Enums;
using HH.Data.Validation.Interfaces;

namespace HH.Data.Validation.Implementations
{
    internal abstract class ValidationMessageBase : IValidationMessage
    {
        protected ValidationMessageBase(string text)
        {
            Text = text;
        }

        public string Text { get; private set; }
        public abstract MessageType MessageType { get; }
    }
}
