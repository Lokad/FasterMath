using System.Runtime.InteropServices;

namespace Lokad.FastMath
{
    /// <summary> Helper intended for quick-cast float into int (and vice-versa). </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    internal struct FloatIntUnion
    {
        [FieldOffset(0)]
        public float Float;

        [FieldOffset(0)]
        public int Int;
    }
}
