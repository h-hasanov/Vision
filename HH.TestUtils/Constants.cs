using System;

namespace HH.TestUtils
{
    public static class Constants
    {
        public static double LowToleranceDelta = Math.Exp(-14 * Math.Log(10));
        public static double MediumToleranceDelta = Math.Exp(-10*Math.Log(10));
        public static double HighToleranceDelta = Math.Exp(-6 * Math.Log(10));
    }
}
