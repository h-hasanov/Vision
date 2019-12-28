using System;
using HH.Math.Random.Enums;
using HH.Math.Random.Factories.Interfaces;
using HH.Math.Random.Implementations;
using HH.Math.Random.Interfaces;

namespace HH.Math.Random.Factories.Implementations
{
    public sealed class RandomSourceFactory : IRandomSourceFactory
    {
        public static IRandomSourceFactory DefaultRandomSourceFactory = new RandomSourceFactory();

        public IRandomSource CreateRandomSource(RandomSourceType randomSourceType, int seed)
        {
            switch (randomSourceType)
            {
                case RandomSourceType.System:
                    return CreateSystemRandomSource(seed);
                default:
                    throw new ArgumentOutOfRangeException("randomSourceType", randomSourceType, null);
            }
        }

        public IRandomSource CreateRandomSource(RandomSourceType randomSourceType)
        {
            switch (randomSourceType)
            {
                case RandomSourceType.System:
                    return CreateSystemRandomSource();
                default:
                    throw new ArgumentOutOfRangeException("randomSourceType", randomSourceType, null);
            }
        }

        private static IRandomSource CreateSystemRandomSource()
        {
            return new SystemRandomSource();
        }

        private static IRandomSource CreateSystemRandomSource(int seed)
        {
            return new SystemRandomSource(seed);
        }
    }
}
