// <copyright file="WeightedRgbConsoleColourMapperTests.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Test.ColourMappers
{
    using System;
    using FluentAssertions;
    using Sde.AsciiArt;
    using Sde.AsciiArt.ColourMappers;
    using Sde.AsciiArt.Models;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="WeightedRgbConsoleColourMapper"/>.
    /// </summary>
    public class WeightedRgbConsoleColourMapperTests
    {
        private readonly WeightedRgbConsoleColourMapper mapper = new WeightedRgbConsoleColourMapper();

        /// <summary>
        /// Tests that known RGB values map to the expected ConsoleColor.
        /// </summary>
        /// <param name="r">Red component.</param>
        /// <param name="g">Green component.</param>
        /// <param name="b">Blue component.</param>
        /// <param name="expected">Expected ConsoleColor.</param>
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
        [InlineData(128, 0, 0, ConsoleColor.DarkRed)]
        [InlineData(0, 128, 0, ConsoleColor.DarkGreen)]
        [InlineData(0, 0, 128, ConsoleColor.DarkBlue)]
        [InlineData(128, 128, 0, ConsoleColor.DarkYellow)]
        [InlineData(0, 128, 128, ConsoleColor.DarkCyan)]
        [InlineData(128, 0, 128, ConsoleColor.DarkMagenta)]
        public void Maps_KnownRgb_To_ExpectedConsoleColor(int r, int g, int b, ConsoleColor expected)
        {
            var pixel = new Pixel(r, g, b);
            var result = this.mapper.MapToConsoleColor(pixel);
            result.Should().Be(expected);
        }

        /// <summary>
        /// Tests that ArgumentNullException is thrown when pixel is null.
        /// </summary>
        [Fact]
        public void Throws_ArgumentNullException_When_Pixel_Is_Null()
        {
            Action act = () => this.mapper.MapToConsoleColor(null!);
            act.Should().Throw<ArgumentNullException>().WithParameterName("pixel");
        }
    }
}
