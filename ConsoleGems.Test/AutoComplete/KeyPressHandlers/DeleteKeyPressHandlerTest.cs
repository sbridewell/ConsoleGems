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
            var enteredText = "123456";
            var autoCompleter = this.CreateAutoCompleter(enteredText);

            var deleteKeyPress = CreateKey(ConsoleKey.Delete);

            // Act
            handler.Handle(deleteKeyPress, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be(enteredText);
            autoCompleter.CursorPositionWithinUserInput.Should().Be(enteredText.Length);
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
            var enteredText = "123456";
            var autoCompleter = this.CreateAutoCompleter(enteredText);
            autoCompleter.MoveCursorToHome();
            autoCompleter.MoveCursorRight(3);
            var deleteKeyPress = CreateKey(ConsoleKey.Delete);

            // Act
            handler.Handle(deleteKeyPress, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be("12356");
            autoCompleter.CursorPositionWithinUserInput.Should().Be(3);
        }
    }
}
