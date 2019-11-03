using System.Runtime.Intrinsics;
using Xunit;

namespace Lokad.Numerics.Tests
{
    public class LogGammaTests
    {
        [Fact]
        public void LogGamma_float_limited()
        {
            // Source: https://www.wolframalpha.com/ (2019-02-11)
            var results = new[] {
                (0.1f, 2.252712651f),
                (0.8f, 0.152060f),
                (1.5f, -0.120782f),
                (15f, 25.191221f),
                (150f, 600.0094705f)
            };

            foreach (var (val, expected) in results)
            {
                var r = FxMath.LogGamma(val);
                Assert.True(expected.RelError(r) < 1e-4f);
            }
        }

        [Fact]
        public void LogGamma_float()
        {
            for (var i = 0.001; i < 10000d; i *= 1.2)
            {
                var expected = (float)AltMath.LogGamma(i);
                var r = FxMath.LogGamma((float)i);
                Assert.True(expected.RelError(r) < 1e-3f);
            }
        }

        [Fact]
        public void LogGamma_Vector256()
        {
            for (var i = 0.001; i < 10000d; i *= 1.2)
            {
                var r = FxMath.LogGamma(Vector256.Create((float)i));
                var expected = FxMath.LogGamma((float)i);

                for (var k = 0; k < 8; k++)
                {
                    Assert.Equal(expected, r.GetElement(k));
                }
            }
        }
    }
}
