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

        // TODO: #25 Use Mock<IAutoCompleter> to remove dependency on AutoCompleter in tests

        /// <summary>
        /// Tests that if text has already been entered which does not match any
        /// of the suggestions, the tab key does not change the user input.
        /// </summary>
        [Fact]
        public void Handle_WithNonMatchingMatchTextAlreadyEntered_UserInputIsNotChanged()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            var keyInfo = CreateKey(ConsoleKey.Tab);

            // Act
            handler.Handle(keyInfo, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.SelectFirstMatchingSuggestion(), Times.Exactly(1));
            mockAutoCompleter.Verify(m => m.SelectNextSuggestion(), Times.Never);
            mockAutoCompleter.Verify(m => m.SelectPreviousSuggestion(), Times.Never);
            mockAutoCompleter.Verify(m => m.ReplaceUserInputWith(It.IsAny<string>()), Times.Never);
        }

        /// <summary>
        /// Tests that if no suggestions are available, the tab key does nothing.
        /// </summary>
        [Fact]
        public void Handle_WithNoTextEnteredAndNoSuggestions_DoesNothing()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            mockAutoCompleter.Setup(m => m.GetCurrentSuggestion()).Returns((string?)null);
            var keyInfo = CreateKey(ConsoleKey.Tab);

            // Act
            handler.Handle(keyInfo, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.SelectFirstMatchingSuggestion(), Times.Once);
            mockAutoCompleter.Verify(m => m.SelectNextSuggestion(), Times.Never);
            mockAutoCompleter.Verify(m => m.SelectPreviousSuggestion(), Times.Never);
            mockAutoCompleter.Verify(m => m.ReplaceUserInputWith(It.IsAny<string>()), Times.Never);
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
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            mockAutoCompleter.Setup(m => m.UserInput).Returns(enteredText);
            mockAutoCompleter.Setup(m => m.Suggestions).Returns(this.Suggestions);
            var keyInfo = CreateKey(ConsoleKey.Tab);

            // Act
            handler.Handle(keyInfo, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.SelectFirstMatchingSuggestion(), Times.Once);
            mockAutoCompleter.Verify(m => m.SelectNextSuggestion(), Times.Never);
            mockAutoCompleter.Verify(m => m.SelectPreviousSuggestion(), Times.Never);
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
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            mockAutoCompleter.Setup(m => m.UserInput).Returns(enteredText);
            mockAutoCompleter.Setup(m => m.Suggestions).Returns(this.Suggestions);
            var keyInfo = CreateKey(ConsoleKey.Tab, shift: true);

            // Act
            handler.Handle(keyInfo, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.SelectFirstMatchingSuggestion(), Times.Once);
            mockAutoCompleter.Verify(m => m.SelectNextSuggestion(), Times.Never);
            mockAutoCompleter.Verify(m => m.SelectPreviousSuggestion(), Times.Never);
        }

        /// <summary>
        /// Tests that pressing tab for a second time without the shift key
        /// cycles forward through the suggestions.
        /// </summary>
        [Fact]
        public void Handle_SubsequentTabWithoutShiftKey_SelectsNextSuggestion()
        {
            var handler = this.HandlerUnderTest;
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            mockAutoCompleter.Setup(m => m.Suggestions).Returns(this.Suggestions);
            mockAutoCompleter.Setup(m => m.GetCurrentSuggestion()).Returns(this.Suggestions[0]);
            var keyInfo = CreateKey(ConsoleKey.Tab);

            // Act
            handler.Handle(keyInfo, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.SelectFirstMatchingSuggestion(), Times.Never);
            mockAutoCompleter.Verify(m => m.SelectNextSuggestion(), Times.Once);
            mockAutoCompleter.Verify(m => m.SelectPreviousSuggestion(), Times.Never);
        }

        /// <summary>
        /// Tests that pressing the tab key for a second time with the shift key
        /// cycles backwards through the suggestions.
        /// </summary>
        [Fact]
        public void Handle_SubsequentTabWithShiftKey_SelectsPreviousSuggestion()
        {
            var handler = this.HandlerUnderTest;
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            mockAutoCompleter.Setup(m => m.Suggestions).Returns(this.Suggestions);
            mockAutoCompleter.Setup(m => m.GetCurrentSuggestion()).Returns(this.Suggestions[0]);
            var keyInfo = CreateKey(ConsoleKey.Tab, shift: true);

            // Act
            handler.Handle(keyInfo, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.SelectFirstMatchingSuggestion(), Times.Never);
            mockAutoCompleter.Verify(m => m.SelectNextSuggestion(), Times.Never);
            mockAutoCompleter.Verify(m => m.SelectPreviousSuggestion(), Times.Once);
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
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            mockAutoCompleter.Setup(m => m.GetCurrentSuggestion()).Returns((string?)null);
            var keyInfo = CreateKey(ConsoleKey.Tab);

            // Act
            handler.Handle(keyInfo, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.SelectFirstMatchingSuggestion(), Times.Once);
            mockAutoCompleter.Verify(m => m.SelectNextSuggestion(), Times.Never);
            mockAutoCompleter.Verify(m => m.SelectPreviousSuggestion(), Times.Never);
        }
    }
}
