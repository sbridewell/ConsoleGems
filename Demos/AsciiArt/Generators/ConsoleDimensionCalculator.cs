// <copyright file="ConsoleDimensionCalculator.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Generators
{
    /// <summary>
    /// Helper class for calculating console dimensions for ASCII art rendering.
    /// </summary>
    public static class ConsoleDimensionCalculator
    {
        /// <summary>
        /// Calculates the console dimensions to preserve the aspect ratio of the image and ensure it fits within the console window.
        /// </summary>
        /// <param name="imgWidth">Image width.</param>
        /// <param name="imgHeight">Image height.</param>
        /// <param name="consoleWidth">Maximum console width.</param>
        /// <param name="consoleHeight">Maximum console height.</param>
        /// <param name="charAspectRatio">Character aspect ratio.</param>
        /// <returns>A <see cref="AsciiArtGenerator.ConsoleDimensions"/> record representing the output area.</returns>
        public static AsciiArtGenerator.ConsoleDimensions Calculate(int imgWidth, int imgHeight, int consoleWidth, int consoleHeight, double charAspectRatio)
        {
            double scaleW = consoleWidth / (imgWidth * charAspectRatio);
            double scaleH = consoleHeight / (double)imgHeight;
            double scale = Math.Min(scaleW, scaleH);
            int finalWidth = Math.Max(1, Math.Min(consoleWidth, (int)Math.Round(imgWidth * charAspectRatio * scale)));
            int finalHeight = Math.Max(1, Math.Min(consoleHeight, (int)Math.Round(imgHeight * scale)));
            return new AsciiArtGenerator.ConsoleDimensions(finalWidth, finalHeight);
        }
    }
}
