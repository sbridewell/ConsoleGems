// <copyright file="ArrowKeyPressHandlerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.AutoComplete.KeyPressHandlers
{
    /// <summary>
    /// Unit tests for the <see cref="ArrowKeyPressHandler"/> class.
    /// </summary>
    public class ArrowKeyPressHandlerTest : KeyPressHandlerTestBase
    {
        /// <inheritdoc/>
        public override IAutoCompleteKeyPressHandler HandlerUnderTest => new ArrowKeyPressHandler();

        /// <summary>
        /// Happy path test for handling the left arrow key.
        /// </summary>
        [Fact]
        public void Handle_LeftArrow_MovesCursorLeftByOneAndDoesNotChangeUserInput()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var enteredText = "123456";
            var autoCompleter = this.CreateAutoCompleter(enteredText);
            var initialCursorPosition = autoCompleter.CursorPositionWithinUserInput;
            var leftArrowKeyPress = CreateKey(ConsoleKey.LeftArrow);

            // Act
            handler.Handle(leftArrowKeyPress, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be(enteredText);
            autoCompleter.CursorPositionWithinUserInput.Should().Be(initialCursorPosition - 1);
        }

        /// <summary>
        /// Tests that the cursor position is not changed when the cursor is at the start of the user input
        /// and the left arrow key is pressed.
        /// </summary>
        [Fact]
        public void Handle_LeftArrowWhenCursorIsAtStartOfUserInput_DoesNotChangeCursorPositionOrUserInput()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var enteredText = "123456";
            var autoCompleter = this.CreateAutoCompleter(enteredText);
            autoCompleter.MoveCursorToHome();
            var leftArrowKeyPress = CreateKey(ConsoleKey.LeftArrow);

            // Act
            handler.Handle(leftArrowKeyPress, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be(enteredText);
            autoCompleter.CursorPositionWithinUserInput.Should().Be(0);
        }

        /// <summary>
        /// Happy path test for handling the right arrow key.
        /// </summary>
        [Fact]
        public void Handle_RightArrow_MovesCursorRightByOneAndDoesNotChangeUserInput()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var enteredText = "123456";
            var autoCompleter = this.CreateAutoCompleter(enteredText);
            var initialCursorPosition = 1;
            autoCompleter.MoveCursorToHome();
            autoCompleter.MoveCursorRight(initialCursorPosition);
            var rightArrowKeyPress = CreateKey(ConsoleKey.RightArrow);

            // Act
            handler.Handle(rightArrowKeyPress, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be(enteredText);
            autoCompleter.CursorPositionWithinUserInput.Should().Be(initialCursorPosition + 1);
        }

        /// <summary>
        /// Tests that the cursor position is not changed when the cursor is at the end of the user input
        /// and the right arrow key is pressed.
        /// </summary>
        [Fact]
        public void Handle_RightArrowWhenCursorIsAtEndOfUserInput_DoesNotChangeCursorPositionOrUserInput()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var enteredText = "123456";
            var autoCompleter = this.CreateAutoCompleter(enteredText);
            var rightArrowKeyPress = CreateKey(ConsoleKey.RightArrow);

            // Act
            handler.Handle(rightArrowKeyPress, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be(enteredText);
            autoCompleter.CursorPositionWithinUserInput.Should().Be(enteredText.Length);
        }
    }
}
