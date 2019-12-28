using System;
using HH.Math.Enums;

namespace HH.Math.Models.Interfaces
{
    public interface IInterval<T> where T : IComparable<T>
    {
        T LowerBound { get; }
        T UpperBound { get; }
        ClosureType ClosureType { get; }

        bool IsInside(T value);
    }
}