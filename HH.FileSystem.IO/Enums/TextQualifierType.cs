using HH.FileSystem.IO.Resources;
using HH.Presentation.Attributes;

namespace HH.FileSystem.IO.Enums
{
    public enum TextQualifierType : byte
    {
        [LocalizedDescription("TextQualifier_SingleQuote", typeof(EnumResources))]
        SingleQuote,

        [LocalizedDescription("TextQualifier_DoubleQuote", typeof(EnumResources))]
        DoubleQuote,

        [LocalizedDescription("TextQualifier_None", typeof(EnumResources))]
        None

    }
}
