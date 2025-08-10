// <copyright file="RuleBasedRgbConsoleColourMapperTests.cs" company="Simon Bridewell">
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
    /// Contains unit tests for the <see cref="RuleBasedRgbConsoleColourMapper"/> class.
    /// </summary>
    public class RuleBasedRgbConsoleColourMapperTests
    {
        /// <summary>
        /// Verifies that MapToConsoleColor returns the expected ConsoleColor for given RGB values.
        /// </summary>
        /// <param name="r">Red component.</param>
        /// <param name="g">Green component.</param>
        /// <param name="b">Blue component.</param>
        /// <param name="expected">Expected ConsoleColor.</param>
        [Theory]
        [ClassData(typeof(ConsoleColorMapperTestData))]
        public void MapToConsoleColor_ReturnsExpectedConsoleColor(int r, int g, int b, ConsoleColor expected)
        {
            var pixel = new Pixel(r, g, b);
            var mapper = new RuleBasedRgbConsoleColourMapper();
            var result = mapper.MapToConsoleColor(pixel);
            result.Should().Be(expected);
        }

        /// <summary>
        /// Verifies that MapToConsoleColor throws ArgumentNullException when pixel is null.
        /// </summary>
        [Fact]
        public void MapToConsoleColor_ThrowsArgumentNullException_WhenPixelIsNull()
        {
            var mapper = new RuleBasedRgbConsoleColourMapper();
            Action act = () => mapper.MapToConsoleColor(null!);
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
