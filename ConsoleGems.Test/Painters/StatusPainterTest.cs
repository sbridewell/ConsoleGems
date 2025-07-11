// <copyright file="StatusPainterTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Painters
{
    using Moq;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.FullScreen;
    using Sde.ConsoleGems.Painters;
    using Sde.ConsoleGems.Text;

    /// <summary>
    /// Unit tests for the <see cref="StatusPainter"/> class.
    /// </summary>
    public class StatusPainterTest
    {
        /// <summary>
        /// Tests that the Paint method does nothing when passed a null string.
        /// </summary>
        [Fact]
        public void Paint_NullText_ReturnsWithoutWritingToScreenBuffer()
        {
            // Arrange
            var mockConsole = new Mock<IConsole>();
            var mockBorderPainter = new Mock<IBorderPainter>();
            var painter = new StatusPainter(mockConsole.Object, mockBorderPainter.Object);

            // Act
            painter.Paint(null!);

            // Assert
            mockConsole.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Tests that the Paint method writes the correct text to the console.
        /// </summary>
        /// <param name="textParameter">The text to pass to the Paint method.</param>
        /// <param name="expectedText">The text which is expected to be written to the console.</param>
        /// <param name="consoleOutputType">The console output type to write the text with.</param>
        [Theory]
        [InlineData("Status: OK", "Status: OK          ", ConsoleOutputType.Default)]
        [InlineData("foo", "foo                 ", ConsoleOutputType.MenuHeader)]
        public void Paint_WritesCorrectText(
            string textParameter,
            string expectedText,
            ConsoleOutputType consoleOutputType)
        {
            // Arrange
            var mockConsole = new Mock<IConsole>();
            var mockBorderPainter = new Mock<IBorderPainter>();
            var painter = new StatusPainter(mockConsole.Object, mockBorderPainter.Object);
            var innerWidth = 20;
            painter.InnerSize = new ConsoleSize(innerWidth, 1);

            // Act
            painter.Paint(textParameter, consoleOutputType);

            // Assert
            mockConsole.Verify(c => c.Write(expectedText, consoleOutputType), Times.Once);
        }
    }
}
