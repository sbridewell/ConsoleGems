// <copyright file="LaunchSnakeGameCommandTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace SnakeGame.Test
{
    using Moq;
    using Sde.SnakeGame;

    /// <summary>
    /// Unit tests for the <see cref="LaunchSnakeGameCommand"/> class.
    /// </summary>
    public class LaunchSnakeGameCommandTest
    {
        /// <summary>
        /// Test case for the Execute method.
        /// </summary>
        [Fact]
        public void Execute_CallsThePlayMethod()
        {
            // Arrange
            var mockGameController = new Mock<ISnakeGameController>();
            var command = new LaunchSnakeGameCommand(mockGameController.Object);

            // Act
            command.Execute();

            // Assert
            mockGameController.Verify(m => m.Play(), Times.Once);
        }
    }
}
