// <copyright file="MazeVisibilityUpdaterTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test.Painters.Map
{
    using FluentAssertions;
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.Models;
    using Sde.MazeGame.Painters.Map;

    /// <summary>
    /// Unit tests for the <see cref="MazeVisibilityUpdater"/> class.
    /// </summary>
    public class MazeVisibilityUpdaterTest
    {
        /// <summary>
        /// Tests that the UpdateVisibility method correctly updates the visibility of points
        /// in the maze.
        /// </summary>
        [Fact]
        public void UpdateVisibility_UpdatesExploredPoints_WhenPlayerCanSeeThem()
        {
            // Arrange
            var factory = new MazeFactory();
            var maze = factory.CreateFromFile(Path.Combine("MazeData", "10x10.maze.txt"));
            var player = new Player { Position = new ConsolePoint(2, 2) };
            var limitOfViewProvider = new LimitOfViewProvider(1);
            var visibilityUpdater = new MazeVisibilityUpdater(limitOfViewProvider);

            // Act
            var repaintRequired = visibilityUpdater.UpdateVisibility(maze, player);

            // Assert
            repaintRequired.Should().BeTrue();
            maze.GetMazePoint(new ConsolePoint(2, 1)).Explored.Should().BeTrue();
            maze.GetMazePoint(new ConsolePoint(2, 3)).Explored.Should().BeTrue();
            maze.GetMazePoint(new ConsolePoint(1, 2)).Explored.Should().BeTrue();
            maze.GetMazePoint(new ConsolePoint(3, 2)).Explored.Should().BeTrue();
        }
    }
}
