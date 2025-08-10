// <copyright file="LabColour.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Models
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a color in the CIE LAB color space.
    /// </summary>
    /// <param name="L">The L* (lightness) component.</param>
    /// <param name="A">The a* (green-red) component.</param>
    /// <param name="B">The b* (blue-yellow) component.</param>
    [ExcludeFromCodeCoverage]
    public record LabColour(double L, double A, double B);
}
