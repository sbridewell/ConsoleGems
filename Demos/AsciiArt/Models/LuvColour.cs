// <copyright file="LuvColour.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Models
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a color in the CIE LUV color space.
    /// </summary>
    /// <param name="L">The L* (lightness) component.</param>
    /// <param name="U">The u* (chromaticity) component.</param>
    /// <param name="V">The v* (chromaticity) component.</param>
    [ExcludeFromCodeCoverage]
    public record LuvColour(double L, double U, double V);
}
