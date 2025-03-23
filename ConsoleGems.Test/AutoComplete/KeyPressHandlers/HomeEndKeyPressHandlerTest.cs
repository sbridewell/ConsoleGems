// <copyright file="HomeEndKeyPressHandlerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.AutoComplete.KeyPressHandlers
{
    /// <summary>
    /// Unit tests for the <see cref="HomeEndKeyPressHandler"/> class.
    /// </summary>
    public class HomeEndKeyPressHandlerTest : KeyPressHandlerTestBase
    {
        /// <inheritdoc/>
        public override IAutoCompleteKeyPressHandler HandlerUnderTest => new HomeEndKeyPressHandler();

        /// <summary>
        /// Tests that the home key moves the cursor to the start of the user input
        /// and does not change the user input.
        /// </summary>
        [Fact]
        public void Handle_HomeKey_MovesCursorToStartOfUserInput()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            var homeKeyPress = CreateKey(ConsoleKey.Home);

            // Act
            handler.Handle(homeKeyPress, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.MoveCursorToHome(), Times.Once);
            mockAutoCompleter.Verify(m => m.MoveCursorToEnd(), Times.Never);
        }

        /// <summary>
        /// Tests that the end key moves the cursor to the end of the user
        /// input and does not change the user input.
        /// </summary>
        [Fact]
        public void Handle_EndKey_MovesCursorToEndOfUserInput()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            var endKeyPress = CreateKey(ConsoleKey.End);

            // Act
            handler.Handle(endKeyPress, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.MoveCursorToHome(), Times.Never);
            mockAutoCompleter.Verify(m => m.MoveCursorToEnd(), Times.Once);
        }
    }
}
