// <copyright file="WeightedRgbConsoleColourMapper.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.ColourMappers
{
    using System;
    using Sde.AsciiArt.Models;

    /// <summary>
    /// Maps a pixel to the closest ConsoleColor using weighted Euclidean distance in RGB space.
    /// </summary>
    public class WeightedRgbConsoleColourMapper : IConsoleColourMapper
    {
        // Weights for R, G, B channels (tuned for human perception)
        private const double RedWeight = 0.3;
        private const double GreenWeight = 0.59;
        private const double BlueWeight = 0.11;

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

            return FindClosestWeightedRgb(r, g, b);
        }

        private static ConsoleColor FindClosestWeightedRgb(int r, int g, int b)
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

            double minDist = double.MaxValue;
            int idx = 0;
            for (int i = 0; i < rgbValues.Length; i++)
            {
                double dr = r - rgbValues[i][0];
                double dg = g - rgbValues[i][1];
                double db = b - rgbValues[i][2];
                double dist = (RedWeight * dr * dr) + (GreenWeight * dg * dg) + (BlueWeight * db * db);
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
