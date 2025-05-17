// <copyright file="TestOutputHelperConsoleTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.FullScreen
{
    using Xunit.Abstractions;

    /// <summary>
    /// Unit tests for the <see cref="TestOutputHelperConsole"/> class.
    /// </summary>
    public class TestOutputHelperConsoleTest
    {
        /// <summary>
        /// Tests that the Clear method initialises the console buffer by filling each element
        /// of  the array with the correct number of spaces.
        /// </summary>
        /// <param name="consoleWidth">Console width in characters.</param>
        /// <param name="consoleHeight">Console height in characters.</param>
        [InlineData(10, 1)]
        [InlineData(10, 2)]
        [InlineData(5, 5)]
        [InlineData(2, 10)]
        [Theory]
        public void Clear_InitialisesScreenBuffer(int consoleWidth, int consoleHeight)
        {
            // Arrange
            var mockOutput = new Mock<ITestOutputHelper>();
            var console = new TestOutputHelperConsole(mockOutput.Object, new ConsoleSize(consoleWidth, consoleHeight));
            console.CursorLeft = 0;
            console.CursorTop = 0;

            // Act
            console.Clear();

            // Assert
            mockOutput.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Never);
            console.ScreenBuffer.Should().HaveCount(consoleHeight);
            foreach (var line in console.ScreenBuffer)
            {
                line.Should().Be(new string(' ', consoleWidth));
            }
        }

        /// <summary>
        /// Tests that the Write method which accepts a character populates the screen
        /// buffer and sets cursor position correctly.
        /// </summary>
        /// <param name="consoleWidth">Console width in characters.</param>
        /// <param name="consoleHeight">Console height in characters.</param>
        /// <param name="initialCursorLeft">Initial X position of the cursor.</param>
        /// <param name="initialCursorTop">Initial Y position of the cursor.</param>
        /// <param name="characterToWrite">The character to write.</param>
        /// <param name="expectedLine">
        /// The expected contents of the updated line in the screen buffer.
        /// </param>
        /// <param name="expectedCursorLeft">
        /// Expected cursor X position after the Write call.
        /// </param>
        /// <param name="expectedCursorTop">
        /// Expected cursor Y position after the Write call.
        /// </param>
        [InlineData(4, 1, 0, 0, 'a', "a   ", 1, 0)]
        [InlineData(4, 1, 1, 0, 'a', " a  ", 2, 0)]
        [InlineData(4, 1, 2, 0, 'a', "  a ", 3, 0)]
        [InlineData(4, 1, 3, 0, 'a', "   a", 0, 0)]
        [InlineData(5, 1, 0, 0, 'a', "a    ", 1, 0)]
        [InlineData(5, 1, 1, 0, 'a', " a   ", 2, 0)]
        [InlineData(5, 1, 2, 0, 'a', "  a  ", 3, 0)]
        [InlineData(5, 1, 3, 0, 'a', "   a ", 4, 0)]
        [InlineData(5, 1, 4, 0, 'a', "    a", 0, 0)]
        [InlineData(2, 2, 0, 0, 'a', "a ", 1, 0)]
        [InlineData(2, 2, 1, 0, 'a', " a", 0, 1)]
        [InlineData(2, 2, 0, 1, 'a', "a ", 1, 1)]
        [InlineData(2, 2, 1, 1, 'a', " a", 0, 0)]
        [Theory]
        public void Write_Character_SetsScreenBufferAndConsolePositionCorrectly(
            int consoleWidth,
            int consoleHeight,
            int initialCursorLeft,
            int initialCursorTop,
            char characterToWrite,
            string expectedLine,
            int expectedCursorLeft,
            int expectedCursorTop)
        {
            // Arrange
            var mockOutput = new Mock<ITestOutputHelper>();
            var console = new TestOutputHelperConsole(mockOutput.Object, new ConsoleSize(consoleWidth, consoleHeight))
            {
                CursorLeft = initialCursorLeft,
                CursorTop = initialCursorTop,
            };
            console.Clear();

            // Act
            console.Write(characterToWrite);
            console.Flush();

            // Assert
            mockOutput.Verify(m => m.WriteLine(expectedLine), Times.Once);
            console.CursorTop.Should().Be(expectedCursorTop);
            console.CursorLeft.Should().Be(expectedCursorLeft);
            mockOutput.Verify(m => m.WriteLine(new string(' ', consoleWidth)), Times.Exactly(consoleHeight - 1));
        }

        /// <summary>
        /// Tests that the Write method which accepts a string populates the screen
        /// buffer and sets cursor position correctly.
        /// </summary>
        /// <param name="consoleWidth">Console width in characters.</param>
        /// <param name="consoleHeight">Console height in characters.</param>
        /// <param name="initialCursorLeft">Initial X position of the cursor.</param>
        /// <param name="initialCursorTop">Initial Y position of the cursor.</param>
        /// <param name="stringToWrite">The string to write.</param>
        /// <param name="expectedLines">
        /// The expected contents of the updated lines in the screen buffer.
        /// </param>
        /// <param name="expectedCursorLeft">
        /// Expected cursor X position after the Write call.
        /// </param>
        /// <param name="expectedCursorTop">
        /// Expected cursor Y position after the Write call.
        /// </param>
        [InlineData(5, 1, 0, 0, "abc", new string[] { "abc  " }, 3, 0)]
        [InlineData(5, 1, 1, 0, "abc", new string[] { " abc " }, 4, 0)]
        [InlineData(5, 1, 2, 0, "abc", new string[] { "  abc" }, 0, 0)]
        [InlineData(5, 2, 0, 0, "abc", new string[] { "abc  " }, 3, 0)]
        [InlineData(5, 2, 1, 0, "abc", new string[] { " abc " }, 4, 0)]
        [InlineData(5, 2, 2, 0, "abc", new string[] { "  abc" }, 0, 1)]
        [InlineData(5, 2, 0, 1, "abc", new string[] { "abc  " }, 3, 1)]
        [InlineData(5, 2, 1, 1, "abc", new string[] { " abc " }, 4, 1)]
        [InlineData(5, 2, 2, 1, "abc", new string[] { "  abc" }, 0, 0)]
        [InlineData(5, 2, 3, 0, "abc", new string[] { "   ab", "c    " }, 1, 1)]
        [InlineData(5, 2, 4, 0, "abc", new string[] { "    a", "bc   " }, 2, 1)]
        [Theory]
        public void Write_String_SetsScreenBufferAndConsolePositionCorrectly(
            int consoleWidth,
            int consoleHeight,
            int initialCursorLeft,
            int initialCursorTop,
            string stringToWrite,
            string[] expectedLines,
            int expectedCursorLeft,
            int expectedCursorTop)
        {
            // Arrange
            var mockOutput = new Mock<ITestOutputHelper>();
            var console = new TestOutputHelperConsole(mockOutput.Object, new ConsoleSize(consoleWidth, consoleHeight))
            {
                CursorLeft = initialCursorLeft,
                CursorTop = initialCursorTop,
            };
            console.Clear();

            // Act
            console.Write(stringToWrite);
            console.Flush();

            // Assert
            foreach (var expectedLine in expectedLines)
            {
                mockOutput.Verify(m => m.WriteLine(expectedLine), Times.Once);
            }

            console.CursorTop.Should().Be(expectedCursorTop);
            console.CursorLeft.Should().Be(expectedCursorLeft);
            mockOutput.Verify(m => m.WriteLine(new string(' ', consoleWidth)), Times.Exactly(consoleHeight - expectedLines.Length));
        }
    }
}
