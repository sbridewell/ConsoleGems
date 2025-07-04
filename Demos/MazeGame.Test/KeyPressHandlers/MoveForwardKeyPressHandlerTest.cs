// <copyright file="MoveForwardKeyPressHandlerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test.KeyPressHandlers
{
    using Moq;
    using Sde.MazeGame.KeyPressHandlers;

    /// <summary>
    /// Unit tests for the <see cref="MoveForwardKeyPressHandler"/> class.
    /// </summary>
    public class MoveForwardKeyPressHandlerTest
    {
        /// <summary>
        /// Tests that the Handle method calls TryToMovePlayerForward on the game controller.
        /// </summary>
        [Fact]
        public void Handle_ShouldCallTryToMovePlayerForward()
        {
            // Arrange
            var mockController = new Mock<IMazeGameController>();
            var handler = new MoveForwardKeyPressHandler();
            var keyInfo = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

            // Act
            handler.Handle(keyInfo, mockController.Object);

            // Assert
            mockController.Verify(m => m.TryToMovePlayerForward(), Times.Once);
            mockController.VerifyNoOtherCalls();
        }
    }
}
