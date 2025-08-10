// <copyright file="DitheredConsoleColourMapperTests.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Test.ColourMappers
{
    using System;
    using FluentAssertions;
    using Sde.AsciiArt.ColourMappers;
    using Sde.AsciiArt.Models;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="DitheredConsoleColourMapper"/>.
    /// </summary>
    public class DitheredConsoleColourMapperTests
    {
        private readonly DitheredConsoleColourMapper mapper = new DitheredConsoleColourMapper();

        /// <summary>
        /// Tests that ArgumentNullException is thrown when the input pixel array is null.
        /// </summary>
        [Fact]
        public void Throws_ArgumentNullException_When_Pixels_Is_Null()
        {
            Action act = () => DitheredConsoleColourMapper.Dither(null!);
            act.Should().Throw<ArgumentNullException>().WithParameterName("pixels");
        }

        /// <summary>
        /// Tests that ArgumentNullException is thrown when the input pixel is null.
        /// </summary>
        [Fact]
        public void Throws_ArgumentNullException_When_Pixel_Is_Null()
        {
            Action act = () => this.mapper.MapToConsoleColor(null!);
            act.Should().Throw<ArgumentNullException>().WithParameterName("pixel");
        }

        /// <summary>
        /// Tests that dithering a single red pixel maps to ConsoleColor.Red.
        /// </summary>
        [Fact]
        public void Dither_Maps_SinglePixel_Correctly()
        {
            var pixels = new Pixel[1, 1] { { new Pixel(255, 0, 0), }, };
            var result = DitheredConsoleColourMapper.Dither(pixels);
            result[0, 0].Should().Be(ConsoleColor.Red);
        }

        /// <summary>
        /// Tests that dithering a 2x2 gradient maps to the expected ConsoleColor values.
        /// </summary>
        [Fact]
        public void Dither_Maps_2x2_Gradient()
        {
            var pixels = new Pixel[2, 2]
            {
                { new Pixel(255, 0, 0), new Pixel(0, 255, 0), },
                { new Pixel(0, 0, 255), new Pixel(255, 255, 255) },
            };
            var result = DitheredConsoleColourMapper.Dither(pixels);
            result[0, 0].Should().Be(ConsoleColor.Red);
            result[0, 1].Should().Be(ConsoleColor.Green);
            result[1, 0].Should().Be(ConsoleColor.Blue);
            result[1, 1].Should().Be(ConsoleColor.White);
        }

        /// <summary>
        /// Tests that dithering handles quantization error and distributes it to neighbors.
        /// </summary>
        [Fact]
        public void Dither_Handles_Quantization_Error()
        {
            var pixels = new Pixel[2, 2]
            {
                { new Pixel(200, 0, 0), new Pixel(0, 200, 0), },
                { new Pixel(0, 0, 200), new Pixel(200, 200, 200), },
            };
            var result = DitheredConsoleColourMapper.Dither(pixels);
            result[0, 0].Should().BeOneOf(ConsoleColor.Red, ConsoleColor.DarkRed);
            result[0, 1].Should().BeOneOf(ConsoleColor.Green, ConsoleColor.DarkGreen);
            result[1, 0].Should().BeOneOf(ConsoleColor.Blue, ConsoleColor.DarkBlue);
            result[1, 1].Should().BeOneOf(ConsoleColor.White, ConsoleColor.Gray, ConsoleColor.DarkGray);
        }

        /// <summary>
        /// Tests that Dither handles edge cases for 1x2 and 2x1 arrays.
        /// </summary>
        [Fact]
        public void Dither_Handles_1x2_And_2x1()
        {
            var pixels1x2 = new Pixel[1, 2]
            {
                { new Pixel(255, 0, 0), new Pixel(0, 0, 255), },
            };
            var result1x2 = DitheredConsoleColourMapper.Dither(pixels1x2);
            result1x2[0, 0].Should().Be(ConsoleColor.Red);
            result1x2[0, 1].Should().Be(ConsoleColor.Blue);

            var pixels2x1 = new Pixel[2, 1]
            {
                { new Pixel(0, 255, 0), },
                { new Pixel(255, 255, 0), },
            };
            var result2x1 = DitheredConsoleColourMapper.Dither(pixels2x1);
            result2x1[0, 0].Should().Be(ConsoleColor.Green);
            result2x1[1, 0].Should().Be(ConsoleColor.Yellow);
        }

        /// <summary>
        /// Tests that Dither handles a 3x3 array and exercises all error diffusion branches.
        /// </summary>
        [Fact]
        public void Dither_Handles_3x3_ErrorDiffusion()
        {
            var pixels = new Pixel[3, 3]
            {
                { new Pixel(255, 0, 0), new Pixel(0, 255, 0), new Pixel(0, 0, 255), },
                { new Pixel(255, 255, 0), new Pixel(0, 255, 255), new Pixel(255, 0, 255), },
                { new Pixel(128, 128, 128), new Pixel(192, 192, 192), new Pixel(0, 0, 0), },
            };
            var result = DitheredConsoleColourMapper.Dither(pixels);
            result[0, 0].Should().Be(ConsoleColor.Red);
            result[0, 1].Should().Be(ConsoleColor.Green);
            result[0, 2].Should().Be(ConsoleColor.Blue);
            result[1, 0].Should().Be(ConsoleColor.Yellow);
            result[1, 1].Should().Be(ConsoleColor.Cyan);
            result[1, 2].Should().Be(ConsoleColor.Magenta);
            result[2, 0].Should().BeOneOf(ConsoleColor.DarkGray, ConsoleColor.Gray);
            result[2, 1].Should().BeOneOf(ConsoleColor.Gray, ConsoleColor.White, ConsoleColor.DarkGray);
            result[2, 2].Should().Be(ConsoleColor.Black);
        }

        /// <summary>
        /// Tests that MapToConsoleColor covers the FindClosestConsoleColor call for a variety of RGB values.
        /// </summary>
        /// <param name="r">The red channel value.</param>
        /// <param name="g">The green channel value.</param>
        /// <param name="b">The blue channel value.</param>
        /// <param name="expected">The expected ConsoleColor.</param>
        [Theory]
        [InlineData(255, 0, 0, ConsoleColor.Red)]
        [InlineData(0, 255, 0, ConsoleColor.Green)]
        [InlineData(0, 0, 255, ConsoleColor.Blue)]
        [InlineData(255, 255, 0, ConsoleColor.Yellow)]
        [InlineData(0, 255, 255, ConsoleColor.Cyan)]
        [InlineData(255, 0, 255, ConsoleColor.Magenta)]
        [InlineData(0, 0, 0, ConsoleColor.Black)]
        [InlineData(255, 255, 255, ConsoleColor.White)]
        [InlineData(128, 128, 128, ConsoleColor.DarkGray)]
        [InlineData(192, 192, 192, ConsoleColor.Gray)]
        public void MapToConsoleColor_Covers_FindClosestConsoleColor(int r, int g, int b, ConsoleColor expected)
        {
            var pixel = new Pixel(r, g, b);
            var result = this.mapper.MapToConsoleColor(pixel);
            result.Should().Be(expected);
        }

        /// <summary>
        /// Tests that GetConsoleColorRgb returns correct RGB values for all ConsoleColor values.
        /// </summary>
        /// <param name="color">The ConsoleColor value.</param>
        /// <param name="r">The expected red channel value.</param>
        /// <param name="g">The expected green channel value.</param>
        /// <param name="b">The expected blue channel value.</param>
        [Theory]
        [InlineData(ConsoleColor.Black, 0, 0, 0)]
        [InlineData(ConsoleColor.DarkBlue, 0, 0, 128)]
        [InlineData(ConsoleColor.DarkGreen, 0, 128, 0)]
        [InlineData(ConsoleColor.DarkCyan, 0, 128, 128)]
        [InlineData(ConsoleColor.DarkRed, 128, 0, 0)]
        [InlineData(ConsoleColor.DarkMagenta, 128, 0, 128)]
        [InlineData(ConsoleColor.DarkYellow, 128, 128, 0)]
        [InlineData(ConsoleColor.Gray, 192, 192, 192)]
        [InlineData(ConsoleColor.DarkGray, 128, 128, 128)]
        [InlineData(ConsoleColor.Blue, 0, 0, 255)]
        [InlineData(ConsoleColor.Green, 0, 255, 0)]
        [InlineData(ConsoleColor.Cyan, 0, 255, 255)]
        [InlineData(ConsoleColor.Red, 255, 0, 0)]
        [InlineData(ConsoleColor.Magenta, 255, 0, 255)]
        [InlineData(ConsoleColor.Yellow, 255, 255, 0)]
        [InlineData(ConsoleColor.White, 255, 255, 255)]
        public void GetConsoleColorRgb_Returns_CorrectRgb_For_All_Colors(ConsoleColor color, int r, int g, int b)
        {
            var rgb = typeof(DitheredConsoleColourMapper)
                .GetMethod("GetConsoleColorRgb", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                !.Invoke(null, new object[] { color }) as int[];
            rgb.Should().NotBeNull();
            rgb![0].Should().Be(r);
            rgb[1].Should().Be(g);
            rgb[2].Should().Be(b);
        }

        /// <summary>
        /// Tests that GetConsoleColorRgb returns black for an undefined ConsoleColor value.
        /// </summary>
        /// <remarks>
        /// This test ensures the default case in the switch expression is covered.
        /// </remarks>
        [Fact]
        public void GetConsoleColorRgb_Returns_Black_For_Undefined_Color()
        {
            var rgb = typeof(DitheredConsoleColourMapper)
                .GetMethod("GetConsoleColorRgb", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                !.Invoke(null, new object[] { (ConsoleColor)99 }) as int[];
            rgb.Should().NotBeNull();
            rgb![0].Should().Be(0);
            rgb[1].Should().Be(0);
            rgb[2].Should().Be(0);
        }
    }
}
