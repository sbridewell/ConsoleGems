// <copyright file="PixelLoader.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Generators
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.Versioning;
    using Sde.AsciiArt.Models;

    /// <summary>
    /// Provides functionality to load pixels from a bitmap image.
    /// </summary>
    public static class PixelLoader
    {
        /// <summary>
        /// Loads the pixels from the specified bitmap into a 2D array of <see cref="Pixel"/>.
        /// </summary>
        /// <param name="bitmap">The bitmap image.</param>
        /// <returns>A 2D array of <see cref="Pixel"/> representing the image.</returns>
        [SupportedOSPlatform("windows")]
        public static Pixel[,] LoadPixels(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            Pixel[,] pixels = new Pixel[width, height];
            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly,
                bitmap.PixelFormat);

            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        byte* pixelPtr = ptr + (y * data.Stride) + (x * bytesPerPixel);
                        int b = pixelPtr[0];
                        int g = pixelPtr[1];
                        int r = pixelPtr[2];
                        pixels[x, y] = new Pixel(r, g, b);
                    }
                }
            }

            bitmap.UnlockBits(data);

            return pixels;
        }
    }
}
