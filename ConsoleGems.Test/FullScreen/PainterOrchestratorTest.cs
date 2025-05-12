// <copyright file="PainterOrchestratorTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.FullScreen
{
    using Xunit.Abstractions;

    /// <summary>
    /// Unit tests for the <see cref="PainterOrchestrator"/> class.
    /// </summary>
    public class PainterOrchestratorTest(ITestOutputHelper output)
    {
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
            var console = new TestOutputHelperConsole(output, new ConsoleSize(20, 20));
            console.Clear();
            var orchestrator = new PainterOrchestrator();
            var painter1 = new TestPainter(console, new ConsolePoint(1, 1), new ConsoleSize(5, 5), hasBorder);
            painter1.PublicWriteToScreenBuffer(0, "12345");
            painter1.PublicWriteToScreenBuffer(1, "hello");
            painter1.PublicWriteToScreenBuffer(2, "world");
            painter1.PublicWriteToScreenBuffer(3, "ABCDE");
            painter1.PublicWriteToScreenBuffer(4, "67890");
            var painter2 = new TestPainter(console, new ConsolePoint(10, 0), new ConsoleSize(2, 2), hasBorder);
            painter2.PublicWriteToScreenBuffer(0, "12");
            painter2.PublicWriteToScreenBuffer(1, "34");
            orchestrator.Painters.Add(painter1);
            orchestrator.Painters.Add(painter2);

            // Act
            orchestrator.Paint();

            // Assert
            console.Flush();
            true.Should().Be(true);
        }
    }
}
