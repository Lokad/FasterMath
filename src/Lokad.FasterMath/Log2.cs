using System.Diagnostics;
using System.Runtime.Intrinsics.X86;


namespace Lokad.Numerics
{
    public partial class FastMath
    {
        public static uint Log2(uint value)
        {
            Debug.Assert(value > 0);

            return 32 - 1 - Lzcnt.LeadingZeroCount(value);
        }

        public static ulong Log2(ulong value)
        {
            Debug.Assert(value > 0);

            return 64 - 1 - Lzcnt.X64.LeadingZeroCount(value);
        }


    }
}
