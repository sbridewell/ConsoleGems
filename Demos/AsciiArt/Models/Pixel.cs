// <copyright file="Pixel.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Models
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a pixel with red, green, and blue components.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("R={Red}, G={Green}, B={Blue}")]
    [ExcludeFromCodeCoverage]
    public class Pixel(int red, int green, int blue)
    {
        /// <summary>
        /// Gets or sets the red component (0-255).
        /// </summary>
        public int Red { get; set; } = red;

        /// <summary>
        /// Gets or sets the green component (0-255).
        /// </summary>
        public int Green { get; set; } = green;

        /// <summary>
        /// Gets or sets the blue component (0-255).
        /// </summary>
        public int Blue { get; set; } = blue;
    }
}
