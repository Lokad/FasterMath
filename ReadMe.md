# Lokad.FastMath

Author: Joannes Vermorel (Lokad, j.vermorel@lokad.com)

This library collects a short series of faster (but approximate)
mathematical functions leveraging hardware intrinsics in .NET.

The overall goal is to maintain a relative precision of the order
of 1e-3 for all the accelerated functions. This threshold loosely
matches most precision requirements from a "machine learning"
perspective.

## Requirements

* .NET Core 3.0+
* Modern CPU with AVX2

## Performance results

Legends:

* `AltMath` refers to reference implementations (typically highly accurate)
* The `F8` suffix indicates a SIMD implementation with `Vector256<float>`.

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i9-8950HK CPU 2.90GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT


|                  Method |       Mean |     Error |    StdDev |     Median |
|------------------------ |-----------:|----------:|----------:|-----------:|
|        Digamma_FastMath |  7.9170 ns | 0.1776 ns | 0.1744 ns |  7.9893 ns |
|     Digamma_FastMath_F8 | 17.7764 ns | 0.3655 ns | 0.3419 ns | 17.7850 ns |
|         Digamma_AltMath | 21.7903 ns | 0.2535 ns | 0.2247 ns | 21.7914 ns |
|        Exp_System_MathF |  3.1244 ns | 0.0755 ns | 0.0630 ns |  3.1207 ns |
|         Exp_System_Math | 14.2307 ns | 0.0843 ns | 0.0747 ns | 14.2200 ns |
|            Exp_FastMath |  1.9704 ns | 0.0671 ns | 0.1260 ns |  1.9491 ns |
|         Exp_FastMath_F8 |  3.7382 ns | 0.0970 ns | 0.0908 ns |  3.7398 ns |
|        Log_System_MathF |  3.7687 ns | 0.1423 ns | 0.1397 ns |  3.7460 ns |
|         Log_System_Math | 11.2308 ns | 0.2265 ns | 0.2119 ns | 11.1222 ns |
|            Log_FastMath |  2.7621 ns | 0.0836 ns | 0.1171 ns |  2.7509 ns |
|         Log_FastMath_F8 |  6.3745 ns | 0.1000 ns | 0.0936 ns |  6.3661 ns |
|       Log2_System_MathF | 12.6858 ns | 0.2752 ns | 0.4820 ns | 12.6605 ns |
|        Log2_System_Math | 16.6412 ns | 0.2213 ns | 0.1961 ns | 16.6913 ns |
|      Log2_FastMath_Uint |  0.0104 ns | 0.0161 ns | 0.0215 ns |  0.0000 ns |
| Log2_AltMath_WithLookup |  1.8607 ns | 0.0523 ns | 0.0437 ns |  1.8616 ns |
|       LogGamma_FastMath | 15.2935 ns | 0.0794 ns | 0.0620 ns | 15.2884 ns |
|    LogGamma_FastMath_F8 | 28.6478 ns | 0.4844 ns | 0.4531 ns | 28.4230 ns |
|        LogGamma_AltMath | 36.2495 ns | 0.7333 ns | 0.7846 ns | 36.0599 ns |
