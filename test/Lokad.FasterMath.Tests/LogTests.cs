using System;
using System.Collections.Generic;
using System.Linq;
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
                var r = FxMath.Log(i);
                Assert.True(MathF.Log(i).AbsError(r) <= 1e-4f);
            }
        }

        [Fact]
        public void Log_Vector256()
        {
            for (float i = -1; i < 85; i += 0.1f)
            {
                var r = FxMath.Log(Vector256.Create((float)i));
                var expected = FxMath.Log((float)i);

                for (var k = 0; k < 8; k++)
                {
                    Assert.Equal(expected, r.GetElement(k));
                }
            }
        }

        [Fact]
        public void Log_Span()
        {
            var pairs = new List<(float, float)>();
            for (float i = -1; i < 85; i += 0.1f)
            {
                pairs.Add((i, FxMath.Log(i)));
            }

            var inputs = pairs.Select(tu => tu.Item1).ToArray();
            var results = new float[pairs.Count];

            FxMath.Log(inputs, results);

            for(var i = 0; i < inputs.Length; i++)
            {
                Assert.Equal(pairs[i].Item2, results[i]);
            }
        }
    }
}
