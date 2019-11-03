using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Lokad.Numerics
{
    public partial class FastMath
    {
        // Source:
        // https://stackoverflow.com/questions/47025373/fastest-implementation-of-exponential-function-using-sse

        ///* max. rel. error <= 1.72886892e-3 on [-87.33654, 88.72283] */
        //__m128 fast_exp_sse(__m128 x)
        //{
        //    __m128 f, p, r;
        //    __m128i t, j;
        //    const __m128 a = _mm_set1_ps(12102203.0f); /* (1 << 23) / log(2) */
        //    const __m128i m = _mm_set1_epi32(0xff800000); /* mask for integer bits */
        //    const __m128 ttm23 = _mm_set1_ps(1.1920929e-7f); /* exp2(-23) */
        //    const __m128 c0 = _mm_set1_ps(0.3371894346f);
        //    const __m128 c1 = _mm_set1_ps(0.657636276f);
        //    const __m128 c2 = _mm_set1_ps(1.00172476f);

        //    t = _mm_cvtps_epi32(_mm_mul_ps(a, x));
        //    j = _mm_and_si128(t, m);            /* j = (int)(floor (x/log(2))) << 23 */
        //    t = _mm_sub_epi32(t, j);
        //    f = _mm_mul_ps(ttm23, _mm_cvtepi32_ps(t)); /* f = (x/log(2)) - floor (x/log(2)) */
        //    p = c0;                              /* c0 */
        //    p = _mm_mul_ps(p, f);               /* c0 * f */
        //    p = _mm_add_ps(p, c1);              /* c0 * f + c1 */
        //    p = _mm_mul_ps(p, f);               /* (c0 * f + c1) * f */
        //    p = _mm_add_ps(p, c2);              /* p = (c0 * f + c1) * f + c2 ~= 2^f */
        //    r = _mm_castsi128_ps(_mm_add_epi32(j, _mm_castps_si128(p))); /* r = p * 2^i*/
        //    return r;
        //}

        /// <summary>
        /// Max. relative error <= 1.72886892e-3 on [-87.33654, 88.72283].
        /// </summary>
        public static float Exp(float x)
        {
            float f, p;
            int t, j;
            FloatIntUnion r;

            const float a = 12102203.0f; /* (1 << 23) / log(2) */
            const int m = unchecked((int)0xff800000); /* mask for integer bits */
            const float ttm23 = 1.1920929e-7f; /* exp2(-23) */
            const float c0 = 0.3371894346f;
            const float c1 = 0.657636276f;
            const float c2 = 1.00172476f;

            t = (int)(a * x);
            j = t & m;      /* j = (int)(floor (x/log(2))) << 23 */
            t = t - j;
            f = ttm23 * t; /* f = (x/log(2)) - floor (x/log(2)) */
            p = c0;        /* c0 */
            p = p * f;     /* c0 * f */
            p = p + c1;    /* c0 * f + c1 */
            p = p * f;     /* (c0 * f + c1) * f */
            p = p + c2;    /* p = (c0 * f + c1) * f + c2 ~= 2^f */

            r.Int = 0; // HACK: work-around compiler error
            r.Float = p;
            r.Int = j + r.Int; /* r = p * 2^i*/
            return r.Float;
        }

        /// <summary>
        /// Max. relative error <= 1.72886892e-3 on [-87.33654, 88.72283].
        /// </summary>
        /// <remarks>
        /// |           Method |      Mean |     Error |    StdDev |
        /// |----------------- |----------:|----------:|----------:|
        /// | Exp_System_MathF |  3.369 ns | 0.1301 ns | 0.1866 ns |
        /// |  Exp_System_Math | 14.355 ns | 0.2615 ns | 0.2183 ns |
        /// |     Exp_FastMath |  4.104 ns | 0.1024 ns | 0.0957 ns |
        /// </remarks>
        public static Vector256<float> Exp(Vector256<float> x)
        {
            Vector256<float> f, p, r;
            Vector256<int> t, j;

            // According to BenchmarkDotNet, isolating all the constants up-front
            // yield nearly 10% speed-up.

            const float a = 12102203.0f; /* (1 << 23) / log(2) */
            const int m = unchecked((int)0xff800000); /* mask for integer bits */
            const float ttm23 = 1.1920929e-7f; /* exp2(-23) */
            const float c0 = 0.3371894346f;
            const float c1 = 0.657636276f;
            const float c2 = 1.00172476f;

            t = Avx2.ConvertToVector256Int32(Avx2.Multiply(Vector256.Create(a), x));
            j = Avx2.And(t, Vector256.Create(m));      /* j = (int)(floor (x/log(2))) << 23 */
            t = Avx2.Subtract(t, j);
            f = Avx2.Multiply(Vector256.Create(ttm23), Avx2.ConvertToVector256Single(t)); /* f = (x/log(2)) - floor (x/log(2)) */
            p = Vector256.Create(c0);                  /* c0 */
            p = Avx2.Multiply(p, f);                   /* c0 * f */
            p = Avx2.Add(p, Vector256.Create(c1));     /* c0 * f + c1 */
            p = Avx2.Multiply(p, f);                   /* (c0 * f + c1) * f */
            p = Avx2.Add(p, Vector256.Create(c2));     /* p = (c0 * f + c1) * f + c2 ~= 2^f */

            r = Avx2.Add(j, p.As<float, int>()).As<int, float>(); /* r = p * 2^i*/
            return r;
        }
    }
}
