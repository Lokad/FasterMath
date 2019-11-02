using System;
using System.Runtime.Intrinsics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Lokad.FastMath.Alt;

namespace Lokad.FastMath.Tests.Bench
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ExpBench>();
            //var summary = BenchmarkRunner.Run<LogBench>();
            //var summary = BenchmarkRunner.Run<Log2Bench>();
        }
    }

    [RPlotExporter]
    public class ExpBench
    {
        public float X = 0.1f;

        public Vector256<float> X8 = Vector256.Create(0.1f);

        [Benchmark]
        public float Exp_System_MathF() => MathF.Exp(X);

        [Benchmark]
        public float Exp_System_Math() => (float)Math.Exp(X);

        [Benchmark]
        public float Exp_FastMath() => FastMath.Exp(X);

        [Benchmark]
        public Vector256<float> Exp_FastMath_F8() => FastMath.Exp(X8);
    }

    [RPlotExporter]
    public class LogBench
    {
        public float X = 0.1f;

        public Vector256<float> X8 = Vector256.Create(0.1f);

        [Benchmark]
        public float Log_System_MathF() => MathF.Log(X);

        [Benchmark]
        public float Log_System_Math() => (float)Math.Log(X);

        [Benchmark]
        public Vector256<float> Log_FastMath_F8() => FastMath.Log(X8);
    }

    [RPlotExporter]
    public class Log2Bench
    {
        public float X = 123;

        [Benchmark]
        public uint Log2_System_MathF() => (uint)MathF.Log(X, 2.0f);

        [Benchmark]
        public uint Log2_System_Math() => (uint)Math.Log(X, 2.0);

        [Benchmark]
        public uint Log2_FastMath_Uint() => FastMath.Log2(123);

        [Benchmark]
        public uint Log2_AltMath_WithLookup() => AltMath.Log2(123);
    }
}
