// <copyright file="BlendingCellMapperTests.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Test
{
    using FluentAssertions;
    using Sde.AsciiArt.CellMappers;
    using Sde.AsciiArt.CharacterBlenders;
    using Sde.AsciiArt.Models;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="BlendingCellMapper"/> class.
    /// </summary>
    public class BlendingCellMapperTests
    {
        /// <summary>
        /// Verifies that Map returns a valid AsciiArtCell for a set of known colors.
        /// </summary>
        /// <param name="r">The red component of the color.</param>
        /// <param name="g">The green component of the color.</param>
        /// <param name="b">The blue component of the color.</param>
        [Theory]
        [InlineData(255, 0, 0)] // Red
        [InlineData(0, 255, 0)] // Green
        [InlineData(0, 0, 255)] // Blue
        [InlineData(255, 255, 255)] // White
        [InlineData(0, 0, 0)] // Black
        public void Map_ReturnsAsciiArtCell_ForKnownColors(int r, int g, int b)
        {
            var blender = new TestCharacterBlender();
            var mapper = new BlendingCellMapper(blender);
            var pixel = new Pixel(r, g, b);
            var cell = mapper.Map(pixel);
            cell.Should().NotBeNull();
            cell.Character.Should().NotBe('\0');
        }

        /// <summary>
        /// Verifies that Map returns different AsciiArtCell results for different pixel colors.
        /// </summary>
        [Fact]
        public void Map_ReturnsDifferentCells_ForDifferentPixels()
        {
            var blender = new TestCharacterBlender();
            var mapper = new BlendingCellMapper(blender);
            var redCell = mapper.Map(new Pixel(255, 0, 0));
            var greenCell = mapper.Map(new Pixel(0, 255, 0));
            var blueCell = mapper.Map(new Pixel(0, 0, 255));
            redCell.Should().NotBe(greenCell);
            greenCell.Should().NotBe(blueCell);
            blueCell.Should().NotBe(redCell);
        }

        /// <summary>
        /// Verifies that Map returns a valid AsciiArtCell for a set of known colors using a custom character blender.
        /// </summary>
        /// <param name="r">The red component of the color.</param>
        /// <param name="g">The green component of the color.</param>
        /// <param name="b">The blue component of the color.</param>
        [Theory]
        [InlineData(255, 0, 0)] // Red
        [InlineData(0, 255, 0)] // Green
        [InlineData(0, 0, 255)] // Blue
        [InlineData(255, 255, 255)] // White
        [InlineData(0, 0, 0)] // Black
        public void Map_UsesCharacterBlender(int r, int g, int b)
        {
            var blender = new TestCharacterBlender();
            var mapper = new BlendingCellMapper(blender);
            var pixel = new Pixel(r, g, b);
            var cell = mapper.Map(pixel);
            cell.Character.Should().Be('X');
        }

        private class TestCharacterBlender : ICharacterBlender
        {
            public char GetCharacterForRatio(double ratio) => 'X';
        }
    }
}
