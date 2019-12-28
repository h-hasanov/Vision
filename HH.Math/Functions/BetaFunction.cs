using System;
using static System.Math;

namespace HH.Math.Functions
{
    public static class BetaFunction
    {
        /// <summary>
        /// Calculates the Beta function for given alpha and beta.
        /// Beta function as Γ(a) * Γ(b) / Γ(a+b).
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="beta"></param>
        /// <returns></returns>
        public static double Beta(double alpha, double beta)
        {
            return alpha.Gamma() * beta.Gamma() / (alpha + beta).Gamma();
        }

        /// <summary>
        /// Incomplete (regularized) Beta function Ix(a, b) = (IncompleteBeta(a,b,x))/Beta(a,b)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double BetaIncompleteRegularized(double a, double b, double x)
        {
            if (a <= 0.0)
                throw new ArgumentOutOfRangeException(nameof(a));
            if (b <= 0.0)
                throw new ArgumentOutOfRangeException(nameof(b));
            if (x <= 0.0 || x >= 1.0)
            {
                if (x == 0.0)
                    return 0.0;
                if (x == 1.0)
                    return 1.0;
                throw new ArgumentOutOfRangeException(nameof(x), "Value must be between 0 and 1.");
            }
            bool flag = false;
            if (b * x <= 1.0 && x <= 0.95)
                return PowerSeries(a, b, x);
            double num1 = 1.0 - x;
            double num2;
            double num3;
            double num4;
            double num5;
            if (x > a / (a + b))
            {
                flag = true;
                num2 = b;
                num3 = a;
                num4 = x;
                num5 = num1;
            }
            else
            {
                num2 = a;
                num3 = b;
                num4 = num1;
                num5 = x;
            }
            if (flag && num3 * num5 <= 1.0 && num5 <= 0.95)
            {
                double num6 = PowerSeries(num2, num3, num5);
                return num6 > 1.11022302462516E-16 ? 1.0 - num6 : 1.0 / 1.0;
            }
            double num7 = num5 * (num2 + num3 - 2.0) - (num2 - 1.0) >= 0.0 ? Incbd(num2, num3, num5) / num4 : Incbcf(num2, num3, num5);
            double num8 = num2 * Log(num5);
            double num9 = num3 * Log(num4);
            if (num2 + num3 < 171.624376956303 && Abs(num8) < 709.782712893384 && Abs(num9) < 709.782712893384)
            {
                double num6 = Pow(num4, num3) * Pow(num5, num2) / num2 * num7 * ((num2 + num3).Gamma() / (num2.Gamma() * num3.Gamma()));
                if (flag)
                    num6 = num6 > 1.11022302462516E-16 ? 1.0 - num6 : 1.0 / 1.0;
                return num6;
            }
            double d = num8 + (num9 + (num2 + num3).LogGamma() - num2.LogGamma() - num3.LogGamma()) + Log(num7 / num2);
            double num10 = d >= -745.133219101941 ? Exp(d) : 0.0;
            if (flag)
                num10 = num10 > 1.11022302462516E-16 ? 1.0 - num10 : 1.0 / 1.0;
            return num10;
        }

        /// <summary>
        ///   Continued fraction expansion #1 for incomplete beta integral.
        /// </summary>
        /// 
        /// <example>
        ///   Please see <see cref="Beta"/>
        /// </example>
        private static double Incbd(double a, double b, double x)
        {
            const double num1 = 4.5035996273705E+15;
            const double num2 = 2.22044604925031E-16;
            var num3 = a;
            var num4 = b - 1.0;
            var num5 = a;
            var num6 = a + 1.0;
            var num7 = 1.0;
            var num8 = a + b;
            var num9 = a + 1.0;
            var num10 = a + 2.0;
            var num11 = 0.0;
            var num12 = 1.0;
            var num13 = 1.0;
            var num14 = 1.0;
            var num15 = x / (1.0 - x);
            var num16 = 1.0;
            var num17 = 1.0;
            var num18 = 0;
            const double num19 = 3.33066907387547E-16;
            do
            {
                double num20 = -(num15 * num3 * num4) / (num5 * num6);
                double num21 = num13 + num11 * num20;
                double num22 = num14 + num12 * num20;
                double num23 = num13;
                double num24 = num21;
                double num25 = num14;
                double num26 = num22;
                double num27 = num15 * num7 * num8 / (num9 * num10);
                double num28 = num24 + num23 * num27;
                double num29 = num26 + num25 * num27;
                num11 = num24;
                num13 = num28;
                num12 = num26;
                num14 = num29;
                if (num29 != 0.0)
                    num17 = num28 / num29;
                double num30;
                if (num17 != 0.0)
                {
                    num30 = Abs((num16 - num17) / num17);
                    num16 = num17;
                }
                else
                    num30 = 1.0;
                if (num30 < num19)
                    return num16;
                ++num3;
                --num4;
                num5 += 2.0;
                num6 += 2.0;
                ++num7;
                ++num8;
                num9 += 2.0;
                num10 += 2.0;
                if (Abs(num29) + Abs(num28) > num1)
                {
                    num11 *= num2;
                    num13 *= num2;
                    num12 *= num2;
                    num14 *= num2;
                }
                if (Abs(num29) < num2 || Abs(num28) < num2)
                {
                    num11 *= num1;
                    num13 *= num1;
                    num12 *= num1;
                    num14 *= num1;
                }
            }
            while (++num18 < 300);
            return num16;
        }

