using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Lokad.FastMath
{
    public partial class FastMath
    {
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
        /// Max. rel. error <= 1.72886892e-3 on [-87.33654, 88.72283].
        /// </summary>
        public unsafe static Vector256<float> Exp(Vector256<float> x)
        {
            Vector256<float> f, p, r;
            Vector256<int> t, j;

            fixed(float* bf = stackalloc float[5])
            fixed(int* bi = stackalloc int[1])
            {
                bf[0] = 12102203.0f;
                bi[0] = unchecked((int)0xff800000);
                bf[1] = 1.1920929e-7f;
                bf[2] = 0.3371894346f;
                bf[3] = 0.657636276f;
                bf[4] = 1.00172476f;

                var a = Avx.BroadcastScalarToVector256(&bf[0]); /* (1 << 23) / log(2) */
                var m = Avx2.BroadcastScalarToVector256(&bi[0]); /* mask for integer bits */
                var ttm23 = Avx.BroadcastScalarToVector256(&bf[1]); /* exp2(-23) */
                var c0 = Avx.BroadcastScalarToVector256(&bf[2]);
                var c1 = Avx.BroadcastScalarToVector256(&bf[3]);
                var c2 = Avx.BroadcastScalarToVector256(&bf[4]);

                t = Avx2.ConvertToVector256Int32(Avx2.Multiply(a, x));
                j = Avx2.And(t, m); /* j = (int)(floor (x/log(2))) << 23 */
                t = Avx2.Subtract(t, j);
                f = Avx2.Multiply(ttm23, Avx2.ConvertToVector256Single(t)); /* f = (x/log(2)) - floor (x/log(2)) */
                p = c0;                  /* c0 */
                p = Avx2.Multiply(p, f); /* c0 * f */
                p = Avx2.Add(p, c1);     /* c0 * f + c1 */
                p = Avx2.Multiply(p, f); /* (c0 * f + c1) * f */
                p = Avx2.Add(p, c2);     /* p = (c0 * f + c1) * f + c2 ~= 2^f */

                r = Avx2.Add(j, p.As<float, int>()).As<int, float>(); /* r = p * 2^i*/
                return r;
            }
        }
    }
}
