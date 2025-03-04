// <copyright file="HomeEndKeyPressHandlerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.AutoComplete.KeyPressHandlers
{
    /// <summary>
    /// Unit tests for the <see cref="HomeEndKeyPressHandler"/> class.
    /// </summary>
    public class HomeEndKeyPressHandlerTest : KeyPressHandlerTestBase
    {
        /// <inheritdoc/>
        public override IAutoCompleteKeyPressHandler HandlerUnderTest => new HomeEndKeyPressHandler();

        /// <summary>
        /// Tests that the home key moves the cursor to the start of the user input
        /// and does not change the user input.
        /// </summary>
        [Fact]
        public void Handle_HomeKey_MovesCursorToStartOfUserInput()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var enteredText = "123456";
            var autoCompleter = this.CreateAutoCompleter(enteredText);
            var homeKeyPress = CreateKey(ConsoleKey.Home);

            // Act
            handler.Handle(homeKeyPress, autoCompleter);

            // Assert
            autoCompleter.CursorPositionWithinUserInput.Should().Be(0);
            autoCompleter.UserInput.Should().Be(enteredText);
        }

        /// <summary>
        /// Tests that the end key moves the cursor to the end of the user
        /// input and does not change the user input.
        /// </summary>
        [Fact]
        public void Handle_EndKey_MovesCursorToEndOfUserInput()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var enteredText = "123456";
            var autoCompleter = this.CreateAutoCompleter(enteredText);
            autoCompleter.MoveCursorToHome();
            autoCompleter.MoveCursorRight(3);
            var endKeyPress = CreateKey(ConsoleKey.End);

            // Act
            handler.Handle(endKeyPress, autoCompleter);

            // Assert
            autoCompleter.CursorPositionWithinUserInput.Should().Be(autoCompleter.UserInput.Length);
            autoCompleter.UserInput.Should().Be(enteredText);
        }
    }
}
