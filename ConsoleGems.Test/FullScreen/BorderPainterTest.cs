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
        public void PaintTopBorderIfRequired_BorderIsRequired_WritesToConsoleCorrectly(int innerWidth)
        {
            // Arrange
            var (mockConsole, borderPainter) = Arrange(new ConsoleSize(innerWidth, 5), true, false, false);

            // Act
            borderPainter.PaintTopBorderIfRequired();

            // Assert
            mockConsole.Verify(c => c.Write("╭", ExpectedConsoleOutputType), Times.Once);
            mockConsole.Verify(c => c.Write(new string('─', innerWidth), ExpectedConsoleOutputType), Times.Once);
            mockConsole.Verify(c => c.Write("╮", ExpectedConsoleOutputType), Times.Once);
        }

        /// <summary>
        /// Tests that the PaintSideBorderIfRequired writes the correct output to the console.
        /// </summary>
        [Fact]
        public void PaintSideBorderIfRequired_BorderIsRequired_WritesToConsoleCorrectly()
        {
            // Arrange
            var (mockConsole, borderPainter) = Arrange(new ConsoleSize(5, 5), true, false, false);

            // Act
            borderPainter.PaintSideBorderIfRequired();

            // Assert
            mockConsole.Verify(c => c.Write("│", ExpectedConsoleOutputType), Times.Once);
        }

        /// <summary>
        /// Tests that the PaintBottomBorderIfRequired writes the correct output to the console.
        /// </summary>
        /// <param name="innerWidth">Width of the painter excluding the border.</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void PaintBottomBorderIfRequired_BorderIsRequired_WritesToConsoleCorrectly(int innerWidth)
        {
            // Arrange
            var (mockConsole, borderPainter) = Arrange(new ConsoleSize(innerWidth, 5), true, false, false);

            // Act
            borderPainter.PaintBottomBorderIfRequired();

            // Assert
            mockConsole.Verify(c => c.Write("╰", ExpectedConsoleOutputType), Times.Once);
            mockConsole.Verify(c => c.Write(new string('─', innerWidth), ExpectedConsoleOutputType), Times.Once);
            mockConsole.Verify(c => c.Write("╯", ExpectedConsoleOutputType), Times.Once);
        }

        /// <summary>
        /// Tests that nothing is written to the console by the PaintTopBorderIfRequired
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
        public void PaintTopBorderIfRequired_BorderNotRequired_NothingWrittenToConsole(bool hasBorder, bool painterIsNull, bool borderIsAlreadyPainted)
        {
            // Arrange
            var (mockConsole, borderPainter) = Arrange(new ConsoleSize(10, 5), hasBorder, painterIsNull, borderIsAlreadyPainted);

            // Act
            borderPainter.PaintTopBorderIfRequired();

            // Assert
            mockConsole.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Tests that nothing is written to the console by the PaintSideBorderIfRequired
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
        public void PaintSideBorderIfRequired_BorderNotRequired_NothingWrittenToConsole(bool hasBorder, bool painterIsNull, bool borderIsAlreadyPainted)
        {
            // Arrange
            var (mockConsole, borderPainter) = Arrange(new ConsoleSize(10, 5), hasBorder, painterIsNull, borderIsAlreadyPainted);

            // Act
            borderPainter.PaintSideBorderIfRequired();

            // Assert
            mockConsole.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Tests that nothing is written to the console by the PaintBottomBorderIfRequired
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
        public void PaintBottomBorderIfRequired_BorderNotRequired_NothingWrittenToConsole(bool hasBorder, bool painterIsNull, bool borderIsAlreadyPainted)
        {
            // Arrange
            var (mockConsole, borderPainter) = Arrange(new ConsoleSize(10, 5), hasBorder, painterIsNull, borderIsAlreadyPainted);

            // Act
            borderPainter.PaintBottomBorderIfRequired();

            // Assert
            mockConsole.VerifyNoOtherCalls();
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
            var testPainter = new TestPainter(mockConsole.Object, borderPainter);
            testPainter.InnerSize = innerSize;
            testPainter.HasBorder = hasBorder;
            borderPainter.Painter = painterIsNull ? null : testPainter;
            if (borderShouldAlreadyBePainted)
            {
                borderPainter.PaintBottomBorderIfRequired();
            }

            mockConsole.Invocations.Clear();
            return (mockConsole, borderPainter);
        }
    }
}
