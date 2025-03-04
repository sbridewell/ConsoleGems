// <copyright file="VKeyPressHandlerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.AutoComplete.KeyPressHandlers
{
    /// <summary>
    /// Unit tests for the <see cref="VKeyPressHandler"/> class.
    /// </summary>
    public class VKeyPressHandlerTest : KeyPressHandlerTestBase
    {
        /// <inheritdoc/>
        public override IAutoCompleteKeyPressHandler HandlerUnderTest => new VKeyPressHandler();

        /// <summary>
        /// Tests that ctrl + V performs a paste operation and positions the cursor after the pasted text.
        /// </summary>
        [Fact]
        public void Handle_WithControlKey_PastesFromClipboardAndPositionsCursorAfterPastedText()
        {
            lock (LockObjects.ClipboardLock)
            {
                // Arrange
                var handler = this.HandlerUnderTest;
                var enteredText = "123456";
                var autoCompleter = this.CreateAutoCompleter(enteredText);
                autoCompleter.MoveCursorToHome();
                autoCompleter.MoveCursorRight(3);
                TextCopy.ClipboardService.SetText("abc");
                var keyPress = CreateKey('v', ConsoleKey.V, control: true);

                // Act
                handler.Handle(keyPress, autoCompleter);

                // Assert
                autoCompleter.UserInput.Should().Be("123abc456");
                autoCompleter.CursorPositionWithinUserInput.Should().Be(6);
            }
        }

        /// <summary>
        /// Tests that the V key without the ctrl key is treated as a literal.
        /// </summary>
        [Fact]
        public void Handle_WithoutControlKey_IsTreatedAsLiteralKey()
        {
            var handler = this.HandlerUnderTest;
            var enteredText = "123456";
            var autoCompleter = this.CreateAutoCompleter(enteredText);
            autoCompleter.MoveCursorToHome();
            autoCompleter.MoveCursorRight(3);
            var keyInfo = CreateKey('v', ConsoleKey.V);

            // Act
            handler.Handle(keyInfo, autoCompleter);

            // Assert
            autoCompleter.UserInput.Should().Be("123v456");
            autoCompleter.CursorPositionWithinUserInput.Should().Be(4);
        }
    }
}
