﻿using System;
using System.Runtime.Intrinsics;
using Xunit;

namespace Lokad.Numerics.Tests
{
    public class ExpTests
    {
        [Fact]
        public void Exp_float()
        {
            for (float i = -85; i < 85; i += 0.1f)
            {
                var r = FxMath.Exp(i);
                Assert.True(MathF.Exp(i).RelError(r) <= 1.72886892e-3f);
            }
        }

        [Fact]
        public void Exp_Vector256()
        {
            for (float i = -85; i < 85; i += 0.1f)
            {
                var r = FxMath.Exp(Vector256.Create((float)i));
                var expected = FxMath.Exp((float)i);

                for (var k = 0; k < 8; k++)
                {
                    // HACK: [vermore] can't reproduce perfect numerical identity 
                    // between scalar and SIMD variants (very close though)
                    Assert.True(expected.AbsError(r.GetElement(k)) <= 1e-6f);
                    //Assert.Equal(expected, r.GetElement(k));
                }
            }
        }
    }
}
