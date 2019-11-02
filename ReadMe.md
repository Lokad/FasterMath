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
