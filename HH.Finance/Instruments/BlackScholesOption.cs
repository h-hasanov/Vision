using HH.Finance.Enums;
using HH.Finance.Interfaces;
using HH.Finance.Model;
using HH.Finance.Result;
using HH.Statistics.Distributions.Univariate;
using static System.Math;

namespace HH.Finance.Instruments
{
    public class BlackScholesOption : VanillaOption, IBlackScholesOption
    {
        #region Fields

        private readonly IOptionInput _optionInput;

        #endregion

        #region Constructors

        public BlackScholesOption(double price, double strike, double volatility, double rate, double dividend, double time, ExerciseType exerciseType, OptionType optionType,
            IISIN isin, string description, IPricingEngine pricingEngine) :
            this(new OptionInput(price, strike, volatility, rate, dividend, time, exerciseType, optionType, isin, description), pricingEngine)
        {
        }

        public BlackScholesOption(IOptionInput optionInput, IPricingEngine pricingEngine) :
            base(optionInput, pricingEngine)
        {
            _optionInput = optionInput;
        }

        #endregion

        #region Methods

        protected override IOptionValue CalculateCallPrice()
        {
            var d1 = CalculateD1();
            var d2 = CalculateD2(d1);

            var callPrice = StandardDistributions.StandardNormalDistribution.CumulativeDistributionFunction(d1) * _optionInput.Price *
                            Exp(-_optionInput.Dividend * _optionInput.Time)
                            -
                            StandardDistributions.StandardNormalDistribution.CumulativeDistributionFunction(d2) * _optionInput.Strike *
                            Exp(-_optionInput.Rate * _optionInput.Time);
            return new OptionValue(callPrice);
        }

        protected override IOptionValue CalculatePutPrice()
        {
            var d1 = CalculateD1();
            var d2 = CalculateD2(d1);

            var putPrice = -StandardDistributions.StandardNormalDistribution.CumulativeDistributionFunction(-d1) * _optionInput.Price *
                           Exp(-_optionInput.Dividend * _optionInput.Time)
                           +
                          StandardDistributions.StandardNormalDistribution.CumulativeDistributionFunction(-d2) * _optionInput.Strike *
                          Exp(-_optionInput.Rate * _optionInput.Time);
            return new OptionValue(putPrice);
        }

        #endregion
    }
}
