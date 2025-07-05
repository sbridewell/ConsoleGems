// <copyright file="ArrowPlayerCharacterProviderTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test.CharacterProviders
{
    using FluentAssertions;
    using Sde.MazeGame.CharacterProviders;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Unit tests for the <see cref="ArrowPlayerCharacterProvider"/> class.
    /// </summary>
    public class ArrowPlayerCharacterProviderTest
    {
        /// <summary>
        /// Test that the GetPlayerChar method returns the correct character.
        /// </summary>
        /// <param name="direction">The direction that the player is facing.</param>
        /// <param name="expectedChar">The expected character to represent the player.</param>
        [Theory]
        [InlineData(Direction.North, '↑')]
        [InlineData(Direction.East, '→')]
        [InlineData(Direction.South, '↓')]
        [InlineData(Direction.West, '←')]
        [InlineData((Direction)999, ' ')] // Test for an invalid direction
        public void GetPlayerChar_ReturnsCorrectCharacter(Direction direction, char expectedChar)
        {
            // Arrange
            var provider = new ArrowPlayerCharacterProvider();
            var player = new Player { FacingDirection = direction };

            // Act
            var actualChar = provider.GetPlayerChar(player);

            // Assert
            actualChar.Should().Be(expectedChar);
        }
    }
}
