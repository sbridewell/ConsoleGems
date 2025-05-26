// <copyright file="ConsolePixel.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.FullScreen
{
    /// <summary>
    /// Represents a single pixel (character) in the console screen buffer.
    /// </summary>
    public class ConsolePixel
    {
        /// <summary>
        /// Gets or sets the character held in this pixel.
        /// </summary>
        public char Character { get; set; } = ' ';

        /// <summary>
        /// Gets or sets the output type of this pixel, e.g. default, warning, prompt.
        /// </summary>
        public ConsoleOutputType OutputType { get; set; } = ConsoleOutputType.Default;

        /// <summary>
        /// Gets or sets a value indicating whether the pixel has been updated since
        /// it was last painted to the console.
        /// </summary>
        public bool IsDirty { get; set; } = true;
    }
}
