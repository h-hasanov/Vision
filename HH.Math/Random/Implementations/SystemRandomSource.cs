using HH.EnvironmentServices.Utils;
using HH.Math.Random.Enums;
using HH.Math.Random.Interfaces;

namespace HH.Math.Random.Implementations
{
    internal sealed class SystemRandomSource : ISystemRandomSource
    {

        #region Fields

        private readonly System.Random _randomNumberGenerator;

        #endregion

        #region Constructors

        public SystemRandomSource(int seed) : this(new System.Random(seed))
        {
        }

        public SystemRandomSource() : this(new System.Random())
        {
        }

        internal SystemRandomSource(System.Random randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator.ArgumentNullCheck("randomNumberGenerator");
        }

        #endregion

        #region Properties

        public RandomSourceType RandomSourceType { get { return RandomSourceType.System; } }

        #endregion

        #region Methods

        public int Next()
        {
            return Next(0, int.MaxValue);
        }

        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            return _randomNumberGenerator.Next(minValue, maxValue);
        }

        public double NextDouble()
        {
            return _randomNumberGenerator.NextDouble();
        }

        #endregion
    }
}
