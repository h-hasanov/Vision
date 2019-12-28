using System;
using HH.Finance.Interfaces;
using static System.Math;

namespace HH.Finance.DiffussionProcesses
{

    /// <summary>
    /// Square-root process class
    /// This class describes a square-root process governed by dx = a (b – x_t) dt + \sigma sqrt{x_t} dW_t.
    /// </summary>
    public class SquareRootProcess : DifusionProcessBase, ISquareRootProcess
    {
        #region Fields

        private readonly double _mean;
        private readonly double _speed;
        private readonly double _volatility;

        #endregion

        #region Constructors

        public SquareRootProcess(double mean, double speed, double volatility)
        {
            _mean = mean;
            _speed = speed;
            _volatility = volatility;
        }

        #endregion

        #region Methods

        public double Mean { get { return _mean; } }

        public double Speed { get { return _speed; } }

        public double Volatility { get { return _volatility; } }

        #endregion

        #region Method

        public override double Drift(double time, double x)
        {
            return _speed * (_mean - x);
        }

        public override double Diffusion(double t, double x)
        {
            return _volatility * Sqrt(x);
        }

        #endregion
    }
}