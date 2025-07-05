// <copyright file="QuitKeyPressHandlerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test.KeyPressHandlers
{
    using Moq;
    using Sde.MazeGame.KeyPressHandlers;

    /// <summary>
    /// Unit tests for the <see cref="QuitKeyPressHandler"/> class.
    /// </summary>
    public class QuitKeyPressHandlerTest
    {
        /// <summary>
        /// Tests that the Handle method calls Quit on the game controller.
        /// </summary>
        [Fact]
        public void Handle_ShouldCallQuitGameOnController()
        {
            // Arrange
            var mockController = new Mock<IMazeGameController>();
            var handler = new QuitKeyPressHandler();
            var keyInfo = new ConsoleKeyInfo('q', ConsoleKey.Q, false, false, false);

            // Act
            handler.Handle(keyInfo, mockController.Object);

            // Assert
            mockController.Verify(m => m.Quit(), Times.Once);
            mockController.VerifyNoOtherCalls();
        }
    }
}
