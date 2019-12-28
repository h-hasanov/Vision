using HH.Math.Random.Enums;
using HH.Math.Random.Interfaces;

namespace HH.Math.Random.Factories.Interfaces
{
    public interface IRandomSourceFactory
    {
        IRandomSource CreateRandomSource(RandomSourceType randomSourceType, int seed);
        IRandomSource CreateRandomSource(RandomSourceType randomSourceType);
    }
}
