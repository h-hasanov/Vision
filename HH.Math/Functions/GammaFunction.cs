using System;
using System.Globalization;
using static System.Math;

namespace HH.Math.Functions
{
    public static class GammaFunction
    {
        private static readonly double[] P =
        {
            -1.71618513886549492533811E+0,
            2.47656508055759199108314E+1,
            -3.79804256470945635097577E+2,
            6.29331155312818442661052E+2,
            8.66966202790413211295064E+2,
            -3.14512729688483675254357E+4,
            -3.61444134186911729807069E+4,
            6.64561438202405440627855E+4
        };

        private static readonly double[] Q =
        {
            -3.08402300119738975254353E+1,
            3.15350626979604161529144E+2,
            -1.01515636749021914166146E+3,
            -3.10777167157231109440444E+3,
            2.25381184209801510330112E+4,
            4.75584627752788110767815E+3,
            -1.34659959864969306392456E+5,
            -1.15132259675553483497211E+5
        };

        private static readonly double[] C =
        {
            1.0/12.0,
            -1.0/360.0,
            1.0/1260.0,
            -1.0/1680.0,
            1.0/1188.0,
            -691.0/360360.0,
            1.0/156.0,
            -3617.0/122400.0
        };

        /// <summary>
        /// Calculates gamma function for given x.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Gamma(this double x)
        {
            if (x <= 0.0)
            {
                throw new ArgumentOutOfRangeException();
            }

            const double gamma = 0.577215664901532860606512090; // Euler's gamma constant

            if (x < 0.001)
                return 1.0/(x*(1.0 + gamma*x));

            if (x < 12.0)
            {
                var y = x;
                var n = 0;
                var argWasLessThanOne = (y < 1.0);

                if (argWasLessThanOne)
                {
                    y += 1.0;
                }
                else
                {
                    n = int.Parse(Floor(y).ToString(CultureInfo.InvariantCulture)) - 1;
                    y -= n;
                }

                var num = 0.0;
                var den = 1.0;
                int i;

                var z = y - 1;
                for (i = 0; i < 8; i++)
                {
                    num = (num + P[i])*z;
                    den = den*z + Q[i];
                }
                var result = num/den + 1.0;

                if (argWasLessThanOne)
                {
                    result /= (y - 1.0);
                }
                else
                {
                    for (i = 0; i < n; i++)
                        result *= y++;
                }

                return result;
            }

            if (x > 171.624)
            {
                return double.MaxValue*2.0;
            }

            return Exp(LogGamma(x));
        }

        /// <summary>
        /// Returns the digamma function
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Digamma(this double x)
        {
            var num1 = 0.0;
            var flag = false;
            if (x <= 0.0)
            {
                flag = true;
                var d = x;
                var num2 = (double) (int) Floor(d);
                if (Abs(num2 - d) < double.Epsilon)
                    throw new OverflowException("Function computation resulted in arithmetic overflow.");
                var num3 = d - num2;
                if (Abs(num3 - 0.5) > double.Epsilon)
                {
                    if (num3 > 0.5)
                    {
                        var num4 = num2 + 1.0;
                        num3 = d - num4;
                    }
                    num1 = PI/Tan(PI*num3);
                }
                else
                    num1 = 0.0;
                x = 1.0 - x;
            }
            double num5;
            if (x <= 10.0 & Abs(x - Floor(x)) < double.Epsilon)
            {
                var num2 = 0.0;
                var num3 = (int) Floor(x);
                for (var index = 1; index <= num3 - 1; ++index)
                {
                    var num4 = (double) index;
                    num2 += 1.0/num4;
                }
                num5 = num2 - 0.577215664901533;
            }
            else
            {
                var d = x;
                var num2 = 0.0;
                for (; d < 10.0; ++d)
                    num2 += 1.0/d;
                double num3;
                if (d < 1E+17)
                {
                    var num4 = 1.0/(d*d);
                    var num6 = (((((1.0/12.0*num4 - 0.0210927960927961)*num4 + 1.0/132.0)*num4 - 1.0/240.0)*num4 +
                                 1.0/252.0)*num4 - 1.0/120.0)*num4 + 1.0/12.0;
                    num3 = num4*num6;
                }
                else
                    num3 = 0.0;
                num5 = Log(d) - 0.5/d - num3 - num2;
            }
            if (flag)
                num5 -= num1;
            return num5;
        }

