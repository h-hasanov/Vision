using HH.Finance.Enums;

namespace HH.Finance.Interfaces
{
    public interface IOptionInput : IInput
    {
        double Price { get; }
        double Strike { get; }
        double Rate { get; }
        double Dividend { get; }
        double Volatility { get; }
        double Time { get; }
        ExerciseType ExerciseType { get; }
        OptionType OptionType { get; }
        IISIN ISIN { get; }
        string Description { get; }
    }
}
