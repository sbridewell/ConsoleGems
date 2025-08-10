// <copyright file="ConsoleColorMappingHelpersTests.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Test.ColourMappers
{
    using System;
    using FluentAssertions;
    using Sde.AsciiArt.ColourMappers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="ConsoleColorMappingHelpers"/>.
    /// </summary>
    public class ConsoleColorMappingHelpersTests
    {
        /// <summary>
        /// Tests <see cref="ConsoleColorMappingHelpers.IsStrongRed(int, int, int)"/> for various RGB values.
        /// </summary>
        /// <param name="r">The red component (0-255).</param>
        /// <param name="g">The green component (0-255).</param>
        /// <param name="b">The blue component (0-255).</param>
        /// <param name="expected">The expected result.</param>
        [Theory]
        [InlineData(255, 0, 0, true)]
        [InlineData(201, 10, 10, true)]
        [InlineData(199, 0, 0, false)]
        [InlineData(255, 100, 100, false)]
        public void IsStrongRed_ReturnsExpected(int r, int g, int b, bool expected)
        {
            ConsoleColorMappingHelpers.IsStrongRed(r, g, b).Should().Be(expected);
        }

        /// <summary>
        /// Tests <see cref="ConsoleColorMappingHelpers.IsStrongDarkRed(int, int, int)"/> for various RGB values.
        /// </summary>
        /// <param name="r">The red component (0-255).</param>
        /// <param name="g">The green component (0-255).</param>
        /// <param name="b">The blue component (0-255).</param>
        /// <param name="expected">The expected result.</param>
        [Theory]
        [InlineData(150, 0, 0, true)]
        [InlineData(101, 10, 10, true)]
        [InlineData(200, 0, 0, true)]
        [InlineData(201, 0, 0, false)]
        [InlineData(100, 0, 0, false)]
        public void IsStrongDarkRed_ReturnsExpected(int r, int g, int b, bool expected)
        {
            ConsoleColorMappingHelpers.IsStrongDarkRed(r, g, b).Should().Be(expected);
        }

        /// <summary>
        /// Tests <see cref="ConsoleColorMappingHelpers.IsStrongGreen(int, int, int)"/> for various RGB values.
        /// </summary>
        /// <param name="r">The red component (0-255).</param>
        /// <param name="g">The green component (0-255).</param>
        /// <param name="b">The blue component (0-255).</param>
        /// <param name="expected">The expected result.</param>
        [Theory]
        [InlineData(0, 255, 0, true)]
        [InlineData(10, 201, 10, true)]
        [InlineData(0, 199, 0, false)]
        [InlineData(100, 255, 100, false)]
        public void IsStrongGreen_ReturnsExpected(int r, int g, int b, bool expected)
        {
            ConsoleColorMappingHelpers.IsStrongGreen(r, g, b).Should().Be(expected);
        }

        /// <summary>
        /// Tests <see cref="ConsoleColorMappingHelpers.IsStrongDarkGreen(int, int, int)"/> for various RGB values.
        /// </summary>
        /// <param name="r">The red component (0-255).</param>
        /// <param name="g">The green component (0-255).</param>
        /// <param name="b">The blue component (0-255).</param>
        /// <param name="expected">The expected result.</param>
        [Theory]
        [InlineData(0, 150, 0, true)]
        [InlineData(10, 101, 10, true)]
        [InlineData(0, 200, 0, true)]
        [InlineData(0, 201, 0, false)]
        [InlineData(0, 100, 0, false)]
        public void IsStrongDarkGreen_ReturnsExpected(int r, int g, int b, bool expected)
        {
            ConsoleColorMappingHelpers.IsStrongDarkGreen(r, g, b).Should().Be(expected);
        }

        /// <summary>
        /// Tests <see cref="ConsoleColorMappingHelpers.IsStrongBlue(int, int, int)"/> for various RGB values.
        /// </summary>
        /// <param name="r">The red component (0-255).</param>
        /// <param name="g">The green component (0-255).</param>
        /// <param name="b">The blue component (0-255).</param>
        /// <param name="expected">The expected result.</param>
        [Theory]
        [InlineData(0, 0, 255, true)]
        [InlineData(10, 10, 201, true)]
        [InlineData(0, 0, 199, false)]
        [InlineData(100, 100, 255, false)]
        public void IsStrongBlue_ReturnsExpected(int r, int g, int b, bool expected)
        {
            ConsoleColorMappingHelpers.IsStrongBlue(r, g, b).Should().Be(expected);
        }

        /// <summary>
        /// Tests <see cref="ConsoleColorMappingHelpers.IsStrongDarkBlue(int, int, int)"/> for various RGB values.
        /// </summary>
        /// <param name="r">The red component (0-255).</param>
        /// <param name="g">The green component (0-255).</param>
        /// <param name="b">The blue component (0-255).</param>
        /// <param name="expected">The expected result.</param>
        [Theory]
        [InlineData(0, 0, 150, true)]
        [InlineData(10, 10, 101, true)]
        [InlineData(0, 0, 200, true)]
        [InlineData(0, 0, 201, false)]
        [InlineData(0, 0, 100, false)]
        public void IsStrongDarkBlue_ReturnsExpected(int r, int g, int b, bool expected)
        {
            ConsoleColorMappingHelpers.IsStrongDarkBlue(r, g, b).Should().Be(expected);
        }

        /// <summary>
        /// Tests <see cref="ConsoleColorMappingHelpers.TryMapPrimaryShade(int, int, int)"/> for various RGB values.
        /// </summary>
        /// <param name="r">The red component (0-255).</param>
        /// <param name="g">The green component (0-255).</param>
        /// <param name="b">The blue component (0-255).</param>
        /// <param name="expected">The expected <see cref="ConsoleColor"/> or null.</param>
        [Theory]
        [InlineData(255, 0, 0, ConsoleColor.Red)]
        [InlineData(0, 255, 0, ConsoleColor.Green)]
        [InlineData(0, 0, 255, ConsoleColor.Blue)]
        [InlineData(120, 0, 0, ConsoleColor.DarkRed)]
        [InlineData(0, 120, 0, ConsoleColor.DarkGreen)]
        [InlineData(0, 0, 120, ConsoleColor.DarkBlue)]
        [InlineData(0, 0, 0, null)]
        public void TryMapPrimaryShade_ReturnsExpected(int r, int g, int b, ConsoleColor? expected)
        {
            ConsoleColorMappingHelpers.TryMapPrimaryShade(r, g, b).Should().Be(expected);
        }

        /// <summary>
        /// Tests <see cref="ConsoleColorMappingHelpers.MapByEuclideanDistance(int, int, int)"/> for various RGB values.
        /// </summary>
        /// <param name="r">The red component (0-255).</param>
        /// <param name="g">The green component (0-255).</param>
        /// <param name="b">The blue component (0-255).</param>
        /// <param name="expected">The expected <see cref="ConsoleColor"/>.</param>
        [Theory]
        [InlineData(255, 0, 0, ConsoleColor.Red)]
        [InlineData(0, 255, 0, ConsoleColor.Green)]
        [InlineData(0, 0, 255, ConsoleColor.Blue)]
        [InlineData(128, 0, 0, ConsoleColor.DarkRed)]
        [InlineData(0, 128, 0, ConsoleColor.DarkGreen)]
        [InlineData(0, 0, 128, ConsoleColor.DarkBlue)]
        [InlineData(255, 255, 255, ConsoleColor.White)]
        [InlineData(0, 0, 0, ConsoleColor.Black)]
        public void MapByEuclideanDistance_ReturnsExpected(int r, int g, int b, ConsoleColor expected)
        {
            ConsoleColorMappingHelpers.MapByEuclideanDistance(r, g, b).Should().Be(expected);
        }
    }
}
