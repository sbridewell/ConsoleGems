// <copyright file="ConsoleColorMappingHelpers.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.ColourMappers
{
    using Sde.AsciiArt.Models;

    /// <summary>
    /// Provides static helper methods for mapping RGB values to <see cref="ConsoleColor"/>.
    /// </summary>
    public static class ConsoleColorMappingHelpers
    {
        /// <summary>
        /// Gets the D65 reference white point (X, Y, Z).
        /// </summary>
        public static XyzColour D65ReferenceWhite => new XyzColour(0.95047, 1.00000, 1.08883);

        /// <summary>
        /// Determines if the color is a strong red (high red, low green and blue).
        /// </summary>
        /// <param name="r">The red component (0-255).</param>
        /// <param name="g">The green component (0-255).</param>
        /// <param name="b">The blue component (0-255).</param>
        /// <returns>True if the color is a strong red; otherwise, false.</returns>
        public static bool IsStrongRed(int r, int g, int b) => r > 200 && g < 80 && b < 80;

        /// <summary>
        /// Determines if the color is a strong dark red (medium red, low green and blue).
        /// </summary>
        /// <param name="r">The red component (0-255).</param>
        /// <param name="g">The green component (0-255).</param>
        /// <param name="b">The blue component (0-255).</param>
        /// <returns>True if the color is a strong dark red; otherwise, false.</returns>
        public static bool IsStrongDarkRed(int r, int g, int b) => r > 100 && r <= 200 && g < 80 && b < 80;

        /// <summary>
        /// Determines if the color is a strong green (high green, low red and blue).
        /// </summary>
        /// <param name="r">The red component (0-255).</param>
        /// <param name="g">The green component (0-255).</param>
        /// <param name="b">The blue component (0-255).</param>
        /// <returns>True if the color is a strong green; otherwise, false.</returns>
        public static bool IsStrongGreen(int r, int g, int b) => g > 200 && r < 80 && b < 80;

        /// <summary>
        /// Determines if the color is a strong dark green (medium green, low red and blue).
        /// </summary>
        /// <param name="r">The red component (0-255).</param>
        /// <param name="g">The green component (0-255).</param>
        /// <param name="b">The blue component (0-255).</param>
        /// <returns>True if the color is a strong dark green; otherwise, false.</returns>
        public static bool IsStrongDarkGreen(int r, int g, int b) => g > 100 && g <= 200 && r < 80 && b < 80;

        /// <summary>
        /// Determines if the color is a strong blue (high blue, low red and green).
        /// </summary>
        /// <param name="r">The red component (0-255).</param>
        /// <param name="g">The green component (0-255).</param>
        /// <param name="b">The blue component (0-255).</param>
        /// <returns>True if the color is a strong blue; otherwise, false.</returns>
        public static bool IsStrongBlue(int r, int g, int b) => b > 200 && r < 80 && g < 80;

        /// <summary>
        /// Determines if the color is a strong dark blue (medium blue, low red and green).
        /// </summary>
        /// <param name="r">The red component (0-255).</param>
        /// <param name="g">The green component (0-255).</param>
        /// <param name="b">The blue component (0-255).</param>
        /// <returns>True if the color is a strong dark blue; otherwise, false.</returns>
        public static bool IsStrongDarkBlue(int r, int g, int b) => b > 100 && b <= 200 && r < 80 && g < 80;

        /// <summary>
        /// Rule-based mapping for primary shades (red, green, blue) and their dark variants.
        /// </summary>
        /// <param name="r">The red component (0-255).</param>
        /// <param name="g">The green component (0-255).</param>
        /// <param name="b">The blue component (0-255).</param>
        /// <returns>The mapped <see cref="ConsoleColor"/>, or null if no match.</returns>
        /// <remarks>
        /// Uses dominance thresholds to determine if one channel is much higher than the others.
        /// Fix: Map 120,0,0 to DarkRed, not Red. Use stricter thresholds for Red/DarkRed.
        /// </remarks>
        public static ConsoleColor? TryMapPrimaryShade(int r, int g, int b)
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

                    if (r < 128)
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

                    if (g < 128)
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

                    // Fix: Use 128 as the threshold for DarkBlue, matching test expectations
                    if (b < 128)
                    {
                        return ConsoleColor.DarkBlue;
                    }

                    return ConsoleColor.Blue;
                }
            }

            return null;
        }

        /// <summary>
        /// Fallback mapping: finds the closest <see cref="ConsoleColor"/> by Euclidean distance in RGB space.
        /// </summary>
        /// <param name="r">The red component (0-255).</param>
        /// <param name="g">The green component (0-255).</param>
        /// <param name="b">The blue component (0-255).</param>
        /// <returns>The closest <see cref="ConsoleColor"/>.</returns>
        /// <remarks>
        /// Used for ambiguous or less common colors not caught by rule-based mapping.
        /// </remarks>
        public static ConsoleColor MapByEuclideanDistance(int r, int g, int b)
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

        /// <summary>
        /// Converts sRGB value to linear RGB.
        /// </summary>
        /// <param name="c">The sRGB value (0.0-1.0).</param>
        /// <returns>The linear RGB value.</returns>
        public static double RgbToLinear(double c)
        {
            return c <= 0.04045 ? c / 12.92 : Math.Pow((c + 0.055) / 1.055, 2.4);
        }

        /// <summary>
        /// Converts linear RGB to XYZ (D65 reference white).
        /// </summary>
        /// <param name="r">Linear R (0.0-1.0).</param>
        /// <param name="g">Linear G (0.0-1.0).</param>
        /// <param name="b">Linear B (0.0-1.0).</param>
        /// <returns>Tuple (x, y, z).</returns>
        public static XyzColour LinearRgbToXyz(double r, double g, double b)
        {
            double x = (r * 0.4124) + (g * 0.3576) + (b * 0.1805);
            double y = (r * 0.2126) + (g * 0.7152) + (b * 0.0722);
            double z = (r * 0.0193) + (g * 0.1192) + (b * 0.9505);
            return new XyzColour(x, y, z);
        }

        /// <summary>
        /// Normalizes RGB integer values to the [0,1] range.
        /// </summary>
        /// <param name="r">Red component (0-255).</param>
        /// <param name="g">Green component (0-255).</param>
        /// <param name="b">Blue component (0-255).</param>
        /// <returns>Tuple of normalized (r, g, b) in [0,1].</returns>
        public static NormalisedRgb NormalizeRgb(int r, int g, int b)
        {
            return new NormalisedRgb(r / 255.0, g / 255.0, b / 255.0);
        }

        /// <summary>
        /// Converts RGB (0-255) to CIELAB (L*, a*, b*) using D65 reference white.
        /// </summary>
        /// <param name="r">Red component (0-255).</param>
        /// <param name="g">Green component (0-255).</param>
        /// <param name="b">Blue component (0-255).</param>
        /// <returns>Tuple (L*, a*, b*).</returns>
        public static LabColour RgbToLab(int r, int g, int b)
        {
            var norm = NormalizeRgb(r, g, b);
            double rNorm = RgbToLinear(norm.R);
            double gNorm = RgbToLinear(norm.G);
            double bNorm = RgbToLinear(norm.B);
            var xyz = LinearRgbToXyz(rNorm, gNorm, bNorm);
            double x = xyz.X / D65ReferenceWhite.X;
            double y = xyz.Y / D65ReferenceWhite.Y;
            double z = xyz.Z / D65ReferenceWhite.Z;
            x = XyzToLabHelper(x);
            y = XyzToLabHelper(y);
            z = XyzToLabHelper(z);
            double l = (116 * y) - 16;
            double a = 500 * (x - y);
            double bLab = 200 * (y - z);
            return new LabColour(l, a, bLab);
        }

        /// <summary>
        /// Helper for XYZ to LAB conversion.
        /// </summary>
        /// <param name="t">Normalized XYZ value.</param>
        /// <returns>Transformed value for LAB conversion.</returns>
        public static double XyzToLabHelper(double t)
        {
            return t > 0.008856 ? Math.Pow(t, 1.0 / 3.0) : (7.787 * t) + (16.0 / 116.0);
        }
    }
}
