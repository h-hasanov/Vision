namespace HH.Extensions.Numeric
{
    public static class NumericExtensions
    {
        public static double Square(this double x)
        {
            return x*x;
        }

        public static double Square(this int x)
        {
            return x * x;
        }

        public static bool IsNaNOrInifinity(this double input)
        {
            return double.IsNaN(input) || double.IsInfinity(input);
        }
    }
}
