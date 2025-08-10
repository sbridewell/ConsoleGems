// <copyright file="IAsciiArtCellMapper.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.CellMappers
{
    using Sde.AsciiArt.Models;

    /// <summary>
    /// Interface for mapping a colour in a source image to a combination
    /// of foeground and background colours in ASCII art, along with a
    /// character which controls the ratio of the foreground and background
    /// colours.
    /// </summary>
    public interface IAsciiArtCellMapper
    {
        /// <summary>
        /// Maps the supplied pixel to a combination of character, foreground
        /// colour and background colour.
        /// </summary>
        /// <param name="pixel">The pixel to map.</param>
        /// <returns>
        /// A record representing the character, foreground colour and background
        /// colour.
        /// </returns>
        AsciiArtCell Map(Pixel pixel);
    }
}
