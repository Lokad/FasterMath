# FasterMath by Lokad

> Author: Joannes Vermorel (Lokad, j.vermorel@lokad.com)

This library collects a short series of faster, but approximate, mathematical 
functions leveraging hardware intrinsics in .NET. The library maintains a 
relative precision of 1e-3 for the accelerated functions. 

The library has _no dependency_ and does _not_ rely on memoization techniques.
The goal is to make the most of the superscalar capabilities of modern CPUs,
without burdening the cache or the garbage collector.

_This library is licensed under the MIT licence._

## Requirements

* .NET Core 3.0+
* Modern CPU with AVX2

## Usage

```csharp
using Lokad.Numerics;
	
var x = FxMath.Log(123f); // scalar
ReadOnlySpan<float> myInputs = .. ; 
Span<float> myResults = .. ;
FxMath.Log(myInputs, myResults); // SIMD-accelerated
```

## Performance results

_The `S8` suffix indicates a SIMD implementation with `Vector256<float>`._


|               Method |       Mean |     Error |    StdDev |
|--------------------- |-----------:|----------:|----------:|
|       Digamma_FxMath |  8.1156 ns | 0.1911 ns | 0.2679 ns |
|    Digamma_FxMath_S8 | 18.4599 ns | 0.3741 ns | 0.3674 ns |
|            Exp_MathF |  3.4134 ns | 0.0962 ns | 0.2210 ns |
|             Exp_Math | 14.5137 ns | 0.2499 ns | 0.2338 ns |
|           Exp_FxMath |  2.0275 ns | 0.0706 ns | 0.0918 ns |
|        Exp_FxMath_S8 |  4.5711 ns | 0.1207 ns | 0.2297 ns |
|            Log_MathF |  3.9548 ns | 0.1100 ns | 0.2528 ns |
|             Log_Math | 11.2905 ns | 0.1388 ns | 0.1159 ns |
|           Log_FxMath |  2.7824 ns | 0.0856 ns | 0.1521 ns |
|        Log_FxMath_S8 |  6.4883 ns | 0.1236 ns | 0.1563 ns |
|           Log2_MathF | 13.0202 ns | 0.2853 ns | 0.3172 ns |
|            Log2_Math | 17.1575 ns | 0.3730 ns | 0.7187 ns |
|          Log2_FxMath |  0.0444 ns | 0.0252 ns | 0.0447 ns |
|      LogGamma_FxMath | 15.8183 ns | 0.3346 ns | 0.3436 ns |
|   LogGamma_FxMath_S8 | 28.8896 ns | 0.6744 ns | 1.0891 ns |

```BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i9-8950HK CPU 2.90GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT```

