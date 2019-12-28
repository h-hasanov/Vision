using HH.Math.Random.Enums;

namespace HH.Math.Random.Interfaces
{
    public interface IRandomSource
    {
        //
        // Summary:
        //     Returns a nonnegative random number.
        //
        // Returns:
        //     A 32-bit signed integer greater than or equal to zero and less than System.Int32.MaxValue.
        int Next();

        //
        // Summary:
        //     Returns a nonnegative random number less than the specified maximum.
        //
        // Parameters:
        //   maxValue:
        //     The exclusive upper bound of the random number to be generated. maxValue must
        //     be greater than or equal to zero.
        //
        // Returns:
        //     A 32-bit signed integer greater than or equal to zero, and less than maxValue;
        //     that is, the range of return values ordinarily includes zero but not maxValue.
        //     However, if maxValue equals zero, maxValue is returned.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     maxValue is less than zero.
        int Next(int maxValue);

        //
        // Summary:
        //     Returns a random number within a specified range.
        //
        // Parameters:
        //   minValue:
        //     The inclusive lower bound of the random number returned.
        //
        //   maxValue:
        //     The exclusive upper bound of the random number returned. maxValue must be greater
        //     than or equal to minValue.
        //
        // Returns:
        //     A 32-bit signed integer greater than or equal to minValue and less than maxValue;
        //     that is, the range of return values includes minValue but not maxValue. If minValue
        //     equals maxValue, minValue is returned.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     minValue is greater than maxValue.
        int Next(int minValue, int maxValue);

        //
        // Summary:
        //     Returns a random number between 0.0 and 1.0.
        //
        // Returns:
        //     A double-precision floating point number greater than or equal to 0.0, and less
        //     than 1.0.
        double NextDouble();

        RandomSourceType RandomSourceType { get; }
    }
}
