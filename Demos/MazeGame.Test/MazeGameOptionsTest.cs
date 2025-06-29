// <copyright file="MazeGameOptionsTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test
{
    using FluentAssertions;
    using Sde.ConsoleGems.Text;

    /// <summary>
    /// Unit tests for the <see cref="MazeGameOptions"/> class.
    /// </summary>
    public class MazeGameOptionsTest
    {
        /// <summary>
        /// Tests that the WithMazeOrigin method sets the MazeOrigin property correctly.
        /// </summary>
        /// <param name="x">The horizontal co-ordinate to set.</param>
        /// <param name="y">The vertical co-ordinate to set.</param>
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(5, 4)]
        public void WithMazeOrigin_SetsMazeOrigin(int x, int y)
        {
            // Arrange
            var options = new MazeGameOptions();

            // Act
            options.WithMazeOrigin(x, y);

            // Assert
            options.MazeOrigin.Should().Be(new ConsolePoint(x, y));
        }

        /// <summary>
        /// Tests that the WithStatusOrigin method sets the StatusOrigin property correctly.
        /// </summary>
        /// <param name="x">The horizontal co-ordinate to set.</param>
        /// <param name="y">The vertical co-ordinate to set.</param>
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(5, 4)]
        public void WithStatusOrigin_SetsStatusOrigin(int x, int y)
        {
            // Arrange
            var options = new MazeGameOptions();

            // Act
            options.WithStatusOrigin(x, y);

            // Assert
            options.StatusOrigin.Should().Be(new ConsolePoint(x, y));
        }
    }
}
