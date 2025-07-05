// <copyright file="LimitOfViewProviderTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test.Painters.Map
{
    using FluentAssertions;
    using Sde.MazeGame.Models;
    using Sde.MazeGame.Painters.Map;

    /// <summary>
    /// Unit tests for the <see cref="LimitOfViewProvider"/> class.
    /// </summary>
    public class LimitOfViewProviderTest
    {
        /// <summary>
        /// Tests that the constructor initializes the provider correctly.
        /// </summary>
        [Fact]
        public void Constructor_ValidVisibleDistance_CreatesCorrectLimitOfView()
        {
            // Arrange
            var visibleDistance = 3;
            var expected = new List<ConsolePointOffset>
            {
                new ConsolePointOffset(-3, 0),
                new ConsolePointOffset(-2, -2),
                new ConsolePointOffset(-2, -1),
                new ConsolePointOffset(-2, 0),
                new ConsolePointOffset(-2, 1),
                new ConsolePointOffset(-2, 2),
                new ConsolePointOffset(-1, -2),
                new ConsolePointOffset(-1, -1),
                new ConsolePointOffset(-1, 0),
                new ConsolePointOffset(-1, 1),
                new ConsolePointOffset(-1, 2),
                new ConsolePointOffset(0, -3),
                new ConsolePointOffset(0, -2),
                new ConsolePointOffset(0, -1),
                new ConsolePointOffset(0, 0),
                new ConsolePointOffset(0, 1),
                new ConsolePointOffset(0, 2),
                new ConsolePointOffset(0, 3),
                new ConsolePointOffset(1, -2),
                new ConsolePointOffset(1, -1),
                new ConsolePointOffset(1, 0),
                new ConsolePointOffset(1, 1),
                new ConsolePointOffset(1, 2),
                new ConsolePointOffset(2, -2),
                new ConsolePointOffset(2, -1),
                new ConsolePointOffset(2, 0),
                new ConsolePointOffset(2, 1),
                new ConsolePointOffset(2, 2),
                new ConsolePointOffset(3, 0),
            };

            // Act
            var limitOfViewProvider = new LimitOfViewProvider(visibleDistance);

            // Assert
            limitOfViewProvider.VisibleDistance.Should().Be(visibleDistance);
            limitOfViewProvider.LimitOfView.Should().BeEquivalentTo(expected);
        }
    }
}
