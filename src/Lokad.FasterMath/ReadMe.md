# Lokad.FasterMath

High-performance low-precision mathematical operations in .NET
leveraging hardware intrinsics.

## Design considerations

The scalar and super-scalar variants must be numerically
identical. Indeed, when considering hybrid scenarios, client
code should not be "accidentally" correct by leveraging the
scalar flavor over the super-scalar one.
