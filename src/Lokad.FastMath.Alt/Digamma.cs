using System;

namespace Lokad.FastMath.Alt
{
    public partial class AltMath
    {
        // Source:
        // https://github.com/mathnet/mathnet-numerics/blob/master/src/Numerics/SpecialFunctions/Gamma.cs

        /// <summary>
        /// Computes the Digamma function which is mathematically defined as the derivative of the logarithm of the gamma function.
        /// This implementation is based on
        ///     Jose Bernardo
        ///     Algorithm AS 103:
        ///     Psi ( Digamma ) Function,
        ///     Applied Statistics,
        ///     Volume 25, Number 3, 1976, pages 315-317.
        /// Using the modifications as in Tom Minka's lightspeed toolbox.
        /// </summary>
        /// <remarks>
        /// Intended as a reference implementation, precision-wise.
        /// </remarks>
        public static double Digamma(double x)
        {
            const double c = 12.0;
            const double d1 = -0.57721566490153286;
            const double d2 = 1.6449340668482264365;
            const double s = 1e-6;
            const double s3 = 1.0 / 12.0;
            const double s4 = 1.0 / 120.0;
            const double s5 = 1.0 / 252.0;
            const double s6 = 1.0 / 240.0;
            const double s7 = 1.0 / 132.0;

            if (x <= 0)
                return double.NaN;

            if (x <= s)
            {
                return d1 - (1 / x) + (d2 * x);
            }

            double result = 0;
            while (x < c)
            {
                result -= 1 / x;
                x++;
            }

            if (x >= c)
            {
                var r = 1 / x;
                result += Math.Log(x) - (0.5 * r);
                r *= r;

                result -= r * (s3 - (r * (s4 - (r * (s5 - (r * (s6 - (r * s7))))))));
            }

            return result;
        }
    }
}
