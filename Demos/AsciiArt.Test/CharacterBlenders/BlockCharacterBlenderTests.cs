// <copyright file="BlockCharacterBlenderTests.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Test.CharacterBlenders
{
    using FluentAssertions;
    using Sde.AsciiArt.CharacterBlenders;
    using Xunit;

    /// <summary>
    /// Contains unit tests for the <see cref="BlockCharacterBlender"/> class.
    /// </summary>
    public class BlockCharacterBlenderTests
    {
        private readonly BlockCharacterBlender blender = new();

        /// <summary>
        /// Tests that <see cref="BlockCharacterBlender.GetCharacterForRatio"/> returns the expected block character for boundary and out-of-range ratio values.
        /// </summary>
        /// <param name="ratio">The foreground-to-background ratio to test.</param>
        /// <param name="expected">The expected character for the given ratio.</param>
        [Theory]
        [InlineData(-0.1, ' ')] // Below lower bound
        [InlineData(0.0, ' ')] // Lower bound
        [InlineData(0.1, '░')] // Between 0 and 0.25
        [InlineData(0.25, '▒')] // Lower mid
        [InlineData(0.3, '▒')] // Between 0.25 and 0.5
        [InlineData(0.5, '▓')] // Middle
        [InlineData(0.6, '▓')] // Between 0.5 and 0.75
        [InlineData(0.75, '█')] // Upper mid
        [InlineData(0.9, '█')] // Between 0.75 and 1.0
        [InlineData(1.0, '█')] // Upper bound
        [InlineData(1.1, '█')] // Above upper bound
        public void GetCharacterForRatio_BoundaryAndRangeValues_ReturnsExpectedCharacter(double ratio, char expected)
        {
            // Act
            var result = this.blender.GetCharacterForRatio(ratio);

            // Assert
            result.Should().Be(expected);
        }
    }
}
