// <copyright file="MazePainterMapTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test.Painters.Map
{
    using FluentAssertions;
    using Moq;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.FullScreen;
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.CharacterProviders;
    using Sde.MazeGame.Models;
    using Sde.MazeGame.Painters.Map;
    using Xunit.Abstractions;

    /// <summary>
    /// Unit tests for the <see cref="MazePainterMap"/> class.
    /// </summary>
    public class MazePainterMapTest
    {
        private readonly ITestOutputHelper output;
        private readonly MazeFactory factory = new ();
        private readonly Maze maze;
        private readonly Player player = new () { Position = new ConsolePoint(5, 5) };
        private readonly Mock<IConsole> mockConsole = new ();
        private readonly Mock<IBorderPainter> mockBorderPainter = new ();
        private readonly IWallCharacterProvider wallCharacterProvider = new LinesWallCharacterProvider();
        private readonly Mock<IPlayerCharacterProvider> mockPlayerCharacterProvider = new ();
        private readonly MazePainterMapProxy painter;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazePainterMapTest"/> class.
        /// </summary>
        /// <param name="output">For writing to the test output window.</param>
        public MazePainterMapTest(ITestOutputHelper output)
        {
            this.output = output;
            this.mockPlayerCharacterProvider.Setup(m => m.GetPlayerChar(It.IsAny<Player>())).Returns('P');
            this.painter = new MazePainterMapProxy(
                this.mockConsole.Object,
                this.mockBorderPainter.Object,
                this.wallCharacterProvider,
                this.mockPlayerCharacterProvider.Object)
            {
                InnerSize = new ConsoleSize(10, 10),
            };
            this.maze = this.factory.CreateFromFile(Path.Combine("MazeData", "10x10.maze.txt"));
        }

        /// <summary>
        /// Tests that the Paint method paints a fully explored maze correctly.
        /// </summary>
        [Fact]
        public void Paint_FullyExploredMaze_PaintsMazeCorrectly()
        {
            // Arrange
            this.ExploreMaze();
            var expectedScreenBuffer = new string[]
            {
                "╶────────╮",
                "♥        │",
                "╭──────╴ │",
                "│        │",
                "│ ╭─┬┬───┤",
                "│ │ ╰P   │",
                "│ │    ╷ │",
                "│ ╰────╯ │",
                "│        │",
                "╰────────╯",
            };

            // Act
            this.painter.Paint(this.maze, this.player);

            // Assert
            var screenBufferValue = this.painter.PublicScreenBuffer.ToStringArray();
            foreach (var line in screenBufferValue)
            {
                this.output.WriteLine(line);
            }

            this.painter.PublicScreenBuffer.ToStringArray().Should().BeEquivalentTo(expectedScreenBuffer);
        }

        /// <summary>
        /// Tests that the Paint method paints an unexplored maze correctly.
        /// </summary>
        [Fact]
        public void Paint_UnexploredMaze_PaintsMazeCorrectly()
        {
            // Arrange
            var expectedScreenBuffer = new string[]
            {
                "░░░░░░░░░░",
                "░░░░░░░░░░",
                "░░░░░░░░░░",
                "░░░░░░░░░░",
                "░░░░░P░░░░",
                "░░░░░░░░░░",
                "░░░░░░░░░░",
                "░░░░░░░░░░",
                "░░░░░░░░░░",
                "░░░░░░░░░░",
            };

            // Act
            this.painter.Paint(this.maze, this.player);

            // Assert
            var screenBufferValue = this.painter.PublicScreenBuffer.ToStringArray();
            foreach (var line in screenBufferValue)
            {
                this.output.WriteLine(line);
            }

            this.painter.PublicScreenBuffer.ToStringArray().Should().BeEquivalentTo(expectedScreenBuffer);
        }

        /// <summary>
        /// Tests that the ErasePlayer method erases the player from the screen buffer.
        /// </summary>
        [Fact]
        public void ErasePlayer_ErasesPlayerFromScreenBuffer()
        {
            // Arrange
            var expectedScreenBuffer = new string[]
            {
                "░░░░░░░░░░",
                "░░░░░░░░░░",
                "░░░░░░░░░░",
                "░░░░░░░░░░",
                "░░░░░ ░░░░",
                "░░░░░░░░░░",
                "░░░░░░░░░░",
                "░░░░░░░░░░",
                "░░░░░░░░░░",
                "░░░░░░░░░░",
            };
            this.painter.Paint(this.maze, this.player);

            // Act
            this.painter.ErasePlayer(this.player);

            // Assert
            var screenBufferValue = this.painter.PublicScreenBuffer.ToStringArray();
            foreach (var line in screenBufferValue)
            {
                this.output.WriteLine(line);
            }

            this.painter.PublicScreenBuffer.ToStringArray().Should().BeEquivalentTo(expectedScreenBuffer);
        }

        private void ExploreMaze()
        {
            for (var y = 0; y < this.maze.Height; y++)
            {
                for (var x = 0; x < this.maze.Width; x++)
                {
                    this.maze.GetMazePoint(x, y).Explored = true;
                }
            }
        }
    }
}
