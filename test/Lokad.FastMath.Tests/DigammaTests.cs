using Xunit;

namespace Lokad.FastMath.Tests
{
    public class DigammaTests
    {
        [Fact]
        public void Digamma_float()
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
                var r = FastMath.Digamma(val);
                Assert.True(expected.RelError(r) < 1e-3f);
            }
        }
    }
}
