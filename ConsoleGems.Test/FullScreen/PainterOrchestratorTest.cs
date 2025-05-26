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
        private record OverlappingPaintersTestCase(List<IPainter> painters);

        private record WindowTooSmallTestCase(List<ConsoleRectangle> painterRectangles, Mock<IConsole> mockConsole, ConsoleSize initialWindowSize);

        private static readonly Mock<IConsole> MockConsole = new Mock<IConsole>();

        private static readonly Mock<IBorderPainter> MockBorderPainter = new Mock<IBorderPainter>();

        private static readonly Dictionary<string, OverlappingPaintersTestCase> OverlappingPaintersTestData = new ()
        {
            ["Two overlapping painters"] = new OverlappingPaintersTestCase(
                new List<IPainter>
                {
                    new TestPainter(MockConsole.Object, MockBorderPainter.Object)
                    {
                        Origin = new ConsolePoint(1, 1),
                        InnerSize = new ConsoleSize(2, 2),
                        HasBorder = false,
                    },
                    new TestPainter(MockConsole.Object, MockBorderPainter.Object)
                    {
                        Origin = new ConsolePoint(2, 2),
                        InnerSize = new ConsoleSize(2, 2),
                        HasBorder = false,
                    },
                }),
            ["Two overlapping painters, both with borders"] = new OverlappingPaintersTestCase(
                new List<IPainter>
                {
                    new TestPainter(MockConsole.Object, MockBorderPainter.Object)
                    {
                        Origin = new ConsolePoint(1, 1),
                        InnerSize = new ConsoleSize(2, 2),
                        HasBorder = true,
                    },
                    new TestPainter(MockConsole.Object, MockBorderPainter.Object)
                    {
                        Origin = new ConsolePoint(3, 3),
                        InnerSize = new ConsoleSize(2, 2),
                        HasBorder = true,
                    },
                }),
            ["Two overlapping painters, one with border"] = new OverlappingPaintersTestCase(
                new List<IPainter>
                {
                    new TestPainter(MockConsole.Object, MockBorderPainter.Object)
                    {
                        Origin = new ConsolePoint(1, 1),
                        InnerSize = new ConsoleSize(2, 2),
                        HasBorder = true,
                    },
                    new TestPainter(MockConsole.Object, MockBorderPainter.Object)
                    {
                        Origin = new ConsolePoint(2, 2),
                        InnerSize = new ConsoleSize(2, 2),
                        HasBorder = false,
                    },
                }),
            ["Three painters, two overlap and one doesn't"] = new OverlappingPaintersTestCase(
                new List<IPainter>
                {
                    new TestPainter(MockConsole.Object, MockBorderPainter.Object)
                    {
                        Origin = new ConsolePoint(1, 1),
                        InnerSize = new ConsoleSize(2, 2),
                        HasBorder = false,
                    },
                    new TestPainter(MockConsole.Object, MockBorderPainter.Object)
                    {
                        Origin = new ConsolePoint(2, 2),
                        InnerSize = new ConsoleSize(2, 2),
                        HasBorder = false,
                    },
                    new TestPainter(MockConsole.Object, MockBorderPainter.Object)
                    {
                        Origin = new ConsolePoint(5, 3),
                        InnerSize = new ConsoleSize(2, 2),
                        HasBorder = false,
                    },
                }),
        };

        private static readonly Dictionary<string, WindowTooSmallTestCase> WindowTooSmallTestData = new ()
        {
            ["Window not wide enough"] = new WindowTooSmallTestCase(
                new List<ConsoleRectangle>
                {
                    new ConsoleRectangle(new ConsolePoint(0, 0), new ConsoleSize(2, 2)),
                    new ConsoleRectangle(new ConsolePoint(2, 0), new ConsoleSize(2, 2)),
                },
                new Mock<IConsole>(),
                new ConsoleSize(3, 2)),
            ["Window not tall enough"] = new WindowTooSmallTestCase(
                new List<ConsoleRectangle>
                {
                    new ConsoleRectangle(new ConsolePoint(1, 1), new ConsoleSize(2, 2)),
                    new ConsoleRectangle(new ConsolePoint(3, 3), new ConsoleSize(2, 2)),
                },
                new Mock<IConsole>(),
                new ConsoleSize(5, 4)),
        };

        /// <summary>
        /// Gets the names of the test cases for overlapping painters.
        /// </summary>
        public static TheoryData<string> OverlappingPainterTestCaseNames
            => new (OverlappingPaintersTestData.Keys);

        /// <summary>
        /// Gets the names of the test cases for a window too small to contain its painters.
        /// </summary>
        public static TheoryData<string> WindowTooSmallTestCaseNames
            => new (WindowTooSmallTestData.Keys);

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
            var orchestrator = new PainterOrchestrator(console);
            var painter1 = new TestPainter(console, MockBorderPainter.Object)
            {
                Origin = new ConsolePoint(1, 1),
                InnerSize = new ConsoleSize(5, 5),
                HasBorder = hasBorder,
            };
            var painter1Chars = new char[][]
            {
                ['1', '2', '3', '4', '5'],
                ['h', 'e', 'l', 'l', 'o'],
                ['w', 'o', 'r', 'l', 'd'],
                ['A', 'B', 'C', 'D', 'E'],
                ['6', '7', '8', '9', '0'],
            };
            WriteToScreenBuffer(painter1Chars, painter1);
            var painter2 = new TestPainter(console, MockBorderPainter.Object)
            {
                Origin = new ConsolePoint(10, 0),
                InnerSize = new ConsoleSize(2, 2),
                HasBorder = hasBorder,
            };
            var painter2Chars = new char[][]
            {
                ['1', '2'],
                ['3', '4'],
            };
            WriteToScreenBuffer(painter2Chars, painter2);
            orchestrator.Painters.Add(painter1);
            orchestrator.Painters.Add(painter2);

            // Act
            orchestrator.Paint();

            // Assert
            console.Flush();
            true.Should().Be(true);
        }

        /// <summary>
        /// Tests that the correct error is thrown if any of the painters overlap.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(OverlappingPainterTestCaseNames))]
        public void OverlappingPainters_Throws(string testCaseName)
        {
            lock (LockObjects.ConsoleLock)
            {
                // Arrange
                var testCase = OverlappingPaintersTestData[testCaseName];
                var mockConsole = new Mock<IConsole>();
                mockConsole.Setup(m => m.WindowHeight).Returns(10);
                mockConsole.Setup(m => m.WindowWidth).Returns(10);
                var orchestrator = new PainterOrchestrator(mockConsole.Object);
                orchestrator.Painters.AddRange(testCase.painters);

                // Act
                var action = () => orchestrator.Paint();

                // Assert
                var ex = action.Should().ThrowExactly<InvalidOperationException>().Which;
                var expectedMsg = "Painters 'Origin: *, InnerSize: *, HasBorder: *' and 'Origin: *, InnerSize: *, HasBorder: *' overlap.";
                ex.Message.Should().Match(expectedMsg);
            }
        }

        /// <summary>
        /// Tests that the correct message is displayed when the console window is too
        /// small to display the painters.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(WindowTooSmallTestCaseNames))]
        public void WindowTooSmall_DisplaysCorrectMessage(string testCaseName)
        {
            // Arrange
            var testCase = WindowTooSmallTestData[testCaseName];
            testCase.mockConsole.SetupSequence(m => m.WindowHeight)
                .Returns(testCase.initialWindowSize.Height)
                .Returns(testCase.painterRectangles.Max(p => p.Bottom + 1));
            testCase.mockConsole.SetupSequence(m => m.WindowWidth)
                .Returns(testCase.initialWindowSize.Width)
                .Returns(testCase.painterRectangles.Max(p => p.Right + 1));
            var orchestrator = new PainterOrchestrator(testCase.mockConsole.Object);
            foreach (var rect in testCase.painterRectangles)
            {
                orchestrator.Painters.Add(new TestPainter(testCase.mockConsole.Object, MockBorderPainter.Object)
                {
                    Origin = rect.Origin,
                    InnerSize = rect.Size,
                    HasBorder = false,
                });
            }

            // Act
            orchestrator.Paint();

            // Assert
            testCase.mockConsole.Verify(m => m.Write(It.IsRegex("Please resize the console window"), ConsoleOutputType.Error), Times.Once);
            testCase.mockConsole.Verify(m => m.Write(It.IsRegex("Current window size is"), ConsoleOutputType.Error), Times.Once);
        }

        private static void WriteToScreenBuffer(char[][] painterChars, TestPainter painter)
        {
            for (var y = 0; y < painterChars.Length; y++)
            {
                for (var x = 0; x < painterChars[y].Length; x++)
                {
                    painter.PublicWriteToScreenBuffer(x, y, painterChars[y][x], ConsoleOutputType.Default);
                }
            }
        }
    }
}
