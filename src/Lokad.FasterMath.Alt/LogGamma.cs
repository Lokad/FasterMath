﻿using System;

namespace Lokad.Numerics
{
    public partial class AltMath
    {
        // Source:
        // https://github.com/mathnet/mathnet-numerics/blob/master/src/Numerics/SpecialFunctions/Gamma.cs

        /// <summary>
        /// The order of the <see cref="GammaLn"/> approximation.
        /// </summary>
        const int GammaN = 10;

        /// <summary>
        /// Auxiliary variable when evaluating the <see cref="GammaLn"/> function.
        /// </summary>
        const double GammaR = 10.900511;

        /// <summary>The number log[e](pi)</summary>
        public const double LnPi = 1.1447298858494001741434273513530587116472948129153d;

        /// <summary>The number log(2 * sqrt(e / pi))</summary>
        public const double LogTwoSqrtEOverPi = 0.6207822376352452223455184457816472122518527279025978;

        /// <summary>
        /// Polynomial coefficients for the <see cref="GammaLn"/> approximation.
        /// </summary>
        static readonly double[] GammaDk =
        {
            2.48574089138753565546e-5,
            1.05142378581721974210,
            -3.45687097222016235469,
            4.51227709466894823700,
            -2.98285225323576655721,
            1.05639711577126713077,
            -1.95428773191645869583e-1,
            1.70970543404441224307e-2,
            -5.71926117404305781283e-4,
            4.63399473359905636708e-6,
            -2.71994908488607703910e-9
        };

        /// <summary>
        /// Computes the logarithm of the Gamma function.
        /// </summary>
        /// <param name="z">The argument of the gamma function.</param>
        /// <returns>The logarithm of the gamma function.</returns>
        /// <remarks>
        /// <para>This implementation of the computation of the gamma and logarithm of the gamma function follows the derivation in
        ///     "An Analysis Of The Lanczos Gamma Approximation", Glendon Ralph Pugh, 2004.
        /// We use the implementation listed on p. 116 which achieves an accuracy of 16 floating point digits. Although 16 digit accuracy
        /// should be sufficient for double values, improving accuracy is possible (see p. 126 in Pugh).</para>
        /// <para>Our unit tests suggest that the accuracy of the Gamma function is correct up to 14 floating point digits.</para>
        /// </remarks>
        public static double LogGamma(double z)
        {
            if (z < 0.5)
            {
                double s = GammaDk[0];
                for (int i = 1; i <= GammaN; i++)
                {
                    s += GammaDk[i] / (i - z);
                }

                return LnPi
                       - Math.Log(Math.Sin(Math.PI * z))
                       - Math.Log(s)
                       - LogTwoSqrtEOverPi
                       - ((0.5 - z) * Math.Log((0.5 - z + GammaR) / Math.E));
            }
            else
            {
                double s = GammaDk[0];
                for (int i = 1; i <= GammaN; i++)
                {
                    s += GammaDk[i] / (z + i - 1.0);
                }

                return Math.Log(s)
                       + LogTwoSqrtEOverPi
                       + ((z - 0.5) * Math.Log((z - 0.5 + GammaR) / Math.E));
            }
        }
    }
}
