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
        /// </summary>
        public static float Digamma(float x)
        {
            Debug.Assert(x > 0);

            const float s3 = 1.0f / 12.0f;
            const float s4 = 1.0f / 120.0f;
            const float s5 = 1.0f / 252.0f;
            const float s6 = 1.0f / 240.0f;
            const float s7 = 1.0f / 132.0f;
            
            // branch-free, to allow SIMD-variants

            float result = 0;
            for(var i = 0; i < 3; i++) // 3 tuned as a tradeoff performance-vs-precision
            {
                result -= 1 / x;
                x++;
            }

            var r = 1 / x;
            result += Log(x) - (0.5f * r);
            r *= r;

            result -= r * (s3 - (r * (s4 - (r * (s5 - (r * (s6 - (r * s7))))))));

            return result;
        }

    }
}
