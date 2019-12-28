using System;
using HH.Finance.Enums;
using HH.Finance.Interfaces;
using HH.Finance.Model;

namespace HH.Finance.Instruments
{
    public class OptionBase : InstrumentBase, IOption
    {
        #region Fields

        private IPricingEngine _pricingEngine;

        #endregion

        #region Constructors

        public OptionBase(double price, double strike, double volatility, double rate, double dividend, double time, ExerciseType exerciseType, OptionType optionType,
    IISIN isin, string description, IPricingEngine pricingEngine)
            : this(new OptionInput(price, strike, volatility, rate, dividend, time, exerciseType, optionType, isin, description), pricingEngine)
        {
        }


        public OptionBase(IOptionInput optionInput, IPricingEngine pricingEngine)
            : base(optionInput.ISIN, optionInput.Description)
        {
            _pricingEngine = pricingEngine;
            OptionInput = optionInput;
        }

        #endregion

        #region Properties

        public IPricingEngine PricingEngine { get { return _pricingEngine; } }

        public IOptionInput OptionInput { get; private set; }

        #endregion

        #region Methods

        public IOptionValue GetOptionValue()
        {
            switch (OptionInput.OptionType)
            {
                case OptionType.Call:
                    return CalculateCallPrice();
                case OptionType.Put:
                    return CalculatePutPrice();
            }
            throw new NotImplementedException();
        }

        protected virtual IOptionValue CalculateCallPrice()
        {
            throw new NotImplementedException();
        }

        protected virtual IOptionValue CalculatePutPrice()
        {
            throw new NotImplementedException();
        }

        protected double CalculateD1()
        {
            return OptionGreeksCalculator.CalculateD1(OptionInput);
        }

        protected double CalculateD2(double d1)
        {
            return OptionGreeksCalculator.CalculateD2(d1, OptionInput);
        }

        public IGreek GetGreek(GreekType greek)
        {
            switch (greek)
            {
                case GreekType.Delta:
                    return new Greek(greek, OptionGreeksCalculator.GetDelta(OptionInput));
                case GreekType.Vega:
                    return new Greek(greek, OptionGreeksCalculator.GetVega(OptionInput));
                case GreekType.Theta:
                    return new Greek(greek, OptionGreeksCalculator.GetTheta(OptionInput));
                case GreekType.Rho:
                    return new Greek(greek, OptionGreeksCalculator.GetRho(OptionInput));
                case GreekType.Gamma:
                    return new Greek(greek, OptionGreeksCalculator.GetGamma(OptionInput));
            }
            throw new NotImplementedException();
        }


        public override bool IsExpired()
        {
            return OptionInput.Time < 0;
        }

        public void SetPricingEngine(IPricingEngine pricingEngine)
        {
            if (pricingEngine == null) throw new ArgumentNullException("pricingEngine");

            _pricingEngine = pricingEngine;
        }

        #endregion

    }
}
