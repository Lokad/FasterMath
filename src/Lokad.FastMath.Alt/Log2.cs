using System;
using System.Diagnostics;

namespace Lokad.FastMath.Alt
{
    public partial class AltMath
    {
        /// <remarks> Log2 on zero is undefined. </remarks>
        static readonly uint[] Log2Tab = {
 uint.MaxValue,  0, 58,  1, 59, 47, 53,  2,
            60, 39, 48, 27, 54, 33, 42,  3,
            61, 51, 37, 40, 49, 18, 28, 20,
            55, 30, 34, 11, 43, 14, 22,  4,
            62, 57, 46, 52, 38, 26, 32, 41,
            50, 36, 17, 19, 29, 10, 13, 21,
            56, 45, 25, 31, 35, 16,  9, 12,
            44, 24, 15,  8, 23,  7,  6,  5};

        public static uint Log2(ulong value)
        {
            Debug.Assert(value > 0);

            // TODO: [vermorel] probably to be moved to Lokad.Numerics with an intrinsic
            // https://stackoverflow.com/questions/11376288/fast-computing-of-log2-for-64-bit-integers

            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;
            value |= value >> 32;
            return Log2Tab[(ulong)((value - (value >> 1)) * 0x07EDD5E59A4E28C2) >> 58];
        }
    }
}
