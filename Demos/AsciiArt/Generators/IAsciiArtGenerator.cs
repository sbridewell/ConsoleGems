// <copyright file="IAsciiArtGenerator.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Generators
{
    using Sde.AsciiArt.CellMappers;
    using Sde.AsciiArt.ColourMappers;
    using Sde.ConsoleGems.Consoles;

    /// <summary>
    /// Interface for reading an image file and writing it to the console as ASCII art.
    /// </summary>
    public interface IAsciiArtGenerator
    {
        /// <summary>
        /// Reads the image file at the supplied path and renders it to the console as ASCII art.
        /// </summary>
        /// <param name="imagePath">Path to the image file.</param>
        /// <param name="console">The console to write to.</param>
        /// <param name="consoleColourMapper">Colour mapper to use for mapping pixels to console colors.</param>
        void RenderImageAsAsciiArt(string imagePath, IConsole console, IConsoleColourMapper consoleColourMapper);

        /// <summary>
        /// Reads the image file at the supplied path and renders it to the console as ASCII art using a cell mapper.
        /// </summary>
        /// <param name="imagePath">Path to the image file.</param>
        /// <param name="console">The console to write to.</param>
        /// <param name="cellMapper">Cell mapper to use for mapping pixels to ASCII art cells.</param>
        void RenderImageAsAsciiArt(string imagePath, IConsole console, IAsciiArtCellMapper cellMapper);
    }
}
