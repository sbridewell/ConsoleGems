// <copyright file="PainterTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

using Microsoft.CodeAnalysis.Operations;

namespace Sde.ConsoleGems.Test.FullScreen
{
    /// <summary>
    /// Unit tests for the <see cref="Painter"/> class.
    /// </summary>
    public class PainterTest
    {
        /// <summary>
        /// Tests that the constructor sets the Size and Position properties correctly.
        /// </summary>
        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var console = new Mock<IConsole>();
            var position = new ConsolePoint(1, 2);
            var size = new ConsoleSize(3, 4);

            // Act
            var painter = new TestPainter(console.Object, position, size);

            // Assert
            painter.Size.Should().Be(size);
            painter.Position.Should().Be(position);
        }

        /// <summary>
        /// Tests that the WriteToScreenBuffer method writes to the
        /// <see cref="Painter.ScreenBuffer"/> property correctly,
        /// and does not make any calls to the <see cref="IConsole"/>.
        /// </summary>
        /// <param name="lineNumber">
        /// The line number in the screen buffer to write to.
        /// </param>
        /// <param name="text">The text to write.</param>
        [InlineData(0, "Hello world")]
        [InlineData(1, "Hello world")]
        [InlineData(2, "Hello world")]
        [InlineData(3, "Hello world")]
        [Theory]
        public void WriteToScreenBuffer_ValidInput_WritesToBuffer(
            int lineNumber,
            string text)
        {
            // Arrange
            var console = new Mock<IConsole>();
            var position = new ConsolePoint(1, 2);
            var size = new ConsoleSize(11, 4);
            var painter = new TestPainter(console.Object, position, size);

            // Act
            painter.PublicWriteToScreenBuffer(lineNumber, text);

            // Assert
            painter.PublicScreenBuffer[lineNumber].Should().Be(text);
            console.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Tests that the correct exception is thrown when the
        /// <see cref="Painter.WriteToScreenBuffer(int, string)"/> method
        /// is passed a line number which is outside the vertical range
        /// of the are of the console window which the painter is responsible
        /// for.
        /// </summary>
        /// <param name="lineNumber">The line number.</param>
        /// <param name="expectedMessage">Expected exception message.</param>
        [InlineData(-1, "Line number -1 is outside the bounds of the painter's area. Must be between zero and 3. (Parameter 'lineNumber')")]
        [InlineData(4, "Line number 4 is outside the bounds of the painter's area. Must be between zero and 3. (Parameter 'lineNumber')")]
        [Theory]
        public void WriteToScreenBuffer_LineNumberOutOfRange_Throws(int lineNumber, string expectedMessage)
        {
            // Arrange
            var console = new Mock<IConsole>();
            var position = new ConsolePoint(1, 2);
            var size = new ConsoleSize(11, 4);
            var painter = new TestPainter(console.Object, position, size);

            // Act
            var action = () => painter.PublicWriteToScreenBuffer(lineNumber, "Hello world");

            // Assert
            var ex = action.Should().ThrowExactly<ArgumentOutOfRangeException>().Which;
            ex.Message.Should().Be(expectedMessage);
            ex.ParamName.Should().Be("lineNumber");
        }

        /// <summary>
        /// Tests that the <see cref="Painter.WriteToScreenBuffer"/> method throws
        /// the correct exception when the length of the supplied text does
        /// not match the width of the area of the console window that the
        /// painter is responsible for.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <param name="expectedMessage">Expected error message.</param>
        [InlineData("Hello worl", "The length of the supplied text (10) does not match the width of the painter's area (11). (Parameter 'text')")]
        [InlineData("Hello world!", "The length of the supplied text (12) does not match the width of the painter's area (11). (Parameter 'text')")]
        [Theory]
        public void WriteToScreenBuffer_TextLengthDoesNotMatchPainterWidth_Throws(string text, string expectedMessage)
        {
            // Arrange
            var console = new Mock<IConsole>();
            var position = new ConsolePoint(1, 2);
            var size = new ConsoleSize(11, 4);
            var painter = new TestPainter(console.Object, position, size);

            // Act
            var action = () => painter.PublicWriteToScreenBuffer(0, text);

            // Assert
            var ex = action.Should().ThrowExactly<ArgumentOutOfRangeException>().Which;
            ex.Message.Should().Be(expectedMessage);
            ex.ParamName.Should().Be("text");
        }

        /// <summary>
        /// Tests that the <see cref="Painter.Paint"/> method writes the
        /// contents of the screen buffer to the console.
        /// </summary>
        [Fact]
        public void Paint_WritesScreenBufferToConsole()
        {
            // Arrange
            var console = new Mock<IConsole>();
            var position = new ConsolePoint(1, 2);
            var size = new ConsoleSize(11, 4);
            var painter = new TestPainter(console.Object, position, size);
            var linesToWrite = new string[]
            {
                "Hello world",
                "Paint test ",
                "ConsoleGems",
                "TestPainter",
            };
            painter.PublicWriteToScreenBuffer(0, linesToWrite[0]);
            painter.PublicWriteToScreenBuffer(1, linesToWrite[1]);
            painter.PublicWriteToScreenBuffer(2, linesToWrite[2]);
            painter.PublicWriteToScreenBuffer(3, linesToWrite[3]);

            // Act
            painter.Paint();

            // Assert
            console.VerifySet(m => m.CursorLeft = 1);
            console.VerifySet(m => m.CursorTop = 2);
            console.Verify(m => m.WriteLine(linesToWrite[0], It.IsAny<ConsoleOutputType>()), Times.Once);
            console.Verify(m => m.WriteLine(linesToWrite[1], It.IsAny<ConsoleOutputType>()), Times.Once);
            console.Verify(m => m.WriteLine(linesToWrite[2], It.IsAny<ConsoleOutputType>()), Times.Once);
            console.Verify(m => m.WriteLine(linesToWrite[3], It.IsAny<ConsoleOutputType>()), Times.Once);
        }
    }
}
