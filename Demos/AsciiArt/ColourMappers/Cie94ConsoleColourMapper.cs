// <copyright file="Cie94ConsoleColourMapper.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.ColourMappers
{
    using Sde.AsciiArt.Models;

    /// <summary>
    /// Maps a pixel to the closest ConsoleColor using CIE94 colour difference in CIELAB space.
    /// </summary>
    public class Cie94ConsoleColourMapper : IConsoleColourMapper
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
                double dist = Cie94Difference(l, a, bLab, l2, a2, b2);
                if (dist < minDist)
                {
                    minDist = dist;
                    idx = i;
                }
            }

            return colors[idx];
        }

        // CIE94 colour difference formula
        private static double Cie94Difference(double l1, double a1, double b1, double l2, double a2, double b2)
        {
            double deltaL = l1 - l2;
            double c1 = Math.Sqrt((a1 * a1) + (b1 * b1));
            double c2 = Math.Sqrt((a2 * a2) + (b2 * b2));
            double deltaC = c1 - c2;
            double deltaA = a1 - a2;
            double deltaB = b1 - b2;
            double deltaH_sq = (deltaA * deltaA) + (deltaB * deltaB) - (deltaC * deltaC);
            double kL = 1.0, kC = 1.0, kH = 1.0;
            double k1 = 0.045, k2 = 0.015;
            double sL = 1.0;
            double sC = 1.0 + (k1 * c1);
            double sH = 1.0 + (k2 * c1);
            double termL = deltaL / (kL * sL);
            double termC = deltaC / (kC * sC);
            double termH = Math.Sqrt(Math.Max(0, deltaH_sq)) / (kH * sH);
            return (termL * termL) + (termC * termC) + (termH * termH);
        }
    }
}
