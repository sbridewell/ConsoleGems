// <copyright file="XyzColour.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Models
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a color in the CIE XYZ color space.
    /// </summary>
    /// <remarks>
    /// The CIE XYZ color space is a linear color space defined by the International
    /// Commission on Illumination (CIE).
    /// It is based on human vision and serves as a device-independent model from which
    /// many other color spaces are derived.
    /// </remarks>
    /// <param name="X">
    /// The X component, representing the response of the color to a standard observer's
    /// red-sensitive cone (long wavelengths).
    /// </param>
    /// <param name="Y">
    /// The Y component, representing the response to the green-sensitive cone
    /// (medium wavelengths) and is also used as a measure of luminance (brightness).
    /// </param>
    /// <param name="Z">
    /// The Z component, representing the response to the blue-sensitive cone (short
    /// wavelengths).
    /// </param>
    [ExcludeFromCodeCoverage]
    public record XyzColour(double X, double Y, double Z);
}
