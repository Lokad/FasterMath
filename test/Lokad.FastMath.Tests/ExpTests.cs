using System;
using System.Runtime.Intrinsics;
using Xunit;

namespace Lokad.FastMath.Tests
{
    public class ExpTests
    {
        [Fact]
        public void Exp256()
        {
            for(float i = -85; i < 85; i += 0.1f)
            {
                var x = Vector256.Create(i, i + 0.01f, i + 0.02f, i + 0.03f, i + 0.04f, i + 0.05f, i + 0.06f, i + 0.07f);
                var r = FastMath.Exp(x);

                for (var k = 0; k < 8; k++)
                {
                    Assert.True(MathF.Exp(i + k * 0.01f).RelError(r.GetElement(k)) <= 1.72886892e-3f);
                }
            }
        }

    }
}
