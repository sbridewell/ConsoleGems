// <copyright file="BorderPainterTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.FullScreen
{
    /// <summary>
    /// Unit tests for the <see cref="BorderPainter"/> class.
    /// </summary>
    public class BorderPainterTest
    {
        private static readonly ConsoleOutputType ExpectedConsoleOutputType = ConsoleOutputType.MenuBody;

        /// <summary>
        /// Tests that the PaintTopBorderIfRequired writes the correct output to the console.
        /// </summary>
        /// <param name="innerWidth">Width of the painter excluding the border.</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void PaintBorderIfRequired_BorderIsRequired_WritesToConsoleCorrectly(int innerWidth)
        {
            // Arrange
            var innerHeight = 5;
            var (mockConsole, borderPainter) = Arrange(new ConsoleSize(innerWidth, innerHeight), true, false, false);

            // Act
            borderPainter.PaintBorderIfRequired();

            // Assert - top & bottom border
            mockConsole.Verify(c => c.Write("╭", ExpectedConsoleOutputType), Times.Once);
            mockConsole.Verify(c => c.WriteLine("╮", ExpectedConsoleOutputType), Times.Once);
            mockConsole.Verify(c => c.Write("╰", ExpectedConsoleOutputType), Times.Once);
            mockConsole.Verify(c => c.WriteLine("╯", ExpectedConsoleOutputType), Times.Once);
            mockConsole.Verify(c => c.Write(new string('─', innerWidth), ExpectedConsoleOutputType), Times.Exactly(2));

            // Assert - side borders
            mockConsole.VerifySet(m => m.CursorLeft = 0, Times.Exactly(innerHeight + 2));
            mockConsole.VerifySet(m => m.CursorLeft = innerWidth + 1, Times.Exactly(innerHeight));
            mockConsole.Verify(c => c.Write("│", ExpectedConsoleOutputType), Times.Exactly(innerHeight));
            mockConsole.Verify(c => c.WriteLine("│", ExpectedConsoleOutputType), Times.Exactly(innerHeight));
        }

        /// <summary>
        /// Tests that nothing is written to the console by the PaintBorderIfRequired
        /// method for scenarios where the border does not need painting.
        /// Output should only be written to the console if HasBorder is true,
        /// the border painter's painter is not null, and the border hasn't already
        /// been painted.
        /// </summary>
        /// <param name="hasBorder">True if the painter should have a border.</param>
        /// <param name="painterIsNull">True if the border painter's painter should be null.</param>
        /// <param name="borderIsAlreadyPainted">True if the border should have already been painted.</param>
        [Theory]
        [InlineData(false, false, false)]
        [InlineData(false, false, true)]
        [InlineData(false, true, false)]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        [InlineData(true, true, true)]
        public void PaintBorderIfRequired_BorderNotRequired_NothingWrittenToConsole(bool hasBorder, bool painterIsNull, bool borderIsAlreadyPainted)
        {
            // Arrange
            var (mockConsole, borderPainter) = Arrange(new ConsoleSize(10, 5), hasBorder, painterIsNull, borderIsAlreadyPainted);

            // Act
            borderPainter.PaintBorderIfRequired();

            // Assert
            mockConsole.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Tests that the Reset method causes the border to be repainted the next time
        /// the PaintBorderIfRequired method is called.
        /// </summary>
        [Fact]
        public void Reset_ResetsBorderPaintedFlag()
        {
            // Arrange
            var (mockConsole, borderPainter) = Arrange(new ConsoleSize(10, 5), true, false, true);

            // Act
            borderPainter.Reset();

            // Assert - the border should be painted again after reset
            borderPainter.PaintBorderIfRequired();
            mockConsole.Verify(c => c.Write("╭", ExpectedConsoleOutputType), Times.Once);
            mockConsole.Verify(c => c.WriteLine("╮", ExpectedConsoleOutputType), Times.Once);
            mockConsole.Verify(c => c.Write("╰", ExpectedConsoleOutputType), Times.Once);
            mockConsole.Verify(c => c.WriteLine("╯", ExpectedConsoleOutputType), Times.Once);
        }

        /// <summary>
        /// Creates a mock console and a border painter, sets the inner size and whether a border is required,
        /// sets the border painter's painter, and clears all the invocations on the mock console which were
        /// performed during this method.
        /// </summary>
        /// <param name="innerSize">Inner size of the painter.</param>
        /// <param name="hasBorder">True if the painter should have a border.</param>
        /// <param name="painterIsNull">True if the border painter's painter should be null.</param>
        /// <param name="borderShouldAlreadyBePainted">True if the border should have already been painted.</param>
        /// <returns>The mock console and the border painter under test.</returns>
        private static (Mock<IConsole> MockConsole, BorderPainter BorderPainter) Arrange(ConsoleSize innerSize, bool hasBorder, bool painterIsNull, bool borderShouldAlreadyBePainted)
        {
            var mockConsole = new Mock<IConsole>();
            var borderPainter = new BorderPainter(mockConsole.Object);
            var testPainter = new TestPainter(mockConsole.Object, borderPainter)
            {
                InnerSize = innerSize,
                HasBorder = hasBorder,
            };
            borderPainter.Painter = painterIsNull ? null : testPainter;
            if (borderShouldAlreadyBePainted)
            {
                borderPainter.PaintBorderIfRequired();
            }

            mockConsole.Invocations.Clear();
            return (mockConsole, borderPainter);
        }
    }
}
