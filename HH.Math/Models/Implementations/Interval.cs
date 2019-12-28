using System;
using HH.Math.Enums;
using HH.Math.Models.Interfaces;

namespace HH.Math.Models.Implementations
{
    public sealed class Interval<T> : IInterval<T> where T : IComparable<T>
    {
        public Interval(T lowerBound, T upperBound, ClosureType closureType)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
            ClosureType = closureType;
        }

        public T LowerBound { get; }
        public T UpperBound { get; }
        public ClosureType ClosureType { get; }

        public bool IsInside(T value)
        {
            switch (ClosureType)
            {
                case ClosureType.OpenOpen:
                    return value.CompareTo(LowerBound) > 0 && value.CompareTo(UpperBound) < 0;
                case ClosureType.OpenClosed:
                    return value.CompareTo(LowerBound) > 0 && value.CompareTo(UpperBound) <= 0;
                case ClosureType.ClosedOpen:
                    return value.CompareTo(LowerBound) >= 0 && value.CompareTo(UpperBound) < 0;
                case ClosureType.ClosedClosed:
                    return value.CompareTo(LowerBound) >= 0 && value.CompareTo(UpperBound) <= 0;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}