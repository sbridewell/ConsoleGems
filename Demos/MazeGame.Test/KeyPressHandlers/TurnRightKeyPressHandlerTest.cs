// <copyright file="TurnRightKeyPressHandlerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test.KeyPressHandlers
{
    using Moq;
    using Sde.MazeGame.KeyPressHandlers;

    /// <summary>
    /// Unit tests for the <see cref="TurnRightKeyPressHandler"/> class.
    /// </summary>
    public class TurnRightKeyPressHandlerTest
    {
        /// <summary>
        /// Tests that the Handle method calls TurnPlayerRight on the game controller.
        /// </summary>
        [Fact]
        public void Handle_ShouldCallTurnPlayerRight()
        {
            // Arrange
            var mockController = new Mock<IGameController>();
            var handler = new TurnRightKeyPressHandler();
            var keyInfo = new ConsoleKeyInfo('d', ConsoleKey.D, false, false, false);

            // Act
            handler.Handle(keyInfo, mockController.Object);

            // Assert
            mockController.Verify(m => m.TurnPlayerRight(), Times.Once);
            mockController.VerifyNoOtherCalls();
        }
    }
}
