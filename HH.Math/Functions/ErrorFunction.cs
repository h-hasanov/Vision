using static System.Math;

namespace HH.Math.Functions
{
    public static class ErrorFunction
    {
        private const int NumberOfErfIterations = 500;
        public const double LogMax = 7.09782712893383996732E2;
        #region Erf, Erfc

        private static readonly double[] ErfcT =
        {
            9.60497373987051638749E0,
            9.00260197203842689217E1,
            2.23200534594684319226E3,
            7.00332514112805075473E3,
            5.55923013010394962768E4
        };

        private static readonly double[] ErfcU =
        {
            3.35617141647503099647E1,
            5.21357949780152679795E2,
            4.59432382970980127987E3,
            2.26290000613890934246E4,
            4.92673942608635921086E4
        };


        private static readonly double[] ErfcP =
        {
            2.46196981473530512524E-10,
            5.64189564831068821977E-1,
            7.46321056442269912687E0,
            4.86371970985681366614E1,
            1.96520832956077098242E2,
            5.26445194995477358631E2,
            9.34528527171957607540E2,
            1.02755188689515710272E3,
            5.57535335369399327526E2
        };

        private static readonly double[] ErfcQ =
        {
            1.32281951154744992508E1,
            8.67072140885989742329E1,
            3.54937778887819891062E2,
            9.75708501743205489753E2,
            1.82390916687909736289E3,
            2.24633760818710981792E3,
            1.65666309194161350182E3,
            5.57535340817727675546E2
        };

        private static readonly double[] ErfcR =
        {
            5.64189583547755073984E-1,
            1.27536670759978104416E0,
            5.01905042251180477414E0,
            6.16021097993053585195E0,
            7.40974269950448939160E0,
            2.97886665372100240670E0
        };

        private static readonly double[] ErfcS =
        {
            2.26052863220117276590E0,
            9.39603524938001434673E0,
            1.20489539808096656605E1,
            1.70814450747565897222E1,
            9.60896809063285878198E0,
            3.36907645100081516050E0
        };

        private static readonly double[] ErvInvImpAn =
        {
            -0.000508781949658280665617,
            -0.00836874819741736770379,
            0.0334806625409744615033,
            -0.0126926147662974029034,
            -0.0365637971411762664006,
            0.0219878681111168899165,
            0.00822687874676915743155,
            -0.00538772965071242932965
        };

        private static readonly double[] ErvInvImpAd =
        {
            1, -0.970005043303290640362,
            -1.56574558234175846809,
            1.56221558398423026363,
            0.662328840472002992063,
            -0.71228902341542847553,
            -0.0527396382340099713954,
            0.0795283687341571680018,
            -0.00233393759374190016776,
            0.000886216390456424707504
        };

        private static readonly double[] ErvInvImpBn =
        {
            -0.202433508355938759655,
            0.105264680699391713268,
            8.37050328343119927838,
            17.6447298408374015486,
            -18.8510648058714251895,
            -44.6382324441786960818,
            17.445385985570866523,
            21.1294655448340526258,
            -3.67192254707729348546
        };

        private static readonly double[] ErvInvImpBd =
        {
            1, 6.24264124854247537712,
            3.9713437953343869095,
            -28.6608180499800029974,
            -20.1432634680485188801,
            48.5609213108739935468,
            10.8268667355460159008,
            -22.6436933413139721736,
            1.72114765761200282724
        };

        private static readonly double[] ErvInvImpCn =
        {
            -0.131102781679951906451,
            -0.163794047193317060787,
            0.117030156341995252019,
            0.387079738972604337464,
            0.337785538912035898924,
            0.142869534408157156766,
            0.0290157910005329060432,
            0.00214558995388805277169,
            -0.679465575181126350155e-6,
            0.285225331782217055858e-7,
            -0.681149956853776992068e-9
        };


        private static readonly double[] ErvInvImpCd =
        {
            1, 3.46625407242567245975,
            5.38168345707006855425,
            4.77846592945843778382,
            2.59301921623620271374,
            0.848854343457902036425,
            0.152264338295331783612,
            0.01105924229346489121
        };

        private static readonly double[] ErvInvImpDn =
        {
            -0.0350353787183177984712,
            -0.00222426529213447927281,
            0.0185573306514231072324,
            0.00950804701325919603619,
            0.00187123492819559223345,
            0.000157544617424960554631,
            0.460469890584317994083e-5,
            -0.230404776911882601748e-9,
            0.266339227425782031962e-11
        };

        /// <summary> Polynomial coefficients for a denominator of ErfInvImp
        /// calculation for Erf^-1(z) in the interval [0.75, 1] with x between 3 and 6.
        /// </summary>
        private static readonly double[] ErvInvImpDd =
        {
            1, 1.3653349817554063097,
            0.762059164553623404043,
            0.220091105764131249824,
            0.0341589143670947727934,
            0.00263861676657015992959,
            0.764675292302794483503e-4
        };

