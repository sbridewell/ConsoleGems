// <copyright file="MazeGameKeyPressMappingsTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test.KeyPressHandlers
{
    using FluentAssertions;
    using Sde.MazeGame.KeyPressHandlers;

    /// <summary>
    /// Unit tests for the <see cref="MazeGameKeyPressMappings"/> class.
    /// </summary>
    public class MazeGameKeyPressMappingsTest
    {
        /// <summary>
        /// Tests that the Mappings dictionary contains the correct key press handlers for each key.
        /// </summary>
        [Fact]
        public void Mappings_ShouldHaveCorrectKeyPressHandlers()
        {
            // Arrange
            var mappings = new MazeGameKeyPressMappings();

            // Act & Assert
            mappings.Mappings[ConsoleKey.LeftArrow].Should().BeOfType<TurnLeftKeyPressHandler>();
            mappings.Mappings[ConsoleKey.RightArrow].Should().BeOfType<TurnRightKeyPressHandler>();
            mappings.Mappings[ConsoleKey.UpArrow].Should().BeOfType<MoveForwardKeyPressHandler>();
            mappings.Mappings[ConsoleKey.Q].Should().BeOfType<QuitKeyPressHandler>();
        }
    }
}
