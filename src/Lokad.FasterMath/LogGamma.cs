using System;
using System.Diagnostics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Lokad.FastMath
{
    public partial class FastMath
    {
        private static readonly double Ln2Pi_2 = Math.Log(2.0 * Math.PI) / 2.0;

        /// <summary>
        /// Log Gama Function continued fractions NIST Handbook of Mathematical Functions see:
        /// https://univ.jeanpaulcalvi.com/Posters/ConfAuchWeb/abramovitz2.pdf
        /// </summary>
        public static float LogGamma(float x)
        {
            Debug.Assert(x > 0);

            // Numeric shift to improve accuracy (cost +1 'Log').
            // logGamma(x) = logGamma(x + 1) - log(x)
            var result = -Log(x);
            x += 1f;

            const float Ln2Pi_2 = 0.91893853320467274178032f;

            // A & S eq. 6.1.48 (continuing fraction)
            const float a0 = (float)(1.0 / 12);
            const float a1 = (float)(1.0 / 30);
            const float a2 = (float)(53.0 / 210);
            const float a3 = (float)(195.0 / 371);
            const float a4 = (float)(22999.0 / 22737);
            const float a5 = (float)(29944523.0 / 19733142);
            const float a6 = (float)(109535241009.0 / 48264275462);

            var t6 = a6 / x;
            var t5 = a5 / (x + t6);
            var t4 = a4 / (x + t5);
            var t3 = a3 / (x + t4);
            var t2 = a2 / (x + t3);
            var t1 = a1 / (x + t2);
            var t0 = a0 / (x + t1);

            result += t0 - x + (x - 0.5f) * Log(x) + Ln2Pi_2;

            return result;
        }

        public static Vector256<float> LogGamma(Vector256<float> x)
        {
            const float one = 1f;
            const float half = 0.5f;

            // Numeric shift to improve accuracy (cost +1 'Log').
            // logGamma(x) = logGamma(x + 1) - log(x)
            var result = Log(x);
            x = Avx.Add(x, Vector256.Create(one));

            const float Ln2Pi_2 = 0.91893853320467274178032f;

            // A & S eq. 6.1.48 (continuing fraction)
            const float a0 = (float)(1.0 / 12);
            const float a1 = (float)(1.0 / 30);
            const float a2 = (float)(53.0 / 210);
            const float a3 = (float)(195.0 / 371);
            const float a4 = (float)(22999.0 / 22737);
            const float a5 = (float)(29944523.0 / 19733142);
            const float a6 = (float)(109535241009.0 / 48264275462);

            var t6 = Avx.Divide(Vector256.Create(a6), x);
            var t5 = Avx.Divide(Vector256.Create(a5), Avx.Add(x, t6));
            var t4 = Avx.Divide(Vector256.Create(a4), Avx.Add(x, t5));
            var t3 = Avx.Divide(Vector256.Create(a3), Avx.Add(x, t4));
            var t2 = Avx.Divide(Vector256.Create(a2), Avx.Add(x, t3));
            var t1 = Avx.Divide(Vector256.Create(a1), Avx.Add(x, t2));
            var t0 = Avx.Divide(Vector256.Create(a0), Avx.Add(x, t1));

            result = Avx.Subtract(
                Avx.Add(
                    Avx.Add(Avx.Subtract(t0, x), Avx.Multiply(Avx.Subtract(x, Vector256.Create(half)), Log(x))),
                    Vector256.Create(Ln2Pi_2)), 
                result);

            return result;
        }
    }
}