        /// <summary> Polynomial coefficients for a numerator of ErfInvImp
        /// calculation for Erf^-1(z) in the interval [0.75, 1] with x between 6 and 18.
        /// </summary>
        private static readonly double[] ErvInvImpEn =
        {
            -0.0167431005076633737133,
            -0.00112951438745580278863,
            0.00105628862152492910091,
            0.000209386317487588078668,
            0.149624783758342370182e-4,
            0.449696789927706453732e-6,
            0.462596163522878599135e-8,
            -0.281128735628831791805e-13,
            0.99055709973310326855e-16
        };

        /// <summary> Polynomial coefficients for a denominator of ErfInvImp
        /// calculation for Erf^-1(z) in the interval [0.75, 1] with x between 6 and 18.
        /// </summary>
        private static readonly double[] ErvInvImpEd =
        {
            1, 0.591429344886417493481,
            0.138151865749083321638,
            0.0160746087093676504695,
            0.000964011807005165528527,
            0.275335474764726041141e-4,
            0.282243172016108031869e-6
        };

        /// <summary> Polynomial coefficients for a numerator of ErfInvImp
        /// calculation for Erf^-1(z) in the interval [0.75, 1] with x between 18 and 44.
        /// </summary>
        private static readonly double[] ErvInvImpFn =
        {
            -0.0024978212791898131227,
            -0.779190719229053954292e-5,
            0.254723037413027451751e-4,
            0.162397777342510920873e-5,
            0.396341011304801168516e-7,
            0.411632831190944208473e-9,
            0.145596286718675035587e-11,
            -0.116765012397184275695e-17
        };

        /// <summary> Polynomial coefficients for a denominator of ErfInvImp
        /// calculation for Erf^-1(z) in the interval [0.75, 1] with x between 18 and 44.
        /// </summary>
        private static readonly double[] ErvInvImpFd =
        {
            1, 0.207123112214422517181,
            0.0169410838120975906478,
            0.000690538265622684595676,
            0.145007359818232637924e-4,
            0.144437756628144157666e-6,
            0.509761276599778486139e-9
        };

        private static readonly double[] ErvInvImpGn =
        {
            -0.000539042911019078575891,
            -0.28398759004727721098e-6,
            0.899465114892291446442e-6,
            0.229345859265920864296e-7,
            0.225561444863500149219e-9,
            0.947846627503022684216e-12,
            0.135880130108924861008e-14,
            -0.348890393399948882918e-21
        };

        private static readonly double[] ErvInvImpGd =
        {
            1, 0.0845746234001899436914,
            0.00282092984726264681981,
            0.468292921940894236786e-4,
            0.399968812193862100054e-6,
            0.161809290887904476097e-8,
            0.231558608310259605225e-11
        };

        /// <summary>
        /// Calculate the Complementary Error Function given x.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Erfc(this double value)
        {
            double x, y, z, p, q;

            if (value < 0.0)
                x = -value;
            else
                x = value;

            if (x < 1.0)
                return 1.0 - Erf(value);

            z = -value * value;

            if (z < - LogMax)
            {
                if (value < 0)
                    return (2.0);
                else
                    return (0.0);
            }

            z = Exp(z);

            if (x < 8.0)
            {
                p = Polevl(x, ErfcP, 8);
                q = P1Evl(x, ErfcQ, 8);
            }
            else
            {
                p = Polevl(x, ErfcR, 5);
                q = P1Evl(x, ErfcS, 6);
            }

            y = z * p / q;

            if (value < 0)
                y = 2.0 - y;

            if (y == 0.0)
            {
                if (value < 0)
                    return 2.0;
                else
                    return (0.0);
            }

            return y;
        }

        /// <summary>
        /// Calculate the Error Function given x.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Erf(this double x)
        {
            return Erf(x, NumberOfErfIterations);
        }

        /// <summary>
        /// Calculate the Error Function given x and number of iterations.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="iterations"></param>
        /// <returns></returns>
        private static double Erf(this double x, int iterations)
        {
            double y, z;

            if (Abs(x) > 1.0)
                return (1.0 - Erfc(x));

            z = x * x;
            y = x * Polevl(z, ErfcT, 4) / P1Evl(z, ErfcU, 5);

            return y;
        }

        #endregion

        /// <summary>
        ///   Evaluates polynomial of degree N
        /// </summary>
        /// 
        public static double Polevl(double x, double[] coef, int n)
        {
            double ans;

            ans = coef[0];

            for (int i = 1; i <= n; i++)
                ans = ans * x + coef[i];

            return ans;
        }

