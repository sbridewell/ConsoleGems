// <copyright file="ExtendedCharacterBlenderTests.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Test.CharacterBlenders
{
    using FluentAssertions;
    using Sde.AsciiArt.CharacterBlenders;
    using Xunit;

    /// <summary>
    /// Contains unit tests for the <see cref="ExtendedCharacterBlender"/> class.
    /// </summary>
    public class ExtendedCharacterBlenderTests
    {
        private readonly ExtendedCharacterBlender blender = new();

        /// <summary>
        /// Tests that <see cref="ExtendedCharacterBlender.GetCharacterForRatio"/> returns the expected character
        /// for boundary, middle, and out-of-range ratio values.
        /// </summary>
        /// <param name="ratio">The foreground-to-background ratio to test.</param>
        /// <param name="expectedIndex">The expected index in the character array.</param>
        [Theory]
        [InlineData(0.0, 0)] // Lower bound
        [InlineData(1.0, 244)] // Upper bound (was 254)
        [InlineData(0.5, 122)] // Middle value (was 127)
        [InlineData(-0.5, 0)] // Below lower bound
        [InlineData(1.5, 244)] // Above upper bound (was 254)
        public void GetCharacterForRatio_BoundaryAndMiddleValues_ReturnsExpectedCharacter(double ratio, int expectedIndex)
        {
            // Act
            var result = this.blender.GetCharacterForRatio(ratio);

            // Assert
            result.Should().Be(ExtendedCharacterBlender.Characters[expectedIndex]);
        }

        /// <summary>
        /// Tests that <see cref="ExtendedCharacterBlender.GetCharacterForRatio"/> provides a consistent linear mapping
        /// from ratio values to character indices for key indices.
        /// </summary>
        /// <param name="index">
        /// The index in the character array to test. Used to calculate the ratio value so that GetCharacterForRatio
        /// should return the character at this index. This verifies that the mapping from ratio to character index
        /// is correct for representative positions in the array.
        /// </param>
        [Theory]
        [InlineData(0)] // First index
        [InlineData(1)] // Second index
        [InlineData(10)] // Early index
        [InlineData(122)] // Middle index
        [InlineData(244)] // Last index
        public void GetCharacterForRatio_LinearMapping_IsConsistent(int index)
        {
            int length = ExtendedCharacterBlender.Characters.Length;
            double ratio = (double)index / (length - 1);
            var character = this.blender.GetCharacterForRatio(ratio);
            character.Should().Be(ExtendedCharacterBlender.Characters[index]);
        }

        /// <summary>
        /// Tests that the <see cref="ExtendedCharacterBlender.Characters"/> array contains the expected number of characters.
        /// </summary>
        [Fact]
        public void Characters_Array_HasExpectedLength()
        {
            ExtendedCharacterBlender.Characters.Length.Should().Be(245); // was 255
        }
    }
}
