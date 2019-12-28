using HH.Finance.Enums;
using HH.Finance.Interfaces;

namespace HH.Finance.Model
{
    public class Greek : IGreek
    {
        public Greek(GreekType greekType, double value)
        {
            Type = greekType;
            Value = value;
        }

        public GreekType Type { get; private set; }
        public double Value { get; private set; }
    }
}
