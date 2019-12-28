using HH.Data.Validation.Resources;
using HH.Presentation.Attributes;

namespace HH.Data.Validation.Enums
{
    public enum MessageType : byte
    {
        [LocalizedDescription("Information", typeof(EnumResources))]
        Information,

        [LocalizedDescription("Warning", typeof(EnumResources))]
        Warning,

        [LocalizedDescription("Error", typeof(EnumResources))]
        Error
    }
}