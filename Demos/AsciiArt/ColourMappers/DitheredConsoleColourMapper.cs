// <copyright file="DitheredConsoleColourMapper.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.ColourMappers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Sde.AsciiArt.Models;

    /// <summary>
    /// Maps pixels to ConsoleColor values using Floyd-Steinberg dithering and CIE76 colour difference in CIELAB space.
    /// </summary>
    public class DitheredConsoleColourMapper : IConsoleColourMapper
    {
        /// <summary>
        /// Applies Floyd-Steinberg dithering to a 2D array of pixels and returns a 2D array of ConsoleColor values.
        /// </summary>
        /// <param name="pixels">The 2D array of pixels to dither.</param>
        /// <returns>A 2D array of ConsoleColor values.</returns>
        [SuppressMessage(
            "Blocker Code Smell",
            "S2368:Public methods should not have multidimensional array parameters",
            Justification = "This is the best return type for this method")]
        public static ConsoleColor[,] Dither(Pixel[,] pixels)
        {
            if (pixels == null)
            {
                throw new ArgumentNullException(nameof(pixels));
            }

            int height = pixels.GetLength(0);
            int width = pixels.GetLength(1);
            var result = new ConsoleColor[height, width];
            var error = new double[height, width, 3];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Add propagated error
                    int r = ClampToByte(pixels[y, x].Red + error[y, x, 0]);
                    int g = ClampToByte(pixels[y, x].Green + error[y, x, 1]);
                    int b = ClampToByte(pixels[y, x].Blue + error[y, x, 2]);
                    var mapped = FindClosestConsoleColor(r, g, b);
                    result[y, x] = mapped;

                    // Get the RGB of the mapped color
                    var mappedRgb = GetConsoleColorRgb(mapped);
                    double errR = r - mappedRgb[0];
                    double errG = g - mappedRgb[1];
                    double errB = b - mappedRgb[2];

                    // Floyd-Steinberg error diffusion
                    if (x + 1 < width)
                    {
                        error[y, x + 1, 0] += errR * 7.0 / 16.0;
                        error[y, x + 1, 1] += errG * 7.0 / 16.0;
                        error[y, x + 1, 2] += errB * 7.0 / 16.0;
                    }

                    if (y + 1 < height)
                    {
                        if (x > 0)
                        {
                            error[y + 1, x - 1, 0] += errR * 3.0 / 16.0;
                            error[y + 1, x - 1, 1] += errG * 3.0 / 16.0;
                            error[y + 1, x - 1, 2] += errB * 3.0 / 16.0;
                        }

                        error[y + 1, x, 0] += errR * 5.0 / 16.0;
                        error[y + 1, x, 1] += errG * 5.0 / 16.0;
                        error[y + 1, x, 2] += errB * 5.0 / 16.0;
                        if (x + 1 < width)
                        {
                            error[y + 1, x + 1, 0] += errR * 1.0 / 16.0;
                            error[y + 1, x + 1, 1] += errG * 1.0 / 16.0;
                            error[y + 1, x + 1, 2] += errB * 1.0 / 16.0;
                        }
                    }
                }
            }

            return result;
        }

        /// <inheritdoc/>
        public ConsoleColor MapToConsoleColor(Pixel pixel)
        {
            if (pixel == null)
            {
                throw new ArgumentNullException(nameof(pixel));
            }

            return FindClosestConsoleColor(pixel.Red, pixel.Green, pixel.Blue);
        }

        private static int ClampToByte(double value)
        {
            return (int)Math.Max(0, Math.Min(255, Math.Round(value)));
        }

        /// <summary>
        /// Finds the closest ConsoleColor to the given RGB value using CIE76 in CIELAB space.
        /// </summary>
        private static ConsoleColor FindClosestConsoleColor(int r, int g, int b)
        {
            LabColour lab = ConsoleColorMappingHelpers.RgbToLab(r, g, b);
            double l = lab.L;
            double a = lab.A;
            double bLab = lab.B;
            ConsoleColor[] colors =
            {
                ConsoleColor.Black,
                ConsoleColor.DarkBlue,
                ConsoleColor.DarkGreen,
                ConsoleColor.DarkCyan,
                ConsoleColor.DarkRed,
                ConsoleColor.DarkMagenta,
                ConsoleColor.DarkYellow,
                ConsoleColor.Gray,
                ConsoleColor.DarkGray,
                ConsoleColor.Blue,
                ConsoleColor.Green,
                ConsoleColor.Cyan,
                ConsoleColor.Red,
                ConsoleColor.Magenta,
                ConsoleColor.Yellow,
                ConsoleColor.White,
            };
            int[][] rgbValues =
            {
                new[] { 0, 0, 0 },
                new[] { 0, 0, 128 },
                new[] { 0, 128, 0 },
                new[] { 0, 128, 128 },
                new[] { 128, 0, 0 },
                new[] { 128, 0, 128 },
                new[] { 128, 128, 0 },
                new[] { 192, 192, 192 },
                new[] { 128, 128, 128 },
                new[] { 0, 0, 255 },
                new[] { 0, 255, 0 },
                new[] { 0, 255, 255 },
                new[] { 255, 0, 0 },
                new[] { 255, 0, 255 },
                new[] { 255, 255, 0 },
                new[] { 255, 255, 255 },
            };
            double minDist = double.MaxValue;
            int idx = 0;
            for (int i = 0; i < rgbValues.Length; i++)
            {
                LabColour lab2 = ConsoleColorMappingHelpers.RgbToLab(rgbValues[i][0], rgbValues[i][1], rgbValues[i][2]);
                double l2 = lab2.L;
                double a2 = lab2.A;
                double b2 = lab2.B;
                double dist = Math.Pow(l - l2, 2) + Math.Pow(a - a2, 2) + Math.Pow(bLab - b2, 2);
                if (dist < minDist)
                {
                    minDist = dist;
                    idx = i;
                }
            }

            return colors[idx];
        }

        /// <summary>
        /// Gets the RGB value for a given ConsoleColor.
        /// </summary>
        private static int[] GetConsoleColorRgb(ConsoleColor color)
        {
            return color switch
            {
                ConsoleColor.Black => new[] { 0, 0, 0 },
                ConsoleColor.DarkBlue => new[] { 0, 0, 128 },
                ConsoleColor.DarkGreen => new[] { 0, 128, 0 },
                ConsoleColor.DarkCyan => new[] { 0, 128, 128 },
                ConsoleColor.DarkRed => new[] { 128, 0, 0 },
                ConsoleColor.DarkMagenta => new[] { 128, 0, 128 },
                ConsoleColor.DarkYellow => new[] { 128, 128, 0 },
                ConsoleColor.Gray => new[] { 192, 192, 192 },
                ConsoleColor.DarkGray => new[] { 128, 128, 128 },
                ConsoleColor.Blue => new[] { 0, 0, 255 },
                ConsoleColor.Green => new[] { 0, 255, 0 },
                ConsoleColor.Cyan => new[] { 0, 255, 255 },
                ConsoleColor.Red => new[] { 255, 0, 0 },
                ConsoleColor.Magenta => new[] { 255, 0, 255 },
                ConsoleColor.Yellow => new[] { 255, 255, 0 },
                ConsoleColor.White => new[] { 255, 255, 255 },
                _ => new[] { 0, 0, 0 },
            };
        }
    }
}
