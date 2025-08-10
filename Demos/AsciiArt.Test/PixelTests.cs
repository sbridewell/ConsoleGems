// <copyright file="PixelTests.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Test
{
    using FluentAssertions;
    using Sde.AsciiArt;
    using Sde.AsciiArt.Models;
    using Xunit;

    /// <summary>
    /// Contains unit tests for the Pixel class.
    /// </summary>
    public class PixelTests
    {
        /// <summary>
        /// Verifies that the constructor sets RGB values correctly.
        /// </summary>
        /// <param name="r">Red component.</param>
        /// <param name="g">Green component.</param>
        /// <param name="b">Blue component.</param>
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(255, 128, 64)]
        [InlineData(10, 20, 30)]
        public void Constructor_SetsRgbValues(int r, int g, int b)
        {
            var pixel = new Pixel(r, g, b);
            pixel.Red.Should().Be(r);
            pixel.Green.Should().Be(g);
            pixel.Blue.Should().Be(b);
        }

        /// <summary>
        /// Verifies that properties can be set and retrieved.
        /// </summary>
        [Fact]
        public void Properties_CanBeSetAndGet()
        {
            var pixel = new Pixel(0, 0, 0)
            {
                Red = 100,
                Green = 150,
                Blue = 200,
            };
            pixel.Red.Should().Be(100);
            pixel.Green.Should().Be(150);
            pixel.Blue.Should().Be(200);
        }
    }
}
