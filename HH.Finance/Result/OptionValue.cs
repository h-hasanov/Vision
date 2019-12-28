using HH.Finance.Interfaces;

namespace HH.Finance.Result
{
    public class OptionValue : IOptionValue
    {
        public OptionValue(double result)
        {
            Value = result;
        }

        public double Value { get; private set; }
    }
}
