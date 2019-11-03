using System.Runtime.Intrinsics;
using Xunit;

namespace Lokad.Numerics.Tests
{
    public class DigammaTests
    {
        [Fact]
        public void Digamma_float_limited()
        {
            // Source: https://www.wolframalpha.com/ (2019-02-11)
            var results = new[] { 
                (0.1f, -10.4237549f),
                (0.8f, -0.965008567f),
                (1.5f, 0.03648997f),
                (15f, 2.67434666166f),
                (150f, 5.0072982570f)
            };

            foreach(var (val, expected) in results)
            {
                var r = FxMath.Digamma(val);
                Assert.True(expected.RelError(r) < 1e-3f);
            }
        }

        [Fact]
        public void Digamma_float()
        {
            for(var i = 0.001; i < 10000d; i *= 1.2)
            {
                var expected = (float)AltMath.Digamma(i);
                var r = FxMath.Digamma((float)i);
                Assert.True(expected.RelError(r) < 1e-3f);
            }
        }

        [Fact]
        public void Digamma_Vector256()
        {
            for (var i = 0.001; i < 10000d; i *= 1.2)
            {
                var r = FxMath.Digamma(Vector256.Create((float)i));
                var expected = FxMath.Digamma((float)i);

                for (var k = 0; k < 8; k++)
                {
                    Assert.Equal(expected, r.GetElement(k));
                }
            }
        }
    }
}
