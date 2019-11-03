using System;
using System.Runtime.Intrinsics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Lokad.Numerics.Bench
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<DigammaBench>();
            //var summary = BenchmarkRunner.Run<ExpBench>();
            //var summary = BenchmarkRunner.Run<LogBench>();
            //var summary = BenchmarkRunner.Run<Log2Bench>();
            //var summary = BenchmarkRunner.Run<LogGammaBench>();

            var summary = BenchmarkRunner.Run<AllBench>();
        }
    }

    [RPlotExporter]
    public class DigammaBench
    {
        public float DiGX = 4f;

        public Vector256<float> DigGX8 = Vector256.Create(4f);

        [Benchmark]
        public float Digamma_FxMath() => FxMath.Digamma(DiGX);

        [Benchmark]
        public Vector256<float> Digamma_FxMath_S8() => FxMath.Digamma(DigGX8);

        [Benchmark]
        public double Digamma_AltMath() => AltMath.Digamma(DiGX);
    }

    [RPlotExporter]
    public class ExpBench
    {
        public float ExpX = 0.1f;

        public Vector256<float> X8 = Vector256.Create(0.1f);

        [Benchmark]
        public float Exp_MathF() => MathF.Exp(ExpX);

        [Benchmark]
        public float Exp_Math() => (float)Math.Exp(ExpX);

        [Benchmark]
        public float Exp_FxMath() => FxMath.Exp(ExpX);

        [Benchmark]
        public Vector256<float> Exp_FxMath_S8() => FxMath.Exp(X8);
    }

    [RPlotExporter]
    public class LogBench
    {
        public float LogX = 0.1f;

        public Vector256<float> LogX8 = Vector256.Create(0.1f);

        [Benchmark]
        public float Log_MathF() => MathF.Log(LogX);

        [Benchmark]
        public float Log_Math() => (float)Math.Log(LogX);

        [Benchmark]
        public float Log_FxMath() => FxMath.Log(LogX);

        [Benchmark]
        public Vector256<float> Log_FxMath_S8() => FxMath.Log(LogX8);
    }

    [RPlotExporter]
    public class Log2Bench
    {
        public float Log2X = 123;

        [Benchmark]
        public uint Log2_MathF() => (uint)MathF.Log(Log2X, 2.0f);

        [Benchmark]
        public uint Log2_Math() => (uint)Math.Log(Log2X, 2.0);

        [Benchmark]
        public uint Log2_FxMath() => FxMath.Log2(123);

        [Benchmark]
        public uint Log2_AltMath_WithLookup() => AltMath.Log2(123);
    }

    [RPlotExporter]
    public class LogGammaBench
    {
        public float LogGX = 0.1f;

        public Vector256<float> LogGX8 = Vector256.Create(0.1f);

        [Benchmark]
        public float LogGamma_FxMath() => FxMath.LogGamma(LogGX);

        [Benchmark]
        public Vector256<float> LogGamma_FxMath_S8() => FxMath.LogGamma(LogGX8);

        [Benchmark]
        public double LogGamma_AltMath() => AltMath.LogGamma(LogGX);
    }
}