        /// <summary>
        /// Returns the trigamma function.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Trigamma(this double x)
        {
            const double num1 = 0.0001;
            const double num2 = 5.0;
            const double num3 = 1.0/6.0;
            const double num4 = -1.0/30.0;
            const double num5 = 1.0/42.0;
            const double num6 = -1.0/30.0;
            if (x <= 0.0)
                throw new ArgumentException("The input parameter x must be positive.", nameof(x));
            var num7 = x;
            if (x <= num1)
                return 1.0/x/x;
            var num8 = 0.0;
            for (; num7 < num2; ++num7)
                num8 += 1.0/num7/num7;
            var num9 = 1.0/num7/num7;
            return num8 + 0.5*num9 + (1.0 + num9*(num3 + num9*(num4 + num9*(num5 + num9*num6))))/num7;
        }

        /// <summary>
        /// Calculate the LogGamma function for given x.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double LogGamma(this double x)
        {
            if (x <= 0.0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (x < 12.0)
            {
                return Log(Abs(Gamma(x)));
            }

            var z = 1.0/(x*x);
            var sum = C[7];
            for (var i = 6; i >= 0; i--)
            {
                sum *= z;
                sum += C[i];
            }
            var series = sum/x;

            const double halfLogTwoPi = 0.91893853320467274178032973640562;
            var logGamma = (x - 0.5)*Log(x) - x + halfLogTwoPi + series;
            return logGamma;
        }

        /// <summary>
        /// Upper incomplete regularized Gamma function Q
        /// (a.k.a the incomplete complemented Gamma function)
        /// This function is equivalent to Q(x) = Γ(s, x) / Γ(s)
        /// where Γ(s, x) is the upper gamma function and Γ(s) is the gamma function.
        /// </summary>
        public static double UpperIncompleteRegularized(double a, double x)
        {
            if (x <= 0.0 || a <= 0.0)
                return 1.0;
            if (x < 1.0 || x < a)
                return 1.0 - LowerIncompleteRegularized(a, x);
            var d = a*Log(x) - x - LogGamma(a);
            if (d < -709.782712893384)
                return 0.0;
            double num1 = Exp(d);
            double num2 = 1.0 - a;
            double num3 = x + num2 + 1.0;
            double num4 = 0.0;
            double num5 = 1.0;
            double num6 = x;
            double num7 = x + 1.0;
            double num8 = num3*x;
            double num9 = num7/num8;
            double num10;
            do
            {
                ++num4;
                ++num2;
                num3 += 2.0;
                double num11 = num2*num4;
                double num12 = num7*num3 - num5*num11;
                double num13 = num8*num3 - num6*num11;
                if (Abs(num13) > double.Epsilon)
                {
                    double num14 = num12/num13;
                    num10 = Abs((num9 - num14)/num14);
                    num9 = num14;
                }
                else
                    num10 = 1.0;
                num5 = num7;
                num7 = num12;
                num6 = num8;
                num8 = num13;
                if (Abs(num12) > 4.5035996273705E+15)
                {
                    num5 *= 2.22044604925031E-16;
                    num7 *= 2.22044604925031E-16;
                    num6 *= 2.22044604925031E-16;
                    num8 *= 2.22044604925031E-16;
                }
            } while (num10 > 1.11022302462516E-16);
            return num9*num1;
        }

        /// <summary>
        /// Lower incomplete regularized gamma function P
        /// (a.k.a. the incomplete Gamma function).
        /// This function is equivalent to P(x) = γ(s, x) / Γ(s)
        /// where γ(s, x) is the lower gamma function and Γ(s) is the gamma function.
        /// </summary>
        public static double LowerIncompleteRegularized(double a, double x)
        {
            if (x <= 0.0 || a <= 0.0)
                return 0.0;
            if (x > 1.0 && x > a)
                return 1.0 - UpperIncompleteRegularized(a, x);
            double d = a*Log(x) - x - LogGamma(a);
            if (d < -709.782712893384)
                return 0.0;
            double num1 = Exp(d);
            double num2 = a;
            double num3 = 1.0;
            double num4 = 1.0;
            do
            {
                ++num2;
                num3 *= x/num2;
                num4 += num3;
            } while (num3/num4 > 1.11022302462516E-16);
            return num4*num1/a;
        }
    }
}
