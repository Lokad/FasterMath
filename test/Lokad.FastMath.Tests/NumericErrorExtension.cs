using System;

namespace Lokad.FastMath.Tests
{
    public static class NumericErrorExtension
    {
        public static float RelError(this float baseline, float other)
        {
            if (float.IsNaN(baseline) && float.IsNaN(other))
                return 0f;

            return Math.Abs(baseline - other) / baseline;
        }

        public static float AbsError(this float baseline, float other)
        {
            if (float.IsNaN(baseline) && float.IsNaN(other))
                return 0f;

            return Math.Abs(baseline - other);
        }
    }
}
