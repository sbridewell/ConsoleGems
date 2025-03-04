// <copyright file="TabKeyPressHandlerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.AutoComplete.KeyPressHandlers
{
    /// <summary>
    /// Unit tests for the <see cref="TabKeyPressHandler"/> class.
    /// </summary>
    public class TabKeyPressHandlerTest : KeyPressHandlerTestBase
    {
        /// <inheritdoc/>
        public override IAutoCompleteKeyPressHandler HandlerUnderTest => new TabKeyPressHandler();

        /// <summary>
        /// Tests that if text has already been entered which does not match any
        /// of the suggestions, the tab key does nothing.
        /// </summary>
        [Fact]
        public void Handle_WithNonMatchingMatchTextAlreadyEntered_DoesNothing()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var enteredText = "123456";
            var autoCompleter = this.CreateAutoCompleter(enteredText, true);
            var keyInfo = CreateKey(ConsoleKey.Tab);

            // Act
            handler.Handle(keyInfo, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be(enteredText);
        }

        /// <summary>
        /// Tests that if no suggestions are available, the tab key does nothing.
        /// </summary>
        [Fact]
        public void Handle_WithNoTextEnteredAndNoSuggestions_DoesNothing()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var enteredText = string.Empty;
            var autoCompleter = this.CreateAutoCompleter(enteredText, false);
            var keyInfo = CreateKey(ConsoleKey.Tab);

            // Act
            handler.Handle(keyInfo, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be(enteredText);
        }

        /// <summary>
        /// Tests that pressing tab for the first time when no text has been entered
        /// causes the first suggestion to be displayed.
        /// </summary>
        [Fact]
        public void Handle_FirstTabWithNoExistingInputAndWithoutShiftKey_DisplaysFirstSuggestion()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var enteredText = string.Empty;
            var autoCompleter = this.CreateAutoCompleter(enteredText, true);
            var keyInfo = CreateKey(ConsoleKey.Tab);
            var expectedSuggestion = this.Suggestions[0];

            // Act
            handler.Handle(keyInfo, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be(this.Suggestions[0]);
            autoCompleter.CursorPositionWithinUserInput.Should().Be(expectedSuggestion.Length);
        }

        /// <summary>
        /// Test that pressing tab for the first time with the shift key, when no text
        /// has been entered, causes the first suggestion to be displayed.
        /// </summary>
        [Fact]
        public void Handle_FirstTabWithNoExistingInputAndWithShiftKey_DisplaysFirstSuggestion()
        {
            var handler = this.HandlerUnderTest;
            var enteredText = string.Empty;
            var autoCompleter = this.CreateAutoCompleter(enteredText, true);
            var keyInfo = CreateKey(ConsoleKey.Tab, shift: true);
            var expectedSuggestion = this.Suggestions[0];

            // Act
            handler.Handle(keyInfo, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be(expectedSuggestion);
            autoCompleter.CursorPositionWithinUserInput.Should().Be(expectedSuggestion.Length);
        }

        /// <summary>
        /// Tests that pressing tab for a second time without the shift key
        /// cycles forward through the suggestions.
        /// </summary>
        [Fact]
        public void Handle_SubsequentTabWithoutShiftKey_SelectsNextSuggestion()
        {
            var handler = this.HandlerUnderTest;
            var enteredText = string.Empty;
            var autoCompleter = this.CreateAutoCompleter(enteredText, true);
            var keyInfo = CreateKey(ConsoleKey.Tab);
            var expectedSuggestion = this.Suggestions[1];
            handler.Handle(keyInfo, autoCompleter);

            // Act
            handler.Handle(keyInfo, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be(expectedSuggestion);
            autoCompleter.CursorPositionWithinUserInput.Should().Be(expectedSuggestion.Length);
        }

        /// <summary>
        /// Tests that pressing the tab key for a second time with the shift key
        /// cycles backwards through the suggestions.
        /// </summary>
        [Fact]
        [SuppressMessage(
            "Minor Code Smell",
            "S6608:Prefer indexing instead of \"Enumerable\" methods on types implementing \"IList\"",
            Justification = "Easier to read and performance is not a concern here")]
        public void Handle_SubsequentTabWithShiftKey_SelectsPreviousSuggestion()
        {
            var handler = this.HandlerUnderTest;
            var enteredText = string.Empty;
            var autoCompleter = this.CreateAutoCompleter(enteredText, true);
            var keyInfo1 = CreateKey(ConsoleKey.Tab);
            var keyInfo2 = CreateKey(ConsoleKey.Tab, shift: true);
            var expectedSuggestion = this.Suggestions.Last();
            handler.Handle(keyInfo1, autoCompleter);

            // Act
            handler.Handle(keyInfo2, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be(expectedSuggestion);
            autoCompleter.CursorPositionWithinUserInput.Should().Be(expectedSuggestion.Length);
        }

        /// <summary>
        /// Tests that pressing the tab key when the user input matches the start of a
        /// suggestion causes that suggestion to be selected.
        /// </summary>
        [Fact]
        public void Handle_SuggestionStartsWithUserInput_MatchingSuggestionIsSelected()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var enteredText = this.Suggestions[1].Substring(0, 2);
            var autoCompleter = this.CreateAutoCompleter(enteredText, true);
            var keyInfo = CreateKey(ConsoleKey.Tab);
            var expectedSelection = this.Suggestions[1];

            // Act
            handler.Handle(keyInfo, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be(expectedSelection);
            autoCompleter.CursorPositionWithinUserInput.Should().Be(expectedSelection.Length);
        }

        /// <summary>
        /// Tests that nothing happens when the tab key is pressed when the
        /// entered text already exactly matches a suggestion.
        /// </summary>
        [Fact]
        public void Handle_UserInputMatchesSuggestionExactly_DoesNothing()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var enteredText = this.Suggestions[1];
            var autoCompleter = this.CreateAutoCompleter(enteredText, true);
            var keyInfo = CreateKey(ConsoleKey.Tab);

            // Act
            handler.Handle(keyInfo, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be(enteredText);
        }
    }
}
