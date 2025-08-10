// <copyright file="BufferBuilder.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Generators
{
    using System;
    using System.Collections.Generic;
    using Sde.AsciiArt.CellMappers;
    using Sde.AsciiArt.ColourMappers;
    using Sde.AsciiArt.Models;
    using Sde.ConsoleGems.Consoles;

    /// <summary>
    /// Provides helper methods for building buffers for ASCII art lines using colour mappers.
    /// </summary>
    public static class BufferBuilder
    {
        /// <summary>
        /// Builds a buffer for a line of ASCII art using a colour mapper.
        /// </summary>
        /// <param name="y">The line index in the console output.</param>
        /// <param name="width">The width of the console output area.</param>
        /// <param name="consoleHeight">The height of the console output area.</param>
        /// <param name="imgWidth">The width of the source image.</param>
        /// <param name="pixelMatrix">The pixel matrix from the source image.</param>
        /// <param name="consoleColourMapper">The colour mapper to use.</param>
        /// <returns>An array of <see cref="AsciiArtGenerator.CharacterColourRun"/> for the line.</returns>
        public static AsciiArtGenerator.CharacterColourRun[] BuildColourMapperBuffer(
            int y, int width, int consoleHeight, int imgWidth, PixelMatrix pixelMatrix, IConsoleColourMapper consoleColourMapper)
        {
            int imgHeight = pixelMatrix.Height;
            int imgY = (int)Math.Round((double)y * imgHeight / consoleHeight);
            imgY = Math.Min(Math.Max(imgY, 0), imgHeight - 1);
            var buffer = new List<AsciiArtGenerator.CharacterColourRun>();
            int runStart = 0;
            ConsoleColor? currentColour = null;
            for (int x = 0; x <= width; x++)
            {
                ConsoleColor? colour = null;
                if (x < width)
                {
                    int imgX = (int)Math.Round((double)x * imgWidth / width);
                    imgX = Math.Min(Math.Max(imgX, 0), imgWidth - 1);
                    Pixel pixel = pixelMatrix.GetPixel(imgX, imgY);
                    colour = consoleColourMapper.MapToConsoleColor(pixel);
                }

                if (x == 0)
                {
                    currentColour = colour;
                    runStart = 0;
                }

                if (x == width || colour != currentColour)
                {
                    int runLength = x - runStart;
                    if (runLength > 0 && currentColour.HasValue)
                    {
                        string spaces = new string(' ', runLength);
                        buffer.Add(new AsciiArtGenerator.CharacterColourRun(spaces, new ConsoleColours(ConsoleColor.Black, currentColour.Value)));
                    }

                    runStart = x;
                    currentColour = colour;
                }
            }

            return buffer.ToArray();
        }

        /// <summary>
        /// Builds a buffer for a line of ASCII art using a cell mapper.
        /// </summary>
        /// <param name="y">The line index in the console output.</param>
        /// <param name="width">The width of the console output area.</param>
        /// <param name="consoleHeight">The height of the console output area.</param>
        /// <param name="imgWidth">The width of the source image.</param>
        /// <param name="pixelMatrix">The pixel matrix from the source image.</param>
        /// <param name="cellMapper">The cell mapper to use.</param>
        /// <returns>An array of <see cref="AsciiArtGenerator.CharacterColourRun"/> for the line.</returns>
        public static AsciiArtGenerator.CharacterColourRun[] BuildCellMapperBuffer(
            int y, int width, int consoleHeight, int imgWidth, PixelMatrix pixelMatrix, IAsciiArtCellMapper cellMapper)
        {
            int imgHeight = pixelMatrix.Height;
            int imgY = (int)Math.Round((double)y * imgHeight / consoleHeight);
            imgY = Math.Min(Math.Max(imgY, 0), imgHeight - 1);
            var buffer = new List<AsciiArtGenerator.CharacterColourRun>();
            int runStart = 0;
            char? currentChar = null;
            ConsoleColours? currentColours = null;
            for (int x = 0; x <= width; x++)
            {
                char? character = null;
                ConsoleColours? colours = null;
                if (x < width)
                {
                    int imgX = (int)Math.Round((double)x * imgWidth / width);
                    imgX = Math.Min(Math.Max(imgX, 0), imgWidth - 1);
                    Pixel pixel = pixelMatrix.GetPixel(imgX, imgY);
                    var cell = cellMapper.Map(pixel);
                    character = cell.Character;
                    colours = new ConsoleColours(cell.Foreground, cell.Background);
                }

                if (x == 0)
                {
                    currentChar = character;
                    currentColours = colours;
                    runStart = 0;
                }

                if (x == width || character != currentChar || (colours != null && !colours.Equals(currentColours)))
                {
                    int runLength = x - runStart;
                    if (runLength > 0 && currentChar.HasValue && currentColours != null)
                    {
                        string runText = new string(currentChar.Value, runLength);
                        buffer.Add(new AsciiArtGenerator.CharacterColourRun(runText, currentColours));
                    }

                    runStart = x;
                    currentChar = character;
                    currentColours = colours;
                }
            }

            return buffer.ToArray();
        }
    }
}
