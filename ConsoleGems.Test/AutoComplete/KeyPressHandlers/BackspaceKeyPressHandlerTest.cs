// <copyright file="BackspaceKeyPressHandlerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.AutoComplete.KeyPressHandlers
{
    /// <summary>
    /// Unit tests for the <see cref="BackspaceKeyPressHandler"/> class.
    /// </summary>
    public class BackspaceKeyPressHandlerTest : KeyPressHandlerTestBase
    {
        /// <inheritdoc/>
        public override IAutoCompleteKeyPressHandler HandlerUnderTest => new BackspaceKeyPressHandler();

        /// <summary>
        /// Happy path test for handling the backspace key.
        /// </summary>
        [Fact]
        public void Handle_BackspaceWhenCursorIsAtEndOfUserInput_RemovesLastCharacterAndMovesCursorLeftByOne()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            mockAutoCompleter.Setup(m => m.CursorIsAtHome).Returns(false);
            var backspaceKeyPress = CreateKey(ConsoleKey.Backspace);

            // Act
            handler.Handle(backspaceKeyPress, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.RemovePreviousCharacterFromUserInput(), Times.Once);
            mockAutoCompleter.Verify(m => m.SelectNoSuggestion(), Times.Once);
        }

        /// <summary>
        /// Tests that when the cursor is at the start of the user input and the
        /// backspace key is pressed, the user input and cursor position are not changed.
        /// </summary>
        [Fact]
        public void Handle_BackspaceWhenCursorIsAtStartOfUserInput_DoesNotChangeCursorPositionOrUserInput()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            mockAutoCompleter.Setup(m => m.CursorIsAtHome).Returns(true);
            var backspaceKeyPress = CreateKey(ConsoleKey.Backspace);

            // Act
            handler.Handle(backspaceKeyPress, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.RemovePreviousCharacterFromUserInput(), Times.Never);
            mockAutoCompleter.Verify(m => m.SelectNoSuggestion(), Times.Never);
        }
    }
}
