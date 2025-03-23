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
        public void Handle_LeftArrow_MovesCursorLeftByOne()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            var leftArrowKeyPress = CreateKey(ConsoleKey.LeftArrow);

            // Act
            handler.Handle(leftArrowKeyPress, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.MoveCursorLeft(), Times.Once);
            mockAutoCompleter.Verify(m => m.MoveCursorRight(), Times.Never);
        }

        /// <summary>
        /// Happy path test for handling the right arrow key.
        /// </summary>
        [Fact]
        public void Handle_RightArrow_MovesCursorRightByOne()
        {
            // Arrange
            var handler = this.HandlerUnderTest;
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            var rightArrowKeyPress = CreateKey(ConsoleKey.RightArrow);

            // Act
            handler.Handle(rightArrowKeyPress, mockAutoCompleter.Object);

            // Assert
            mockAutoCompleter.Verify(m => m.MoveCursorLeft(), Times.Never);
            mockAutoCompleter.Verify(m => m.MoveCursorRight(), Times.Once);
        }
    }
}
