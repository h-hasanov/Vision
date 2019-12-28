using HH.Finance.Enums;
using HH.Finance.Interfaces;

namespace HH.Finance.Model
{
    public class OptionInput : IOptionInput
    {
        public OptionInput(double price, double strike, double volatility, double rate, double dividend, double time, ExerciseType exerciseType, OptionType optionType,
            IISIN isin, string description)
        {
            Price = price;
            Strike = strike;
            Volatility = volatility;
            Rate = rate;
            Dividend = dividend;
            Time = time;
            ExerciseType = exerciseType;
            OptionType = optionType;
            ISIN = isin;
            Description = description;
        }

        public double Price { get; private set; }
        public double Strike { get; private set; }
        public double Rate { get; private set; }
        public double Dividend { get; private set; }
        public double Volatility { get; private set; }
        public double Time { get; private set; }
        public ExerciseType ExerciseType { get; private set; }
        public OptionType OptionType { get; private set; }
        public IISIN ISIN { get; private set; }
        public string Description { get; private set; }
    }
}
