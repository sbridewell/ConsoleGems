// <copyright file="DeleteKeyPressHandlerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.AutoComplete.KeyPressHandlers
{
    /// <summary>
    /// Unit tests for <see cref="DeleteKeyPressHandler"/> class.
    /// </summary>
    public class DeleteKeyPressHandlerTest : KeyPressHandlerTestBase
    {
        /// <inheritdoc/>
        public override IAutoCompleteKeyPressHandler HandlerUnderTest => new DeleteKeyPressHandler();

        /// <summary>
        /// Tests that when the cursor is at the end of the user input and the delete key is pressed,
        /// the cursor does not move and the user input does not change.
        /// </summary>
        [Fact]
        public void Handle_DeleteWhenCursorIsAtEndOfUserInput_DoesNotChangeCursorPositionOrUserInput()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            mockAutoCompleter.SetupGet(m => m.CursorIsAtEnd).Returns(true);
            var deleteKeyPress = CreateKey(ConsoleKey.Delete);

            // Act
            handler.Handle(deleteKeyPress, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.RemoveCurrentCharacterFromUserInput(), Times.Never);
            mockAutoCompleter.Verify(m => m.SelectNoSuggestion(), Times.Never);
        }

        /// <summary>
        /// Tests that when the cursor is in the middle of the user input and the delete key
        /// is pressed, that the correct character is deleted and the cursor position does not
        /// change.
        /// </summary>
        [Fact]
        public void Handle_DeleteWhenCursorIsInMiddleOfUserInput_RemovesCorrectCharacterAndDoesNotChangeCursorPosition()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            var deleteKeyPress = CreateKey(ConsoleKey.Delete);

            // Act
            handler.Handle(deleteKeyPress, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.RemoveCurrentCharacterFromUserInput(), Times.Once);
            mockAutoCompleter.Verify(m => m.SelectNoSuggestion(), Times.Once);
        }
    }
}
