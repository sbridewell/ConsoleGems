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
        /// <summary>
        /// Gets test data consisting of keys which do not represent
        /// printable characters.
        /// </summary>
        public static TheoryData<ConsoleKeyInfo> NonPrintableKeys =>
        [
            new ConsoleKeyInfo('\0', ConsoleKey.LeftArrow, false, false, false),
            new ConsoleKeyInfo('\0', ConsoleKey.RightArrow, false, false, false),
            new ConsoleKeyInfo('\0', ConsoleKey.Home, false, false, false),
            new ConsoleKeyInfo('\0', ConsoleKey.End, false, false, false),
            new ConsoleKeyInfo('\0', ConsoleKey.Delete, false, false, false),
            new ConsoleKeyInfo('\0', ConsoleKey.Backspace, false, false, false),
        ];

        /// <inheritdoc/>
        public override IAutoCompleteKeyPressHandler HandlerUnderTest => new LiteralKeyPressHandler();

        /// <summary>
        /// Tests that the ListeralKeyPressHandler inserts the keyed character
        /// into the user input and that no suggestion is selected.
        /// </summary>
        [Fact]
        public void Handle_InsertsUserInputAndSelectsNoSuggestion()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            var keyInfo = CreateKey('a', ConsoleKey.A);

            // Act
            handler.Handle(keyInfo, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.InsertUserInput('a'), Times.Once);
            mockAutoCompleter.Verify(m => m.SelectNoSuggestion(), Times.Once);
        }

        /// <summary>
        /// Tests that when the supplied console key does not represent
        /// a printable character, the handler does nothing.
        /// </summary>
        /// <param name="keyInfo">The keypress to handle.</param>
        [Theory]
        [MemberData(nameof(NonPrintableKeys))]
        public void Handle_NonPrintableCharacter_DoesNothing(ConsoleKeyInfo keyInfo)
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var mockAutoCompleter = new Mock<IAutoCompleter>();

            // Act
            handler.Handle(keyInfo, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.InsertUserInput(It.IsAny<char>()), Times.Never);
            mockAutoCompleter.Verify(m => m.SelectNoSuggestion(), Times.Never);
        }
    }
}
