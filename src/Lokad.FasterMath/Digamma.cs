using System.Diagnostics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

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

        public static Vector256<float> Digamma(Vector256<float> x)
        {
            const float s3 = 1.0f / 12.0f;
            const float s4 = 1.0f / 120.0f;
            const float s5 = 1.0f / 252.0f;
            const float s6 = 1.0f / 240.0f;
            const float s7 = 1.0f / 132.0f;
            const float half = 0.5f;
            const float one = 1f;

            var vone = Vector256.Create(one);

            // note: 'Reciprocal' does not yield the same numerical results than 'Divide(1f, x)' 

            var result = Vector256<float>.Zero;
            for(var i = 0; i < 3; i++)
            {
                result = Avx.Subtract(result, Avx.Divide(vone, x));
                x = Avx2.Add(x, vone);
            }

            var r = Avx.Divide(vone, x);
            result = Avx.Add(result, Avx.Subtract(Log(x), Avx.Multiply(Vector256.Create(half), r)));
            r = Avx.Multiply(r, r);

            result = Avx.Subtract(result,
                Avx.Multiply(r,
                    Avx.Subtract(Vector256.Create(s3),
                        Avx.Multiply(r,
                            Avx.Subtract(Vector256.Create(s4),
                                Avx.Multiply(r,
                                    Avx.Subtract(Vector256.Create(s5),
                                        Avx.Multiply(r,
                                            Avx.Subtract(Vector256.Create(s6),
                                                Avx.Multiply(r, Vector256.Create(s7)))))))))));

            return result;
        }
    }
}
