using System;
using System.Runtime.Intrinsics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Lokad.FastMath.Tests.Bench
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<ExpBench>();
            var summary = BenchmarkRunner.Run<LogBench>();
        }
    }

    [RPlotExporter]
    public class ExpBench
    {
        public float X = 0.1f;

        public Vector256<float> XV = Vector256.Create(0.1f); 

        [Benchmark]
        public float MathExpF() => MathF.Exp(X);

        [Benchmark]
        public float MathExpD() => (float)Math.Exp(X);

        [Benchmark]
        public Vector256<float> FastMathExpF() => FastMath.Exp(XV);
    }

    [RPlotExporter]
    public class LogBench
    {
        public float X = 0.1f;

        public Vector256<float> XV = Vector256.Create(0.1f);

        [Benchmark]
        public float MathLogF() => MathF.Log(X);

        [Benchmark]
        public float MathExpD() => (float)Math.Log(X);

        [Benchmark]
        public Vector256<float> FastMathExpF() => FastMath.Log(XV);
    }
}