        /// <summary>
        ///   Evaluates polynomial of degree N with assumption that coef[N] = 1.0
        /// </summary>
        /// 
        public static double P1Evl(double x, double[] coef, int n)
        {
            double ans;

            ans = x + coef[0];

            for (int i = 1; i < n; i++)
                ans = ans * x + coef[i];

            return ans;
        }

        public static double ErfcInv(this double z)
        {
            if (z <= 0.0)
            {
                return double.PositiveInfinity;
            }

            if (z >= 2.0)
            {
                return double.NegativeInfinity;
            }

            double p, q, s;
            if (z > 1)
            {
                q = 2 - z;
                p = 1 - q;
                s = -1;
            }
            else
            {
                p = 1 - z;
                q = z;
                s = 1;
            }

            return ErfInvImpl(p, q, s);
        }

        internal static double ErfInvImpl(double p, double q, double s)
        {
            double result;

            if (p <= 0.5)
            {
                // Evaluate inverse erf using the rational approximation:
                //
                // x = p(p+10)(Y+R(p))
                //
                // Where Y is a constant, and R(p) is optimized for a low
                // absolute error compared to |Y|.
                //
                // double: Max error found: 2.001849e-18
                // long double: Max error found: 1.017064e-20
                // Maximum Deviation Found (actual error term at infinite precision) 8.030e-21
                const float y = 0.0891314744949340820313f;
                double g = p * (p + 10);
                double r = EvaluatePolynomial(p, ErvInvImpAn) / EvaluatePolynomial(p, ErvInvImpAd);
                result = (g * y) + (g * r);
            }
            else if (q >= 0.25)
            {
                // Rational approximation for 0.5 > q >= 0.25
                //
                // x = sqrt(-2*log(q)) / (Y + R(q))
                //
                // Where Y is a constant, and R(q) is optimized for a low
                // absolute error compared to Y.
                //
                // double : Max error found: 7.403372e-17
                // long double : Max error found: 6.084616e-20
                // Maximum Deviation Found (error term) 4.811e-20
                const float y = 2.249481201171875f;
                double g = Sqrt(-2 * Log(q));
                double xs = q - 0.25;
                double r = EvaluatePolynomial(xs, ErvInvImpBn) / EvaluatePolynomial(xs, ErvInvImpBd);
                result = g / (y + r);
            }
            else
            {
                // For q < 0.25 we have a series of rational approximations all
                // of the general form:
                //
                // let: x = sqrt(-log(q))
                //
                // Then the result is given by:
                //
                // x(Y+R(x-B))
                //
                // where Y is a constant, B is the lowest value of x for which
                // the approximation is valid, and R(x-B) is optimized for a low
                // absolute error compared to Y.
                //
                // Note that almost all code will really go through the first
                // or maybe second approximation.  After than we're dealing with very
                // small input values indeed: 80 and 128 bit long double's go all the
                // way down to ~ 1e-5000 so the "tail" is rather long...
                double x = Sqrt(-Log(q));
                if (x < 3)
                {
                    // Max error found: 1.089051e-20
                    const float y = 0.807220458984375f;
                    double xs = x - 1.125;
                    double r = EvaluatePolynomial(xs, ErvInvImpCn) / EvaluatePolynomial(xs, ErvInvImpCd);
                    result = (y * x) + (r * x);
                }
                else if (x < 6)
                {
                    // Max error found: 8.389174e-21
                    const float y = 0.93995571136474609375f;
                    double xs = x - 3;
                    double r = EvaluatePolynomial(xs, ErvInvImpDn) / EvaluatePolynomial(xs, ErvInvImpDd);
                    result = (y * x) + (r * x);
                }
                else if (x < 18)
                {
                    // Max error found: 1.481312e-19
                    const float y = 0.98362827301025390625f;
                    double xs = x - 6;
                    double r = EvaluatePolynomial(xs, ErvInvImpEn) / EvaluatePolynomial(xs, ErvInvImpEd);
                    result = (y * x) + (r * x);
                }
                else if (x < 44)
                {
                    // Max error found: 5.697761e-20
                    const float y = 0.99714565277099609375f;
                    double xs = x - 18;
                    double r = EvaluatePolynomial(xs, ErvInvImpFn) / EvaluatePolynomial(xs, ErvInvImpFd);
                    result = (y * x) + (r * x);
                }
                else
                {
                    // Max error found: 1.279746e-20
                    const float y = 0.99941349029541015625f;
                    double xs = x - 44;
                    double r = EvaluatePolynomial(xs, ErvInvImpGn) / EvaluatePolynomial(xs, ErvInvImpGd);
                    result = (y * x) + (r * x);
                }
            }

            return s * result;
        }

        internal static double EvaluatePolynomial(double z, params double[] coefficients)
        {
            double sum = coefficients[coefficients.Length - 1];
            for (int i = coefficients.Length - 2; i >= 0; --i)
            {
                sum *= z;
                sum += coefficients[i];
            }

            return sum;
        }
    }
}
