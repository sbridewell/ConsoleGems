// <copyright file="Cie76ConsoleColourMapper.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.ColourMappers
{
    using System;
    using Sde.AsciiArt.Models;

    /// <summary>
    /// Maps a pixel to the closest ConsoleColor using CIE76 (?E*ab) in CIELAB colour space.
    /// </summary>
    public class Cie76ConsoleColourMapper : IConsoleColourMapper
    {
        /// <inheritdoc/>
        public ConsoleColor MapToConsoleColor(Pixel pixel)
        {
            if (pixel == null)
            {
                throw new ArgumentNullException(nameof(pixel));
            }

            int r = Math.Clamp(pixel.Red, 0, 255);
            int g = Math.Clamp(pixel.Green, 0, 255);
            int b = Math.Clamp(pixel.Blue, 0, 255);

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
    }
}
