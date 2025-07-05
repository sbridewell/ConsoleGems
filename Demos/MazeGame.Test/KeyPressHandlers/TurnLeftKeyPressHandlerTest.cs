// <copyright file="TurnLeftKeyPressHandlerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test.KeyPressHandlers
{
    using Moq;
    using Sde.MazeGame.KeyPressHandlers;

    /// <summary>
    /// Unit tests for the <see cref="TurnLeftKeyPressHandler"/> class.
    /// </summary>
    public class TurnLeftKeyPressHandlerTest
    {
        /// <summary>
        /// Tests that the Handle method calls TurnPlayerLeft on the game controller.
        /// </summary>
        [Fact]
        public void Handle_ShouldCallTurnPlayerLeft()
        {
            // Arrange
            var mockController = new Mock<IMazeGameController>();
            var handler = new TurnLeftKeyPressHandler();
            var keyInfo = new ConsoleKeyInfo('a', ConsoleKey.A, false, false, false);

            // Act
            handler.Handle(keyInfo, mockController.Object);

            // Assert
            mockController.Verify(m => m.TurnPlayerLeft(), Times.Once);
            mockController.VerifyNoOtherCalls();
        }
    }
}
