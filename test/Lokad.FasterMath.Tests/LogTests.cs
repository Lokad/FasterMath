using System;
using System.Runtime.Intrinsics;
using Xunit;

namespace Lokad.Numerics.Tests
{
    public class LogTests
    {
        [Fact]
        public void Log_float()
        {
            for (float i = -1; i < 85; i += 0.1f)
            {
                var r = FastMath.Log(i);
                Assert.True(MathF.Log(i).AbsError(r) <= 1e-4f);
            }
        }

        [Fact]
        public void Log_Vector256()
        {
            for (float i = -1; i < 85; i += 0.1f)
            {
                var r = FastMath.Log(Vector256.Create((float)i));
                var expected = FastMath.Log((float)i);

                for (var k = 0; k < 8; k++)
                {
                    Assert.Equal(expected, r.GetElement(k));
                }
            }
        }
    }
}
