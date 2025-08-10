// <copyright file="CieluvConsoleColourMapper.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.ColourMappers
{
    using System;
    using Sde.AsciiArt.Models;

    /// <summary>
    /// Maps a pixel to the closest ConsoleColor using the CIELUV colour difference in CIELUV space.
    /// </summary>
    public class CieluvConsoleColourMapper : IConsoleColourMapper
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

            LuvColour luv = RgbToLuv(r, g, b);
            double l = luv.L;
            double u = luv.U;
            double v = luv.V;

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
                LuvColour luv2 = RgbToLuv(rgbValues[i][0], rgbValues[i][1], rgbValues[i][2]);
                double l2 = luv2.L;
                double u2 = luv2.U;
                double v2 = luv2.V;
                double dist = Math.Pow(l - l2, 2) + Math.Pow(u - u2, 2) + Math.Pow(v - v2, 2);
                if (dist < minDist)
                {
                    minDist = dist;
                    idx = i;
                }
            }

            return colors[idx];
        }

        /// <summary>
        /// Converts RGB to CIELUV.
        /// </summary>
        private static LuvColour RgbToLuv(int r, int g, int b)
        {
            var norm = ConsoleColorMappingHelpers.NormalizeRgb(r, g, b);
            double rNorm = ConsoleColorMappingHelpers.RgbToLinear(norm.R);
            double gNorm = ConsoleColorMappingHelpers.RgbToLinear(norm.G);
            double bNorm = ConsoleColorMappingHelpers.RgbToLinear(norm.B);
            var xyz = ConsoleColorMappingHelpers.LinearRgbToXyz(rNorm, gNorm, bNorm);
            var refWhite = ConsoleColorMappingHelpers.D65ReferenceWhite;
            double epsilon = 1e-10;
            double denom = xyz.X + (15 * xyz.Y) + (3 * xyz.Z);
            double uPrime = Math.Abs(denom) < epsilon ? 0 : 4 * xyz.X / denom;
            double vPrime = Math.Abs(denom) < epsilon ? 0 : 9 * xyz.Y / denom;
            double refDenom = refWhite.X + (15 * refWhite.Y) + (3 * refWhite.Z);
            double refUPrime = 4 * refWhite.X / refDenom;
            double refVPrime = 9 * refWhite.Y / refDenom;
            double yRatio = xyz.Y / refWhite.Y;
            double l = yRatio > 0.008856 ? (116 * Math.Pow(yRatio, 1.0 / 3.0)) - 16 : 903.3 * yRatio;
            double u = 13 * l * (uPrime - refUPrime);
            double v = 13 * l * (vPrime - refVPrime);
            return new LuvColour(l, u, v);
        }
    }
}
