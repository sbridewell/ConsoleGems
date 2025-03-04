// <copyright file="LiteralKeyPressHandlerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.AutoComplete.KeyPressHandlers
{
    /// <summary>
    /// Unit tests for the <see cref="LiteralKeyPressHandler"/> class.
    /// </summary>
    public class LiteralKeyPressHandlerTest : KeyPressHandlerTestBase
    {
        /// <inheritdoc/>
        public override IAutoCompleteKeyPressHandler HandlerUnderTest => new LiteralKeyPressHandler();

        /// <summary>
        /// Tests that when a literal character is entered and the cursor is at the end
        /// of the user input, the correct character is added to the end of the user input
        /// and the cursor is positioned after it.
        /// </summary>
        [Fact]
        public void Handle_LiteralCharacterWhenCursorIsAtEndOfUserInput_InsertsCharacterAtCursorPositionAndPositionsCursorAfterId()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var enteredText = "123456";
            var autoCompleter = this.CreateAutoCompleter(enteredText);
            var initialCursorPosition = autoCompleter.CursorPositionWithinUserInput;
            var keyPress = CreateKey('a');

            // Act
            handler.Handle(keyPress, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be("123456a");
            autoCompleter.CursorPositionWithinUserInput.Should().Be(initialCursorPosition + 1);
        }

        /// <summary>
        /// Tests that when a literal character is entered into the middle of the user
        /// input, the correct character is added to the user input and the cursor is
        /// positioned after it.
        /// </summary>
        [Fact]
        public void Handle_LiteralCharacterWhenCursorIsInMiddleOfInput_InsertsCharacterAtCursorPositionAndPositionsCursorAfterIt()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var enteredText = "123456";
            var autoCompleter = this.CreateAutoCompleter(enteredText);
            autoCompleter.MoveCursorToHome();
            autoCompleter.MoveCursorRight(3);
            var keyPress = CreateKey('a');

            // Act
            handler.Handle(keyPress, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be("123a456");
            autoCompleter.CursorPositionWithinUserInput.Should().Be(4);
        }

        /// <summary>
        /// Tests that entering a literal character resets the suggestion
        /// index to -1.
        /// </summary>
        [Fact]
        public void Handle_SetsCurrentSuggestionIndexToMinusOne()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var enteredText = "z";
            var autoCompleter = this.CreateAutoCompleter(enteredText, true);
            autoCompleter.SelectNextSuggestion();
            var keyPress = CreateKey('b');

            // Act
            handler.Handle(keyPress, autoCompleter);

            // Assert
            autoCompleter.GetCurrentSuggestion().Should().BeNull();
        }
    }
}
