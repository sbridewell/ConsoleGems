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
    }
}
