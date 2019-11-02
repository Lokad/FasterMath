using System;
using System.Diagnostics;

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

            // Numeric shift to improe accuracy (cost +1 'Log').
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
    }
}
