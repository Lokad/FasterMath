using Lokad.FastMath.Alt;
using System;
using Xunit;

namespace Lokad.FastMath.Tests
{
    public class Log2Tests
    {
        [Fact]
        public void Log2_uint()
        {
            for(var i = 1.0; i < uint.MaxValue; i *= 1.3)
            {
                var n = (uint)i;
                Assert.Equal((uint)Math.Log(n, 2.0), FastMath.Log2(n));
            }
        }

        [Fact]
        public void Log2_ulong()
        {
            for (var i = 1.0; i < uint.MaxValue; i *= 1.3)
            {
                var n = (ulong)i;
                Assert.Equal((ulong)Math.Log(n, 2.0), FastMath.Log2(n));
            }
        }

        [Fact]
        public void Log2_WithLookup()
        {
            for (var i = 1.0; i < uint.MaxValue; i *= 1.3)
            {
                var n = (ulong)i;
                Assert.Equal((uint)Math.Log(n, 2.0), AltMath.Log2(n));
            }
        }
    }
}
