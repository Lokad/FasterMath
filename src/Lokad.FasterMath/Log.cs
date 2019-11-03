using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Lokad.Numerics
{
    public partial class FastMath
    {
        // Source:
        // https://github.com/jhjourdan/SIMD-math-prims/blob/master/simd_math_prims.h

        ///* Absolute error bounded by 1e-6 for normalized inputs
        //    Returns a finite number for +inf input
        //    Returns -inf for nan and <= 0 inputs.
        //    Continuous error. */
        //inline float logapprox(float val) {
        //    union { float f; int32_t i; } valu;
        //    float exp, addcst, x;
        //    valu.f = val;
        //    exp = valu.i >> 23;
        //    /* -89.970756366f = -127 * log(2) + constant term of polynomial below. */
        //    addcst = val > 0 ? -89.970756366f : -(float)INFINITY;
        //    valu.i = (valu.i & 0x7FFFFF) | 0x3F800000;
        //    x = valu.f;


        //    /* Generated in Sollya using:
        //    > f = remez(log(x)-(x-1)*log(2),
        //            [|1,(x-1)*(x-2), (x-1)*(x-2)*x, (x-1)*(x-2)*x*x,
        //                (x-1)*(x-2)*x*x*x|], [1,2], 1, 1e-8);
        //    > plot(f+(x-1)*log(2)-log(x), [1,2]);
        //    > f+(x-1)*log(2)
        //    */
        //    return
        //    x * (3.529304993f + x * (-2.461222105f + x * (1.130626167f +
        //        x * (-0.288739945f + x * 3.110401639e-2f))))
        //    + (addcst + 0.6931471805f*exp);
        //}

        public static float Log(float x)
        {
            float exp, addcst, val;
            FloatIntUnion valu;

            valu.Int = 0; // HACK: work-around compiler error
            valu.Float = x;
            exp = valu.Int >> 23;

            addcst = x > 0 ? -89.970756366f : float.NaN;

            valu.Int = (valu.Int & 0x7FFFFF) | 0x3F800000;
            val = valu.Float;

            return val * (3.529304993f +
                    val * (-2.461222105f +
                      val * (1.130626167f +
                        val * (-0.288739945f +
                          val * 3.110401639e-2f))))
                + (addcst + 0.6931471805f * exp);
        }

        /// <summary>
        /// Absolute error bounded by 1e-4.
        /// </summary>
        /// <remarks>
        /// |           Method |      Mean |     Error |    StdDev |
        /// |----------------- |----------:|----------:|----------:|
        /// | Log_System_MathF |  3.809 ns | 0.1051 ns | 0.1869 ns |
        /// |  Log_System_Math | 11.931 ns | 0.2665 ns | 0.3370 ns |
        /// |     Log_FastMath |  6.591 ns | 0.1568 ns | 0.3022 ns |
        /// </remarks>
        public static Vector256<float> Log(Vector256<float> x)
        {
            Vector256<float> exp, addcst, val;

            exp = Avx2.ConvertToVector256Single(Avx2.ShiftRightArithmetic(x.As<float, int>(), 23));

            // According to BenchmarkDotNet, isolating all the constants up-front
            // yield nearly 10% speed-up.

            const float bf0 = -89.970756366f;
            const float bf1 = float.NaN; // behavior of MathF.Log() on negative numbers
            const float bf2 = 3.529304993f;
            const float bf3 = -2.461222105f;
            const float bf4 = 1.130626167f;
            const float bf5 = -0.288739945f;
            const float bf6 = 3.110401639e-2f;
            const float bf7 = 0.6931471805f;

            const int bi0 = 0x7FFFFF;
            const int bi1 = 0x3F800000;

            //addcst = val > 0 ? -89.970756366f : -(float)INFINITY;

            addcst = Avx.BlendVariable(Vector256.Create(bf0),
                Vector256.Create(bf1),
                Avx.Compare(x, Vector256<float>.Zero, FloatComparisonMode.OrderedLessThanNonSignaling));

            val = Avx2.Or(Avx2.And(
                    x.As<float, int>(),
                    Vector256.Create(bi0)),
                    Vector256.Create(bi1)).As<int, float>();

            /*    x * (3.529304993f + 
                    x * (-2.461222105f + 
                      x * (1.130626167f +
                        x * (-0.288739945f + 
                          x * 3.110401639e-2f))))
                + (addcst + 0.6931471805f*exp); */

            return Avx2.Add(
                   Avx2.Multiply(val, Avx2.Add(Vector256.Create(bf2),
                     Avx2.Multiply(val, Avx2.Add(Vector256.Create(bf3),
                       Avx2.Multiply(val, Avx2.Add(Vector256.Create(bf4),
                         Avx2.Multiply(val, Avx2.Add(Vector256.Create(bf5),
                           Avx2.Multiply(val, Vector256.Create(bf6)))))))))),
                   Avx.Add(addcst,
                     Avx2.Multiply(Vector256.Create(bf7), exp)));
        }
    }
}
