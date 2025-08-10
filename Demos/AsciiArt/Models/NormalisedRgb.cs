// <copyright file="NormalisedRgb.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Models
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents normalized RGB color components in the range [0, 1].
    /// </summary>
    /// <param name="R">The normalized red component.</param>
    /// <param name="G">The normalized green component.</param>
    /// <param name="B">The normalized blue component.</param>
    [ExcludeFromCodeCoverage]
    public record NormalisedRgb(double R, double G, double B);
}
