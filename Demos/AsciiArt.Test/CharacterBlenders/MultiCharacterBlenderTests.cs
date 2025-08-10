// <copyright file="MultiCharacterBlenderTests.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Test.CharacterBlenders
{
    using System.Linq;
    using FluentAssertions;
    using Sde.AsciiArt.CharacterBlenders;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="MultiCharacterBlender"/> class.
    /// </summary>
    public class MultiCharacterBlenderTests
    {
        /// <summary>
        /// Verifies that GetCharacterForRatio returns the first character for ratio 0.0 and the last for 1.0.
        /// </summary>
        /// <param name="ratio">The ratio of foreground to background.</param>
        /// <param name="expected">The expected character for the ratio.</param>
        [Theory]
        [InlineData(0.0, ' ')]
        [InlineData(1.0, '█')]
        public void GetCharacterForRatio_HandlesEdgeCases(double ratio, char expected)
        {
            var blender = new MultiCharacterBlender();
            blender.GetCharacterForRatio(ratio).Should().Be(expected);
        }

        /// <summary>
        /// Verifies that GetCharacterForRatio returns a character from the set for intermediate ratios.
        /// </summary>
        /// <param name="ratio">The ratio of foreground to background.</param>
        [Theory]
        [InlineData(0.05)]
        [InlineData(0.25)]
        [InlineData(0.5)]
        [InlineData(0.75)]
        [InlineData(0.95)]
        public void GetCharacterForRatio_ReturnsValidCharacter(double ratio)
        {
            var blender = new MultiCharacterBlender();
            var ch = blender.GetCharacterForRatio(ratio);
            MultiCharacterBlender.Characters.Should().Contain(ch);
        }

        /// <summary>
        /// Verifies that GetCharacterForRatio returns a smooth gradient of characters for increasing ratios.
        /// </summary>
        [Fact]
        public void GetCharacterForRatio_ProducesGradient()
        {
            var blender = new MultiCharacterBlender();
            char prev = blender.GetCharacterForRatio(0.0);
            for (int i = 1; i <= 20; i++)
            {
                double ratio = i / 20.0;
                char ch = blender.GetCharacterForRatio(ratio);
                MultiCharacterBlender.Characters.ToList().IndexOf(ch).Should().BeGreaterOrEqualTo(MultiCharacterBlender.Characters.ToList().IndexOf(prev));
                prev = ch;
            }
        }
    }
}
