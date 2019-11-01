using System;

namespace Lokad.FastMath.Tests
{
    public static class RelativeErrorExtension
    {
        public static float RelError(this float baseline, float other)
        {
            return Math.Abs(baseline - other) / baseline;
        }
    }
}