        /// <summary>
        ///   Continued fraction expansion #2 for incomplete beta integral.
        /// </summary>
        /// 
        /// <example>
        ///   Please see <see cref="Beta"/>
        /// </example>
        private static double Incbcf(double a, double b, double x)
        {
            const double num1 = 4.5035996273705E+15;
            const double num2 = 2.22044604925031E-16;
            var num3 = a;
            var num4 = a + b;
            var num5 = a;
            var num6 = a + 1.0;
            var num7 = 1.0;
            var num8 = b - 1.0;
            var num9 = num6;
            var num10 = a + 2.0;
            var num11 = 0.0;
            var num12 = 1.0;
            var num13 = 1.0;
            var num14 = 1.0;
            var num15 = 1.0;
            var num16 = 1.0;
            var num17 = 0;
            const double num18 = 3.33066907387547E-16;
            do
            {
                double num19 = -(x * num3 * num4) / (num5 * num6);
                double num20 = num13 + num11 * num19;
                double num21 = num14 + num12 * num19;
                double num22 = num13;
                double num23 = num20;
                double num24 = num14;
                double num25 = num21;
                double num26 = x * num7 * num8 / (num9 * num10);
                double num27 = num23 + num22 * num26;
                double num28 = num25 + num24 * num26;
                num11 = num23;
                num13 = num27;
                num12 = num25;
                num14 = num28;
                if (num28 != 0.0)
                    num16 = num27 / num28;
                double num29;
                if (num16 != 0.0)
                {
                    num29 = Abs((num15 - num16) / num16);
                    num15 = num16;
                }
                else
                    num29 = 1.0;
                if (num29 < num18)
                    return num15;
                ++num3;
                ++num4;
                num5 += 2.0;
                num6 += 2.0;
                ++num7;
                --num8;
                num9 += 2.0;
                num10 += 2.0;
                if (Abs(num28) + Abs(num27) > num1)
                {
                    num11 *= num2;
                    num13 *= num2;
                    num12 *= num2;
                    num14 *= num2;
                }
                if (Abs(num28) < num2 || Abs(num27) < num2)
                {
                    num11 *= num1;
                    num13 *= num1;
                    num12 *= num1;
                    num14 *= num1;
                }
            }
            while (++num17 < 300);
            return num15;
        }

        /// <summary>
        ///   Power series for incomplete beta integral. Use when b*x
        ///   is small and x not too close to 1.
        /// </summary>
        /// 
        /// <example>
        ///   Please see <see cref="Beta"/>
        /// </example>
        private static double PowerSeries(double a, double b, double x)
        {
            double num1 = 1.0 / a;
            double num2 = (1.0 - b) * x;
            double num3 = num2 / (a + 1.0);
            double num4 = num3;
            double num5 = num2;
            double num6 = 2.0;
            double num7 = 0.0;
            double num8 = 1.11022302462516E-16 * num1;
            while (Abs(num3) > num8)
            {
                double num9 = (num6 - b) * x / num6;
                num5 *= num9;
                num3 = num5 / (a + num6);
                num7 += num3;
                ++num6;
            }
            double d1 = num7 + num4 + num1;
            double num10 = a * Log(x);
            double num11;
            if (a + b < 171.624376956303 && Abs(num10) < 709.782712893384)
            {
                double num9 = (a + b).Gamma() / (a.Gamma() * b.Gamma());
                num11 = d1 * num9 * Pow(x, a);
            }
            else
            {
                double d2 = (a + b).LogGamma() - a.LogGamma() - b.LogGamma() + num10 + Log(d1);
                num11 = d2 >= -745.133219101941 ? Exp(d2) : 0.0;
            }
            return num11;
        }
    }
}
