using System.Diagnostics;

namespace Lokad.FastMath
{
    public partial class FastMath
    {
        // Source:
        // https://github.com/mathnet/mathnet-numerics/blob/master/src/Numerics/SpecialFunctions/Gamma.cs

        /// <summary>
        /// Computes the Digamma function which is mathematically defined as the derivative 
        /// of the logarithm of the gamma function.
        /// 
        /// This implementation is based on
        ///     Jose Bernardo
        ///     Algorithm AS 103:
        ///     Psi ( Digamma ) Function,
        ///     Applied Statistics,
        ///     Volume 25, Number 3, 1976, pages 315-317.
        /// Using the modifications as in Tom Minka's lightspeed toolbox.
        /// </summary>
        public static float Digamma(float x)
        {
            Debug.Assert(x > 0);

            const float c = 12.0f;
            const float d1 = -0.57721566490153286f;
            const float d2 = 1.6449340668482264365f;
            const float s = 1e-6f;
            const float s3 = 1.0f / 12.0f;
            const float s4 = 1.0f / 120.0f;
            const float s5 = 1.0f / 252.0f;
            const float s6 = 1.0f / 240.0f;
            const float s7 = 1.0f / 132.0f;

            if (x <= s)
            {
                return d1 - (1 / x) + (d2 * x);
            }

            float result = 0;
            while (x < c)
            {
                result -= 1 / x;
                x++;
            }

            if (x >= c)
            {
                var r = 1 / x;
                result += Log(x) - (0.5f * r);
                r *= r;

                result -= r * (s3 - (r * (s4 - (r * (s5 - (r * (s6 - (r * s7))))))));
            }

            return result;
        }

    }
}
