using HH.Finance.Enums;
using HH.Finance.Interfaces;
using HH.Finance.Model;

namespace HH.Finance.Instruments
{
    public class VanillaOption : OptionBase, IVanillaOption
    {
        public VanillaOption(double price, double strike, double volatility, double rate, double dividend, double time, ExerciseType exerciseType, OptionType optionType,
    IISIN isin, string description, IPricingEngine pricingEngine)
            : this(new OptionInput(price, strike, volatility, rate, dividend, time, exerciseType, optionType, isin, description), pricingEngine)
        {
        }

        public VanillaOption(IOptionInput optionInput, IPricingEngine pricingEngine)
            : base(optionInput, pricingEngine)
        {
        }

    }
}
