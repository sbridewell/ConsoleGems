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
            var enteredText = "123456";
            var autoCompleter = this.CreateAutoCompleter(enteredText);
            var initialCursorPosition = autoCompleter.CursorPositionWithinUserInput;
            var backspaceKeyPress = CreateKey(ConsoleKey.Backspace);

            // Act
            handler.Handle(backspaceKeyPress, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be("12345");
            autoCompleter.CursorPositionWithinUserInput.Should().Be(initialCursorPosition - 1);
        }

        /// <summary>
        /// Tests that when the cursor is in the middle of the user input and the backspace key is pressed,
        /// the correct character is deleted and the cursor is moved left by one character.
        /// </summary>
        [Fact]
        public void Handle_BackspaceWhenCursorIsInMiddleOfUserInput_RemovesCorrectCharacterAndMovesCursorLeftByOne()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var enteredText = "123456";
            var autoCompleter = this.CreateAutoCompleter(enteredText);
            autoCompleter.MoveCursorToHome();
            autoCompleter.MoveCursorRight(3);
            var backspaceKeyPress = CreateKey(ConsoleKey.Backspace);

            // Act
            handler.Handle(backspaceKeyPress, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be("12456");
            autoCompleter.CursorPositionWithinUserInput.Should().Be(2);
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
            var enteredText = "123456";
            var autoCompleter = this.CreateAutoCompleter(enteredText);
            autoCompleter.MoveCursorToHome();
            var backspaceKeyPress = CreateKey(ConsoleKey.Backspace);

            // Act
            handler.Handle(backspaceKeyPress, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be(enteredText);
            autoCompleter.CursorPositionWithinUserInput.Should().Be(0);
        }
    }
}
