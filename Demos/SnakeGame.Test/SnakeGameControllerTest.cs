// <copyright file="SnakeGameControllerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace SnakeGame.Test
{
    using Moq;
    using Sde.ConsoleGems.Consoles;
    using Sde.SnakeGame;

    /// <summary>
    /// Unit tests for the <see cref="SnakeGameController"/> class.
    /// </summary>
    public class SnakeGameControllerTest
    {
        private readonly Mock<IConsole> mockConsole = new ();

        /// <summary>
        /// Placeholder test for the Play method.
        /// </summary>
        [Fact]
        public void Play_WriteSomeTests()
        {
            // Arrange
            var controller = new SnakeGameController(this.mockConsole.Object);

            // Act
            controller.Play();

            // Assert
            Assert.Fail("Write some tests for the SnakeGameController.Play method.");
        }
    }
}