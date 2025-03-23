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
                var mockAutoCompleter = new Mock<IAutoCompleter>();
                TextCopy.ClipboardService.SetText("abc");
                var keyPress = CreateKey('v', ConsoleKey.V, control: true);

                // Act
                handler.Handle(keyPress, mockAutoCompleter.Object);

                // Assert
                mockAutoCompleter.Verify(m => m.InsertUserInput("abc"), Times.Once);
                mockAutoCompleter.Verify(m => m.SelectNoSuggestion(), Times.Once);
            }
        }

        /// <summary>
        /// Tests that the V key without the ctrl key is treated as a literal.
        /// </summary>
        [Fact]
        public void Handle_WithoutControlKey_IsTreatedAsLiteralKey()
        {
            var handler = this.HandlerUnderTest;
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            var keyInfo = CreateKey('v', ConsoleKey.V);

            // Act
            handler.Handle(keyInfo, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.InsertUserInput('v'), Times.Once);
            mockAutoCompleter.Verify(m => m.SelectNoSuggestion(), Times.Once);
        }
    }
}
