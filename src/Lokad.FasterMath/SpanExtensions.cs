using System;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Lokad.Numerics
{
	public partial class FastMath
    {
		public static void Digamma(ReadOnlySpan<float> values, Span<float> results)
        {
            var v = MemoryMarshal.Cast<float, Vector256<float>>(values);
            var r = MemoryMarshal.Cast<float, Vector256<float>>(results);

            for (var i = 0; i < r.Length; i++)
                r[i] = Digamma(v[i]);

            for (var i = r.Length * Vector256<float>.Count; i < results.Length; i++)
                results[i] = Digamma(values[i]);
        }

		public static void Exp(ReadOnlySpan<float> values, Span<float> results)
        {
            var v = MemoryMarshal.Cast<float, Vector256<float>>(values);
            var r = MemoryMarshal.Cast<float, Vector256<float>>(results);

            for (var i = 0; i < r.Length; i++)
                r[i] = Exp(v[i]);

            for (var i = r.Length * Vector256<float>.Count; i < results.Length; i++)
                results[i] = Exp(values[i]);
        }

		public static void Log(ReadOnlySpan<float> values, Span<float> results)
        {
            var v = MemoryMarshal.Cast<float, Vector256<float>>(values);
            var r = MemoryMarshal.Cast<float, Vector256<float>>(results);

            for (var i = 0; i < r.Length; i++)
                r[i] = Log(v[i]);

            for (var i = r.Length * Vector256<float>.Count; i < results.Length; i++)
                results[i] = Log(values[i]);
        }

		public static void LogGamma(ReadOnlySpan<float> values, Span<float> results)
        {
            var v = MemoryMarshal.Cast<float, Vector256<float>>(values);
            var r = MemoryMarshal.Cast<float, Vector256<float>>(results);

            for (var i = 0; i < r.Length; i++)
                r[i] = LogGamma(v[i]);

            for (var i = r.Length * Vector256<float>.Count; i < results.Length; i++)
                results[i] = LogGamma(values[i]);
        }

	}
}