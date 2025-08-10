// <copyright file="AsciiArtGenerator.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Generators
{
    using System;
    using System.Drawing;
    using System.Runtime.Versioning;
    using Sde.AsciiArt.CellMappers;
    using Sde.AsciiArt.ColourMappers;
    using Sde.AsciiArt.Models;
    using Sde.ConsoleGems.Consoles;

    /// <summary>
    /// Implementation of IAsciiArtGenerator for rendering images as ASCII art.
    /// </summary>
    public class AsciiArtGenerator
        : IAsciiArtGenerator
    {
        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        /// <param name="bitmap">The bitmap image.</param>
        /// <returns>The width of the image.</returns>
        [SupportedOSPlatform("windows")]
        public static int GetImageWidth(Bitmap bitmap)
        {
            return bitmap.Width;
        }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        /// <param name="bitmap">The bitmap image.</param>
        /// <returns>The height of the image.</returns>
        [SupportedOSPlatform("windows")]
        public static int GetImageHeight(Bitmap bitmap)
        {
            return bitmap.Height;
        }

        /// <inheritdoc/>
        [SupportedOSPlatform("windows")]
        public void RenderImageAsAsciiArt(string imagePath, IConsole console, IConsoleColourMapper consoleColourMapper)
        {
            ValidateArguments(imagePath, console, consoleColourMapper);
            using var bitmap = (Bitmap)Image.FromFile(imagePath);
            int imgWidth = GetImageWidth(bitmap);
            int imgHeight = GetImageHeight(bitmap);
            var dimensions = ConsoleDimensionCalculator.Calculate(imgWidth, imgHeight, console.WindowWidth, console.WindowHeight, 2.0);
            var pixelMatrix = new PixelMatrix(ConvertToJaggedArray(PixelLoader.LoadPixels(bitmap)));

            RenderHelper.RenderLines(
                dimensions.Width,
                dimensions.Height,
                (y, width) => BufferBuilder.BuildColourMapperBuffer(y, width, dimensions.Height, imgWidth, pixelMatrix, consoleColourMapper),
                console);
        }

        /// <inheritdoc/>
        [SupportedOSPlatform("windows")]
        public void RenderImageAsAsciiArt(string imagePath, IConsole console, IAsciiArtCellMapper cellMapper)
        {
            ValidateArguments(imagePath, console, cellMapper);
            using var bitmap = (Bitmap)Image.FromFile(imagePath);
            int imgWidth = GetImageWidth(bitmap);
            int imgHeight = GetImageHeight(bitmap);
            var dimensions = ConsoleDimensionCalculator.Calculate(imgWidth, imgHeight, console.WindowWidth, console.WindowHeight, 2.0);
            Pixel[,] pixels = PixelLoader.LoadPixels(bitmap);
            CursorHelper.WithCursorHidden(console, () =>
            {
                RenderHelper.RenderLines(
                    dimensions.Width,
                    dimensions.Height,
                    (y, width) => BuildCellMapperBuffer(y, width, dimensions.Height, imgWidth, pixels, cellMapper),
                    console);
            });
        }

        /// <summary>
        /// Validates required arguments for rendering.
        /// </summary>
        private static void ValidateArguments(string imagePath, IConsole console, object mapper)
        {
            if (imagePath == null)
            {
                throw new ArgumentNullException(nameof(imagePath));
            }

            if (console == null)
            {
                throw new ArgumentNullException(nameof(console));
            }

            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }
        }

        /// <summary>
        /// Builds a buffer for a line of ASCII art using a cell mapper.
        /// </summary>
        /// <param name="y">The line index in the console output.</param>
        /// <param name="width">The width of the console output area.</param>
        /// <param name="consoleHeight">The height of the console output area.</param>
        /// <param name="imgWidth">The width of the source image.</param>
        /// <param name="pixels">The pixel array from the source image.</param>
        /// <param name="cellMapper">The cell mapper to use.</param>
        /// <returns>An array of <see cref="CharacterColourRun"/> for the line.</returns>
        private static CharacterColourRun[] BuildCellMapperBuffer(
            int y, int width, int consoleHeight, int imgWidth, Pixel[,] pixels, IAsciiArtCellMapper cellMapper)
        {
            int imgHeight = pixels.GetLength(1);
            int imgY = (int)Math.Round((double)y * imgHeight / consoleHeight);
            imgY = Math.Min(Math.Max(imgY, 0), imgHeight - 1);
            var buffer = new List<CharacterColourRun>();
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
                    Pixel pixel = pixels[imgX, imgY];
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
                        buffer.Add(new CharacterColourRun(runText, currentColours));
                    }

                    runStart = x;
                    currentChar = character;
                    currentColours = colours;
                }
            }

            return buffer.ToArray();
        }

        private static List<List<Pixel>> ConvertToJaggedArray(Pixel[,] pixels)
        {
            int height = pixels.GetLength(1);
            int width = pixels.GetLength(0);
            var jagged = new List<List<Pixel>>(height);
            for (int y = 0; y < height; y++)
            {
                var row = new List<Pixel>(width);
                for (int x = 0; x < width; x++)
                {
                    row.Add(pixels[x, y]);
                }

                jagged.Add(row);
            }

            return jagged;
        }

        /// <summary>
        /// Represents the dimensions of the console output area.
        /// </summary>
        public record ConsoleDimensions
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ConsoleDimensions"/> class.
            /// </summary>
            /// <param name="width">The width of the console output area.</param>
            /// <param name="height">The height of the console output area.</param>
            public ConsoleDimensions(int width, int height)
            {
                this.Width = width;
                this.Height = height;
            }

            /// <summary>
            /// Gets the width of the console output area.
            /// </summary>
            public int Width { get; init; }

            /// <summary>
            /// Gets the height of the console output area.
            /// </summary>
            public int Height { get; init; }
        }

        /// <summary>
        /// Represents a run of characters and their associated console colours for output.
        /// </summary>
        public record CharacterColourRun
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CharacterColourRun"/> class.
            /// </summary>
            /// <param name="text">The text to output.</param>
            /// <param name="colours">The console colours for the run.</param>
            public CharacterColourRun(string text, ConsoleColours colours)
            {
                this.Text = text;
                this.Colours = colours;
            }

            /// <summary>
            /// Gets the text to output.
            /// </summary>
            public string Text { get; init; }

            /// <summary>
            /// Gets the console colours for the run.
            /// </summary>
            public ConsoleColours Colours { get; init; }
        }
    }
}
