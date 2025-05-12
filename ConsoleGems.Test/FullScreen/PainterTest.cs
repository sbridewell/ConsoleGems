// <copyright file="PainterTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.FullScreen
{
    using Xunit.Abstractions;

    /// <summary>
    /// Unit tests for the <see cref="Painter"/> class.
    /// </summary>
    public class PainterTest(ITestOutputHelper output)
    {
        /// <summary>
        /// Tests that the constructor sets the Size and Position properties correctly.
        /// </summary>
        /// <param name="hasBorder">True if the painter should have a border.</param>
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Constructor_SetsPropertiesCorrectly(bool hasBorder)
        {
            // Arrange
            var console = new Mock<IConsole>();
            var position = new ConsolePoint(1, 2);
            var size = new ConsoleSize(3, 4);

            // Act
            var painter = new TestPainter(console.Object, position, size, hasBorder);

            // Assert
            painter.InnerSize.Should().Be(size);
            painter.OuterSize.Width.Should().Be(size.Width + (hasBorder ? 2 : 0));
            painter.OuterSize.Height.Should().Be(size.Height + (hasBorder ? 2 : 0));
            painter.Origin.Should().Be(position);
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
        /// <param name="hasBorder">True to draw a border around the painter's content.</param>
        [InlineData(0, "Hello world", true)]
        [InlineData(1, "ConsoleGems", true)]
        [InlineData(2, "Hurrah!!!!!", true)]
        [InlineData(3, "That's all.", true)]
        [InlineData(0, "Hello world", false)]
        [InlineData(1, "ConsoleGems", false)]
        [InlineData(2, "Hurrah!!!!!", false)]
        [InlineData(3, "That's all.", false)]
        [Theory]
        public void WriteToScreenBuffer_ValidInput_WritesToBuffer(
            int lineNumber,
            string text,
            bool hasBorder)
        {
            // Arrange
            var console = new Mock<IConsole>();
            var position = new ConsolePoint(1, 2);
            var size = new ConsoleSize(11, 4);
            var painter = new TestPainter(console.Object, position, size, hasBorder);

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
        /// <param name="hasBorder">True to draw a border around the painter's content.</param>
        [InlineData(
            -1,
            "Line number -1 is outside the bounds of the painter's area. Must be between zero and 3. (Parameter 'lineNumber')",
            true)]
        [InlineData(
            4,
            "Line number 4 is outside the bounds of the painter's area. Must be between zero and 3. (Parameter 'lineNumber')",
            true)]
        [InlineData(
            -1,
            "Line number -1 is outside the bounds of the painter's area. Must be between zero and 3. (Parameter 'lineNumber')",
            false)]
        [InlineData(
            4,
            "Line number 4 is outside the bounds of the painter's area. Must be between zero and 3. (Parameter 'lineNumber')",
            false)]
        [Theory]
        public void WriteToScreenBuffer_LineNumberOutOfRange_Throws(int lineNumber, string expectedMessage, bool hasBorder)
        {
            // Arrange
            var console = new Mock<IConsole>();
            var position = new ConsolePoint(1, 2);
            var size = new ConsoleSize(11, 4);
            var painter = new TestPainter(console.Object, position, size, hasBorder);

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
        /// <param name="hasBorder">True to draw a border around the painter's content.</param>
        [InlineData(
            "Hello worl",
            "The length of the supplied text (10) does not match the width of the painter's area (11). (Parameter 'text')",
            true)]
        [InlineData(
            "Hello world!",
            "The length of the supplied text (12) does not match the width of the painter's area (11). (Parameter 'text')",
            true)]
        [InlineData(
            "Hello worl",
            "The length of the supplied text (10) does not match the width of the painter's area (11). (Parameter 'text')",
            false)]
        [InlineData(
            "Hello world!",
            "The length of the supplied text (12) does not match the width of the painter's area (11). (Parameter 'text')",
            false)]
        [Theory]
        public void WriteToScreenBuffer_TextLengthDoesNotMatchPainterWidth_Throws(
            string text,
            string expectedMessage,
            bool hasBorder)
        {
            // Arrange
            var console = new Mock<IConsole>();
            var position = new ConsolePoint(1, 2);
            var size = new ConsoleSize(11, 4);
            var painter = new TestPainter(console.Object, position, size, hasBorder);

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
        /// <param name="hasBorder">True to draw a border around the painter's content.</param>
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Paint_WritesScreenBufferToConsole(bool hasBorder)
        {
            // Arrange
            var console = new Mock<IConsole>();
            var position = new ConsolePoint(1, 2);
            var size = new ConsoleSize(11, 4);
            var painter = new TestPainter(console.Object, position, size, hasBorder);
            var linesToWrite = new string[]
            {
                "Hello world",
                "Paint test ",
                "ConsoleGems",
                "TestPainter",
            };
            ////painter.ScreenBuffer[0] = linesToWrite[0]; // CS0200 - ScreenBuffer is read only :-)
            ////painter.WriteToScreenBuffer(0, linesToWrite[0]); // CS0122 - WriteToScreenBuffer is protected :-)
            painter.PublicWriteToScreenBuffer(0, linesToWrite[0]);
            painter.PublicWriteToScreenBuffer(1, linesToWrite[1]);
            painter.PublicWriteToScreenBuffer(2, linesToWrite[2]);
            painter.PublicWriteToScreenBuffer(3, linesToWrite[3]);

            // Act
            painter.Paint();

            // Assert
            console.VerifySet(m => m.CursorLeft = 1);
            console.VerifySet(m => m.CursorTop = 2);
            console.Verify(m => m.Write(linesToWrite[0], It.IsAny<ConsoleOutputType>()), Times.Once);
            console.Verify(m => m.Write(linesToWrite[1], It.IsAny<ConsoleOutputType>()), Times.Once);
            console.Verify(m => m.Write(linesToWrite[2], It.IsAny<ConsoleOutputType>()), Times.Once);
            console.Verify(m => m.Write(linesToWrite[3], It.IsAny<ConsoleOutputType>()), Times.Once);
        }

        /// <summary>
        /// Not really a unit test, instead it writes to the test output window using
        /// <see cref="TestOutputHelperConsole"/> to enable a visual check.
        /// </summary>
        /// <param name="hasBorder">True to draw a border around the painter's content.</param>
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void WriteConsoleOutputToTestOutputWindow(bool hasBorder)
        {
            // Arrange
            var innerSize = new ConsoleSize(11, 4);
            var consoleSize = new ConsoleSize(
                innerSize.Width + (hasBorder ? 2 : 0),
                innerSize.Height + (hasBorder ? 2 : 0));
            var console = new TestOutputHelperConsole(output, consoleSize);
            console.Clear();
            var position = new ConsolePoint(0, 0);
            var painter = new TestPainter(console, position, innerSize, hasBorder);
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
            console.Flush();
            Assert.True(true); // just to stop the analyzer compalining that there are no asserts
        }
    }
}
