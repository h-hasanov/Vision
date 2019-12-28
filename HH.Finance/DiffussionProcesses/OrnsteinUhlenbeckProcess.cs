using HH.Extensions.Numeric;
using HH.Finance.Interfaces;
using static System.Math;

namespace HH.Finance.DiffussionProcesses
{
    /// <summary>
    /// Ornstein-Uhlenbeck process class 
    /// This class describes the Ornstein-Uhlenbeck process governed by dx = -a x(t) dt + sigma dz(t).
    /// </summary>
    public class OrnsteinUhlenbeckProcess : DifusionProcessBase, IOrnsteinUhlenbeckProcess
    {
        #region Fields

        private readonly double _speed;
        private readonly double _volatility;

        #endregion

        #region Constructors

        public OrnsteinUhlenbeckProcess(double speed, double volatility)
        {
            _speed = speed;
            _volatility = volatility;
        }

        #endregion

        #region Properties

        public double Speed { get { return _speed; } }

        public double Volatility { get { return _volatility; } }

        #endregion

        #region Methods

        public override double Drift(double time, double x)
        {
            return -_speed * x;
        }

        public override double Diffusion(double t, double x)
        {
            return _volatility;
        }

        public override double Expectation(double t0, double x0, double dt)
        {
            return x0 * Exp(-_speed * dt);
        }

        public override double Variance(double t0, double x0, double dt)
        {
            return 0.5 * _volatility.Square() / _speed * (1.0 - Exp(-2.0 * _speed * dt));
        }

        #endregion
    }
}
