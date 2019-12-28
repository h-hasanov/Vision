using HH.Data.Validation.Enums;

namespace HH.Data.Validation.Interfaces
{
    public interface IMessage
    {
        string Text { get; }

        MessageType MessageType { get; }
    }
}
