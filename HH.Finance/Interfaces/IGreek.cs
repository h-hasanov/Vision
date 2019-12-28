using HH.Finance.Enums;

namespace HH.Finance.Interfaces
{
    public interface IGreek
    {
        GreekType Type { get; }
        double Value { get; }
    }
}
