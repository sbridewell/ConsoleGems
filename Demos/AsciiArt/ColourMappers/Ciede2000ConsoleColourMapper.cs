// <copyright file="Ciede2000ConsoleColourMapper.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.ColourMappers
{
    using System;
    using Sde.AsciiArt.Models;

    /// <summary>
    /// Maps a pixel to the closest ConsoleColor using the CIEDE2000 colour difference in CIELAB space.
    /// </summary>
    public class Ciede2000ConsoleColourMapper : IConsoleColourMapper
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
                double dist = Ciede2000Difference(l, a, bLab, l2, a2, b2);
                if (dist < minDist)
                {
                    minDist = dist;
                    idx = i;
                }
            }

            return colors[idx];
        }

        /// <summary>
        /// Calculates the CIEDE2000 colour difference between two LAB colours.
        /// </summary>
        private static double Ciede2000Difference(double l1, double a1, double b1, double l2, double a2, double b2)
        {
            // Implementation based on the official CIEDE2000 formula
            // See: https://en.wikipedia.org/wiki/Color_difference#CIEDE2000
            double avgLp = (l1 + l2) / 2.0;
            double c1 = Math.Sqrt((a1 * a1) + (b1 * b1));
            double c2 = Math.Sqrt((a2 * a2) + (b2 * b2));
            double avgC = (c1 + c2) / 2.0;
            double g = 0.5 * (1 - Math.Sqrt(Math.Pow(avgC, 7) / (Math.Pow(avgC, 7) + Math.Pow(25.0, 7))));
            double a1p = (1 + g) * a1;
            double a2p = (1 + g) * a2;
            double c1p = Math.Sqrt((a1p * a1p) + (b1 * b1));
            double c2p = Math.Sqrt((a2p * a2p) + (b2 * b2));
            double avgCp = (c1p + c2p) / 2.0;
            double h1p = Math.Atan2(b1, a1p);
            if (h1p < 0)
            {
                h1p += 2 * Math.PI;
            }

            double h2p = Math.Atan2(b2, a2p);
            if (h2p < 0)
            {
                h2p += 2 * Math.PI;
            }

            double deltahp;
            if (Math.Abs(h1p - h2p) <= Math.PI)
            {
                deltahp = h2p - h1p;
            }
            else if (h2p <= h1p)
            {
                deltahp = h2p - h1p + (2 * Math.PI);
            }
            else
            {
                deltahp = h2p - h1p - (2 * Math.PI);
            }

            double deltaLp = l2 - l1;
            double deltaCp = c2p - c1p;
            double deltaHp = 2 * Math.Sqrt(c1p * c2p) * Math.Sin(deltahp / 2.0);
            double avgHp;
            if (Math.Abs(h1p - h2p) > Math.PI)
            {
                avgHp = (h1p + h2p + (2 * Math.PI)) / 2.0;
            }
            else
            {
                avgHp = (h1p + h2p) / 2.0;
            }

            double t = 1 - (0.17 * Math.Cos(avgHp - (Math.PI / 6)))
                          + (0.24 * Math.Cos(2 * avgHp))
                          + (0.32 * Math.Cos((3 * avgHp) + (Math.PI / 30)))
                          - (0.20 * Math.Cos((4 * avgHp) - ((21 * Math.PI) / 60)));
            double deltaTheta = 30 * Math.PI / 180 * Math.Exp(-Math.Pow(((avgHp * 180 / Math.PI) - 275) / 25, 2));
            double rc = 2 * Math.Sqrt(Math.Pow(avgCp, 7) / (Math.Pow(avgCp, 7) + Math.Pow(25.0, 7)));
            double sl = 1 + ((0.015 * Math.Pow(avgLp - 50, 2)) / Math.Sqrt(20 + Math.Pow(avgLp - 50, 2)));
            double sc = 1 + (0.045 * avgCp);
            double sh = 1 + (0.015 * avgCp * t);
            double rt = -Math.Sin(2 * deltaTheta) * rc;
            double dE = Math.Sqrt(
                Math.Pow(deltaLp / sl, 2) +
                Math.Pow(deltaCp / sc, 2) +
                Math.Pow(deltaHp / sh, 2) +
                (rt * (deltaCp / sc) * (deltaHp / sh)));
            return dE;
        }
    }
}
