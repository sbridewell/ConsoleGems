// <copyright file="LaunchMazeGameCommandTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test.Commands
{
    using Moq;
    using Sde.ConsoleGems.AutoComplete;
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.Commands;

    /// <summary>
    /// Unit tests for the <see cref="LaunchMazeGameCommand"/> class.
    /// </summary>
    public class LaunchMazeGameCommandTest
    {
        /// <summary>
        /// Tests the Execute method.
        /// </summary>
        [Fact]
        public void ExecuteTest()
        {
            // Arrange
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            mockAutoCompleter.Setup(ac => ac.ReadLine(It.IsAny<List<string>>(), It.IsAny<string>()))
                .Returns("foo.maze.txt");
            var mockGameController = new Mock<IGameController>();
            var command = new LaunchMazeGameCommand(mockAutoCompleter.Object, mockGameController.Object);

            // Act
            command.Execute();

            // Assert
            mockAutoCompleter.Verify(
                ac => ac.ReadLine(It.IsAny<List<string>>(), "Tab through the available maze files: "),
                Times.Once);
            mockGameController.Verify(
                gc => gc.Initialise(
                    It.Is<MazeGameOptions>(
                        o => o.MapViewOrigin == new ConsolePoint(41, 3)
                        && o.PovViewOrigin == new ConsolePoint(0, 3)
                        && o.StatusOrigin == new ConsolePoint(0, 0)
                        && o.MazeDataFile == "foo.maze.txt")),
                Times.Once);
            mockGameController.Verify(gc => gc.Play(), Times.Once);
        }
    }
}
