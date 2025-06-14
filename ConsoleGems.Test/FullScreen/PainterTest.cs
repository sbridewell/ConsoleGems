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
            var mockConsole = new Mock<IConsole>();
            var mockBorderPainter = new Mock<IBorderPainter>();
            var position = new ConsolePoint(1, 2);
            var size = new ConsoleSize(3, 4);

            // Act
            var painter = new TestPainter(mockConsole.Object, mockBorderPainter.Object)
            {
                Origin = position,
                InnerSize = size,
                HasBorder = hasBorder,
            };

            // Assert
            painter.InnerSize.Should().Be(size);
            painter.OuterSize.Width.Should().Be(size.Width + (hasBorder ? 2 : 0));
            painter.OuterSize.Height.Should().Be(size.Height + (hasBorder ? 2 : 0));
            painter.Origin.Should().Be(position);
        }

        /// <summary>
        /// Tests that the WriteToScreenBuffer method writes to the screen buffer correctly,
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
            var mockConsole = new Mock<IConsole>();
            var mockBorderPainter = new Mock<IBorderPainter>();
            var position = new ConsolePoint(1, 2);
            var size = new ConsoleSize(11, 4);
            var painter = new TestPainter(mockConsole.Object, mockBorderPainter.Object)
            {
                Origin = position,
                InnerSize = size,
                HasBorder = hasBorder,
            };

            // Act
            for (var i = 0; i < text.Length; i++)
            {
                painter.PublicWriteToScreenBuffer(i, lineNumber, text[i], ConsoleOutputType.Default);
            }

            // Assert
            painter.PublicScreenBuffer.ToStringArray()[lineNumber].Should().Be(text);
            mockConsole.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Tests that the correct exception is thrown when the
        /// <see cref="Painter.WriteToScreenBuffer"/> method
        /// is passed a X coordinate which is outside the horizontal range
        /// of the are of the console window which the painter is responsible
        /// for.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="hasBorder">True to draw a border around the painter's content.</param>
        [InlineData(-1, true)]
        [InlineData(11, true)]
        [InlineData(-1, false)]
        [InlineData(11, false)]
        [Theory]
        public void WriteToScreenBuffer_XOutOfRange_Throws(int x, bool hasBorder)
        {
            // Arrange
            var mockConsole = new Mock<IConsole>();
            var mockBorderPainter = new Mock<IBorderPainter>();
            var position = new ConsolePoint(1, 2);
            var size = new ConsoleSize(11, 4);
            var expectedMsg =
                $"X coordinate {x} is outside the bounds of the painter's area. "
                + $"Must be between zero and {size.Width - 1}. (Parameter 'x')";
            var painter = new TestPainter(mockConsole.Object, mockBorderPainter.Object)
            {
                Origin = position,
                InnerSize = size,
                HasBorder = hasBorder,
            };

            // Act
            var action = () => painter.PublicWriteToScreenBuffer(x, 0, 'a', ConsoleOutputType.Default);

            // Assert
            var ex = action.Should().ThrowExactly<ArgumentOutOfRangeException>().Which;
            ex.Message.Should().Be(expectedMsg);
            ex.ParamName.Should().Be("x");
        }

        /// <summary>
        /// Tests that the correct exception is thrown when the
        /// <see cref="Painter.WriteToScreenBuffer"/> method
        /// is passed a Y coordinate which is outside the vertical range
        /// of the are of the console window which the painter is responsible
        /// for.
        /// </summary>
        /// <param name="lineNumber">The line number.</param>
        /// <param name="hasBorder">True to draw a border around the painter's content.</param>
        [InlineData(-1, true)]
        [InlineData(4, true)]
        [InlineData(-1, false)]
        [InlineData(4, false)]
        [Theory]
        public void WriteToScreenBuffer_YOutOfRange_Throws(int lineNumber, bool hasBorder)
        {
            // Arrange
            var mockConsole = new Mock<IConsole>();
            var mockBorderPainter = new Mock<IBorderPainter>();
            var position = new ConsolePoint(1, 2);
            var size = new ConsoleSize(11, 4);
            var expectedMsg =
                $"Y coordinate {lineNumber} is outside the bounds of the painter's area. "
                + $"Must be between zero and {size.Height - 1}. (Parameter 'y')";
            var painter = new TestPainter(mockConsole.Object, mockBorderPainter.Object)
            {
                Origin = position,
                InnerSize = size,
                HasBorder = hasBorder,
            };

            // Act
            var action = () => painter.PublicWriteToScreenBuffer(0, lineNumber, 'a', ConsoleOutputType.Default);

            // Assert
            var ex = action.Should().ThrowExactly<ArgumentOutOfRangeException>().Which;
            ex.Message.Should().Be(expectedMsg);
            ex.ParamName.Should().Be("y");
        }

        /// <summary>
        /// Tests that the correct exception is thrown if the WriteToScreenBuffer method is
        /// called before the screen buffer has been initialised.
        /// </summary>
        [Fact]
        public void WriteScreenBufferToConsole_ScreenBufferNotInitialised_Throws()
        {
            // Arrange
            var mockConsole = new Mock<IConsole>();
            var mockBorderPainter = new Mock<IBorderPainter>();
            var expectedMsg = "ScreenBuffer is not initialised. Set InnerSize before writing to the screen buffer.";
            var painter = new TestPainter(mockConsole.Object, mockBorderPainter.Object);

            // Act
            var actionn = () => painter.PublicWriteToScreenBuffer(0, 0, 'a', ConsoleOutputType.Default);

            // Assert
            var ex = actionn.Should().ThrowExactly<InvalidOperationException>().Which;
            ex.Message.Should().Be(expectedMsg);
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
            var mockConsole = new Mock<IConsole>();
            var mockBorderPainter = new Mock<IBorderPainter>();
            var painterOrigin = new ConsolePoint(1, 2);
            var painterSize = new ConsoleSize(11, 4);
            var painter = new TestPainter(mockConsole.Object, mockBorderPainter.Object)
            {
                Origin = painterOrigin,
                InnerSize = painterSize,
                HasBorder = hasBorder,
            };
            var linesToWrite = new string[]
            {
                "Hello world",
                "Paint test ",
                "ConsoleGems",
                "TestPainter",
            };
            ////painter.WriteToScreenBuffer(0, linesToWrite[0]); // CS0122 - WriteToScreenBuffer is protected :-)
            for (var y = 0; y < linesToWrite.Length; y++)
            {
                var lineToWrite = linesToWrite[y];
                for (var x = 0; x < lineToWrite.Length; x++)
                {
                    painter.PublicWriteToScreenBuffer(x, y, lineToWrite[x], ConsoleOutputType.Default);
                }
            }

            // Act
            painter.Paint();

            // Assert
            mockConsole.Verify(m => m.Write(linesToWrite[0], It.IsAny<ConsoleOutputType>()), Times.Once);
            mockConsole.Verify(m => m.Write(linesToWrite[1], It.IsAny<ConsoleOutputType>()), Times.Once);
            mockConsole.Verify(m => m.Write(linesToWrite[2], It.IsAny<ConsoleOutputType>()), Times.Once);
            mockConsole.Verify(m => m.Write(linesToWrite[3], It.IsAny<ConsoleOutputType>()), Times.Once);
            mockBorderPainter.Verify(m => m.PaintBorderIfRequired(), Times.Once);
        }

        /// <summary>
        /// Tests that when the HasBorder property of a painter is set to true,
        /// the painter paints the border correctly.
        /// </summary>
        [Fact]
        public void Paint_WithBorder_PaintsBorderCorrectly()
        {
            // Arrange
            var painterOrigin = new ConsolePoint(1, 2);
            var painterInnerSize = new ConsoleSize(11, 4);
            var console = new TextWriterConsole();
            console.WindowWidth = painterInnerSize.Width + 2 + painterOrigin.X;
            console.WindowHeight = painterInnerSize.Height + 2 + painterOrigin.Y;
            var borderPainter = new BorderPainter(console);
            var painter = new TestPainter(console, borderPainter)
            {
                Origin = painterOrigin,
                InnerSize = painterInnerSize,
                HasBorder = true,
            };
            var linesToWrite = new string[]
            {
                "Hello world",
                "Paint test ",
                "ConsoleGems",
                "TestPainter",
            };
            for (var y = 0; y < linesToWrite.Length; y++)
            {
                var lineToWrite = linesToWrite[y];
                for (var x = 0; x < lineToWrite.Length; x++)
                {
                    painter.PublicWriteToScreenBuffer(x, y, lineToWrite[x], ConsoleOutputType.Default);
                }
            }

            // Act
            painter.Paint();

            // Assert
            output.WriteLine(console.ToString());
            console.ToString().Should().Be(
                "              " + Environment.NewLine +
                "              " + Environment.NewLine +
                " ╭───────────╮" + Environment.NewLine +
                " │Hello world│" + Environment.NewLine +
                " │Paint test │" + Environment.NewLine +
                " │ConsoleGems│" + Environment.NewLine +
                " │TestPainter│" + Environment.NewLine +
                " ╰───────────╯" + Environment.NewLine);
        }

        /// <summary>
        /// Tests that the correct exception is thrown if the Paint method is
        /// called before the screen buffer is initialised.
        /// </summary>
        [Fact]
        public void Paint_ScreenBufferNotInitialised_Throws()
        {
            // Arrange
            var console = new TextWriterConsole();
            var mockBorderPainter = new Mock<IBorderPainter>();
            var painter = new TestPainter(console, mockBorderPainter.Object);

            // Act
            var action = () => painter.Paint();

            // Assert
            var ex = action.Should().ThrowExactly<InvalidOperationException>().Which;
            ex.Message.Should().Be("ScreenBuffer is not initialised. Set InnerSize before calling Paint.");
        }

        /// <summary>
        /// Tests that the Reset method calls the border painter's Reset method.
        /// </summary>
        [Fact]
        public void Reset_ResetsBorderPainter()
        {
            // Arrange
            var mockConsole = new Mock<IConsole>();
            var mockBorderPainter = new Mock<IBorderPainter>();
            var painter = new TestPainter(mockConsole.Object, mockBorderPainter.Object);

            // Act
            painter.Reset();

            // Assert
            mockBorderPainter.Verify(m => m.Reset(), Times.Once);
        }

        /// <summary>
        /// Tests that the ClearScreenBuffer method sets all characters in the
        /// screen buffer to spaces.
        /// </summary>
        [Fact]
        public void ClearScreenBuffer_SetsAllCharactersToSpaces()
        {
            // Arrange
            var mockConsole = new Mock<IConsole>();
            var mockBorderPainter = new Mock<IBorderPainter>();
            var innerWidth = 11;
            var innerHeight = 4;
            var painterOrigin = new ConsolePoint(1, 2);
            var painterInnerSize = new ConsoleSize(innerWidth, innerHeight);
            var painter = new TestPainter(mockConsole.Object, mockBorderPainter.Object)
            {
                Origin = painterOrigin,
                InnerSize = painterInnerSize,
                HasBorder = false,
            };
            for (var y = 0; y < innerHeight; y++)
            {
                for (var x = 0; x < innerWidth; x++)
                {
                    painter.PublicWriteToScreenBuffer(x, y, 'X', ConsoleOutputType.Default);
                }
            }

            // Act
            painter.PublicClearScreenBuffer();

            // Assert
            var screenBuffer = painter.PublicScreenBuffer;
            screenBuffer.ToStringArray().Should().Equal(
                new string(' ', innerWidth),
                new string(' ', innerWidth),
                new string(' ', innerWidth),
                new string(' ', innerWidth));
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
            var borderPainter = new BorderPainter(console);
            var position = new ConsolePoint(0, 0);
            var painter = new TestPainter(console, borderPainter)
            {
                Origin = position,
                InnerSize = innerSize,
                HasBorder = hasBorder,
            };
            var linesToWrite = new string[]
            {
                "Hello world",
                "Paint test ",
                "ConsoleGems",
                "TestPainter",
            };
            for (var y = 0; y < linesToWrite.Length; y++)
            {
                var lineToWrite = linesToWrite[y];
                for (var x = 0; x < lineToWrite.Length; x++)
                {
                    painter.PublicWriteToScreenBuffer(x, y, lineToWrite[x], ConsoleOutputType.Default);
                }
            }

            // Act
            painter.Paint();

            // Assert
            console.Flush();
            Assert.True(true); // just to stop the analyzer complaining that there are no asserts
        }
    }
}
