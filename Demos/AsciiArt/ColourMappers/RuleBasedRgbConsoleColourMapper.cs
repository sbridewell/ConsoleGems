// <copyright file="RuleBasedRgbConsoleColourMapper.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.ColourMappers
{
    using System;
    using Sde.AsciiArt.Models;

    /// <summary>
    /// Provides logic to map RGB values to the closest <see cref="ConsoleColor"/>.
    /// </summary>
    public class RuleBasedRgbConsoleColourMapper : IConsoleColourMapper
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

            // Try each mapping strategy in order
            return TryMapStrongPrimary(r, g, b)
                ?? TryMapSecondary(r, g, b)
                ?? TryMapPrimaryShade(r, g, b)
                ?? TryMapGray(r, g, b)
                ?? MapByEuclideanDistance(r, g, b);
        }

        // Extracted method: Rule-based mapping for strong primary colours
        private static ConsoleColor? TryMapStrongPrimary(int r, int g, int b)
        {
            if (ConsoleColorMappingHelpers.IsStrongRed(r, g, b))
            {
                return ConsoleColor.Red;
            }

            if (ConsoleColorMappingHelpers.IsStrongDarkRed(r, g, b))
            {
                return ConsoleColor.DarkRed;
            }

            if (ConsoleColorMappingHelpers.IsStrongGreen(r, g, b))
            {
                return ConsoleColor.Green;
            }

            if (ConsoleColorMappingHelpers.IsStrongDarkGreen(r, g, b))
            {
                return ConsoleColor.DarkGreen;
            }

            if (ConsoleColorMappingHelpers.IsStrongBlue(r, g, b))
            {
                return ConsoleColor.Blue;
            }

            if (ConsoleColorMappingHelpers.IsStrongDarkBlue(r, g, b))
            {
                return ConsoleColor.DarkBlue;
            }

            return null;
        }

        // Extracted method: Rule-based mapping for secondary colours
        private static ConsoleColor? TryMapSecondary(int r, int g, int b)
        {
            if (g > 200 && b > 200 && r < 80)
            {
                return ConsoleColor.Cyan;
            }

            if (r > 200 && b > 200 && g < 80)
            {
                return ConsoleColor.Magenta;
            }

            if (r > 200 && g > 200 && b < 80)
            {
                return ConsoleColor.Yellow;
            }

            return null;
        }

        // Extracted method: Rule-based mapping for primary shades
        private static ConsoleColor? TryMapPrimaryShade(int r, int g, int b)
        {
            int max = Math.Max(r, Math.Max(g, b));
            int min = Math.Min(r, Math.Min(g, b));
            int mid = r + g + b - max - min;
            int dominanceThreshold = 40;

            if (max - mid > dominanceThreshold && (mid - min > dominanceThreshold || min < 20))
            {
                if (max == r)
                {
                    if (r < 40)
                    {
                        return ConsoleColor.DarkRed;
                    }

                    if (r < 120)
                    {
                        return ConsoleColor.DarkRed;
                    }

                    return ConsoleColor.Red;
                }

                if (max == g)
                {
                    if (g < 40)
                    {
                        return ConsoleColor.DarkGreen;
                    }

                    if (g < 120)
                    {
                        return ConsoleColor.DarkGreen;
                    }

                    return ConsoleColor.Green;
                }

                if (max == b)
                {
                    if (b < 40)
                    {
                        return ConsoleColor.DarkBlue;
                    }

                    if (b < 120)
                    {
                        return ConsoleColor.DarkBlue;
                    }

                    return ConsoleColor.Blue;
                }
            }

            return null;
        }

        // Extracted method: Rule-based mapping for grays
        private static ConsoleColor? TryMapGray(int r, int g, int b)
        {
            if (Math.Abs(r - g) < 20 && Math.Abs(r - b) < 20 && Math.Abs(g - b) < 20)
            {
                if (r < 40)
                {
                    return ConsoleColor.Black;
                }

                if (r < 120)
                {
                    return ConsoleColor.DarkGray;
                }

                if (r < 200)
                {
                    return ConsoleColor.Gray;
                }

                return ConsoleColor.White;
            }

            return null;
        }

        // Extracted method: Fallback to closest ConsoleColor by Euclidean distance
        private static ConsoleColor MapByEuclideanDistance(int r, int g, int b)
        {
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

            int minDist = int.MaxValue;
            int idx = 0;
            for (int i = 0; i < rgbValues.Length; i++)
            {
                int dr = r - rgbValues[i][0];
                int dg = g - rgbValues[i][1];
                int db = b - rgbValues[i][2];
                int dist = (dr * dr) + (dg * dg) + (db * db);
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
