using System;
using NUnit.Framework;

namespace HH.TestUtils
{
    public static class Utils
    {
        public static void AssertMemory(double memoryThresholdInMb)
        {
            var actualMemoryInMb = GC.GetTotalMemory(false) / Math.Pow(1024, 2);
            Assert.True(memoryThresholdInMb > actualMemoryInMb,
                $"Expected memory {memoryThresholdInMb} MB; Actual Memory {actualMemoryInMb} MB");
        }
    }
}
