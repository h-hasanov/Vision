using HH.Finance.Enums;

namespace HH.Finance.Interfaces
{
    public interface IOption : IInstrument
    {
        IPricingEngine PricingEngine { get; }
        IOptionInput OptionInput { get; }
        IGreek GetGreek(GreekType greek);
        IOptionValue GetOptionValue();
        void SetPricingEngine(IPricingEngine pricingEngine);
    }
}
