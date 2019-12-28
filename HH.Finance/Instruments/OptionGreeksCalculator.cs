using HH.Extensions.Numeric;
using HH.Finance.Enums;
using HH.Finance.Interfaces;
using HH.Statistics.Distributions.Univariate;
using static System.Math;

namespace HH.Finance.Instruments
{
    public static class OptionGreeksCalculator
    {
        public static double GetVega(IOptionInput optionInput)
        {
            var d1 = CalculateD1(optionInput);
            var vega = (StandardDistributions.StandardNormalDistribution.ProbabilityDensityFunction(d1) * Exp(-optionInput.Dividend * optionInput.Time)) * 
                optionInput.Price * Sqrt(optionInput.Time);

            return vega;
        }

        public static double GetDelta(IOptionInput optionInput)
        {
            var d1 = CalculateD1(optionInput);
            var delta = 0.0;
            switch (optionInput.OptionType)
            {
                case OptionType.Call:
                    delta = Exp(-optionInput.Dividend * optionInput.Time) * StandardDistributions.StandardNormalDistribution.CumulativeDistributionFunction(d1);
                    break;
                case OptionType.Put:
                    delta = Exp(-optionInput.Dividend * optionInput.Time) * (StandardDistributions.StandardNormalDistribution.CumulativeDistributionFunction(d1) - 1.0);
                    break;
            }
            return delta;
        }

        public static double GetGamma(IOptionInput optionInput)
        {
            var d1 = CalculateD1(optionInput);
            var normalPrime = StandardDistributions.StandardNormalDistribution.ProbabilityDensityFunction(d1);
            var gamma = (normalPrime * Exp(-optionInput.Dividend * optionInput.Time)) / (optionInput.Price * optionInput.Volatility *
                       Sqrt(optionInput.Time));
            return gamma;
        }

        public static double GetRho(IOptionInput optionInput)
        {
            var d1 = CalculateD1(optionInput);
            var d2 = CalculateD2(d1, optionInput);

            var rho = 0.0;

            switch (optionInput.OptionType)
            {
                case OptionType.Call:
                    rho = optionInput.Strike * optionInput.Time * Exp(-optionInput.Rate * optionInput.Time) *
                          StandardDistributions.StandardNormalDistribution.CumulativeDistributionFunction(d2);
                    break;
                case OptionType.Put:
                    rho = -optionInput.Strike * optionInput.Time *
                        Exp(-optionInput.Rate * optionInput.Time) *
                          StandardDistributions.StandardNormalDistribution.CumulativeDistributionFunction(-d2);
                    break;
            }
            return rho;
        }

        public static double GetTheta(IOptionInput optionInput)
        {
            var d1 = CalculateD1(optionInput);
            var d2 = CalculateD2(d1, optionInput);
            var theta = 0.0;

            switch (optionInput.OptionType)
            {
                case OptionType.Call:
                    theta = (-optionInput.Price * StandardDistributions.StandardNormalDistribution.ProbabilityDensityFunction(d1) * optionInput.Volatility *
                             Exp(-optionInput.Dividend * optionInput.Time)) /
                            (2.0 * Sqrt(optionInput.Time)) +
                            optionInput.Dividend * optionInput.Price * StandardDistributions.StandardNormalDistribution.CumulativeDistributionFunction(d1) *
                            Exp(-optionInput.Dividend * optionInput.Time) -
                            optionInput.Rate * optionInput.Strike * Exp(-optionInput.Rate * optionInput.Time) *
                            StandardDistributions.StandardNormalDistribution.CumulativeDistributionFunction(d2);
                    break;
                case OptionType.Put:
                    theta = (-optionInput.Price * StandardDistributions.StandardNormalDistribution.ProbabilityDensityFunction(d1) * optionInput.Volatility *
                             Exp(-optionInput.Dividend * optionInput.Time)) /
                            (2.0 * Sqrt(optionInput.Time)) -
                            optionInput.Dividend * optionInput.Price * StandardDistributions.StandardNormalDistribution.CumulativeDistributionFunction(-d1) *
                            Exp(-optionInput.Dividend * optionInput.Time) +
                            optionInput.Rate * optionInput.Strike * Exp(-optionInput.Rate * optionInput.Time) *
                            StandardDistributions.StandardNormalDistribution.CumulativeDistributionFunction(-d2);
                    break;
            }
            return theta;
        }

        public static double CalculateD1(IOptionInput optionInput)
        {
            return (Log(optionInput.Price / optionInput.Strike) +
              (optionInput.Rate - optionInput.Dividend + optionInput.Volatility.Square() * 0.5) * optionInput.Time) /
                   (optionInput.Volatility * Sqrt(optionInput.Time));
        }

        public static double CalculateD2(double d1, IOptionInput optionInput)
        {
            return d1 - optionInput.Volatility * Sqrt(optionInput.Time);
        }
    }
}