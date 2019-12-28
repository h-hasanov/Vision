using HH.Extensions.Numeric;
using HH.Finance.Interfaces;

namespace HH.Finance.DiffussionProcesses
{
    /// <summary>
    /// This class describes the stochastic process governed by dS = (r – 0.5{sigma^2}) dt + sigmadz(t)
    /// </summary>
    public class BlackScholesProcess : DifusionProcessBase, IBlackScholesProcess
    {
        #region Fields

        private readonly double _rate;
        private readonly double _volatility;

        #endregion

        #region Constructors

        public BlackScholesProcess(double rate, double volatility)
        {
            _rate = rate;
            _volatility = volatility;
        }

        #endregion

        #region Properties

        public double Rate { get { return _rate; } }

        public double Volatility { get { return _volatility; } }

        #endregion

        #region Methods

        public override double Drift(double time, double x)
        {
            return _rate - 0.5 * _volatility.Square();
        }

        public override double Diffusion(double t, double x)
        {
            return _volatility;
        }

        #endregion
    }
}
