// <copyright file="GameTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace SnakeGame.Test
{
    using FluentAssertions;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Moq;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Painters;
    using Sde.ConsoleGems.Text;
    using Sde.SnakeGame;

    /// <summary>
    /// Unit tests for the <see cref="Game"/> class.
    /// </summary>
    public class GameTest
    {
        private readonly Mock<IConsole> mockConsole = new ();
        private readonly Mock<IStatusPainter> mockStatusPainter = new ();
        private readonly Mock<ISnakeGamePainter> mockSnakeGamePainter = new ();
        private readonly Mock<ISnake> mockSnake = new ();
        private readonly Mock<ISnakeGameRandomiser> mockSnakeGameRandomiser = new ();

        /// <summary>
        /// Tests that the Initialise method initialises the game correctly.
        /// </summary>
        [Fact]
        public void Initialise_ResetsPropertiesAndSetsUpTheGame()
        {
            // Arrange
            var game = this.InstantiateGame();

            // Act
            game.Initialise();

            // Assert
            game.GameOver.Should().BeFalse();
            game.Score.Should().Be(0);
            this.mockConsole.Verify(c => c.Clear(), Times.Once);
            this.mockConsole.VerifySet(c => c.CursorVisible = false, Times.Once);
            this.mockStatusPainter.Verify(s => s.Reset(), Times.Once);
            this.mockStatusPainter.VerifySet(m => m.InnerSize = It.IsAny<ConsoleSize>(), Times.Once);
            this.mockStatusPainter.VerifySet(m => m.HasBorder = true, Times.Once);
            this.mockSnakeGamePainter.Verify(s => s.Reset(), Times.Once);
            this.mockSnakeGamePainter.VerifySet(m => m.InnerSize = It.IsAny<ConsoleSize>(), Times.Once);
            this.mockSnakeGamePainter.VerifySet(m => m.HasBorder = true, Times.Once);
        }

        /// <summary>
        /// Tests that when the Iterate method results into the snake moving onto food,
        /// the snake moves forward and grows, and new food is added to the game.
        /// </summary>
        [Fact]
        public void Iterate_SnakeMovesOntoFood_SnakeGrowsAndNewFoodIsAdded()
        {
            // Arrange
            var game = this.InstantiateGame();
            this.mockSnakeGameRandomiser.Setup(m => m.GetFoodPosition(It.IsAny<Game>())).Returns(new ConsolePoint(5, 4));
            this.mockSnake.Setup(m => m.HeadPosition).Returns(new ConsolePoint(5, 4));
            game.Initialise();

            // Act
            game.Iterate();

            // Assert
            this.mockStatusPainter.Verify(
                m => m.Paint(
                    It.Is<string>(s => s.Contains("Use arrow keys to control the snake")),
                    ConsoleOutputType.Default),
                Times.Once);
            this.mockSnake.Verify(s => s.MoveForward(), Times.Once);
            this.mockSnake.Verify(s => s.TrimTail(), Times.Never);
            this.mockSnakeGameRandomiser.Verify(m => m.GetFoodPosition(It.IsAny<Game>()), Times.Exactly(2));
        }

        /// <summary>
        /// Tests that when the Iterate method results in the sname not moving onto food,
        /// the snake moves forward without growing and no food is added to the game.
        /// </summary>
        [Fact]
        public void Iterate_SnakeDoesNotMoveOntoFood_SnakeMovesForwardWithoutGrowing()
        {
            // Arrange
            var game = this.InstantiateGame();
            this.mockSnakeGameRandomiser.Setup(m => m.GetFoodPosition(It.IsAny<Game>())).Returns(new ConsolePoint(1, 1));
            this.mockSnake.Setup(m => m.HeadPosition).Returns(new ConsolePoint(5, 4));
            game.Initialise();

            // Act
            game.Iterate();

            // Assert
            this.mockStatusPainter.Verify(
                m => m.Paint(
                    It.Is<string>(s => s.Contains("Use arrow keys to control the snake")),
                    ConsoleOutputType.Default),
                Times.Once);
            this.mockSnake.Verify(s => s.MoveForward(), Times.Once);
            this.mockSnake.Verify(s => s.TrimTail(), Times.Once);
            this.mockSnakeGameRandomiser.Verify(m => m.GetFoodPosition(It.IsAny<Game>()), Times.Once);
        }

        /// <summary>
        /// Tests that when the Iterate method results in the snake moving out of the game surface,
        /// the game is over.
        /// </summary>
        [Fact]
        public void Iterate_SnakeMovesOutOfGameSurface_GameOverIsTrue()
        {
            // Arrange
            var game = this.InstantiateGame();
            this.mockSnakeGameRandomiser.Setup(m => m.GetFoodPosition(It.IsAny<Game>())).Returns(new ConsolePoint(1, 1));
            var segments = new Queue<ConsolePoint>();
            segments.Enqueue(new ConsolePoint(5, 5)); // Snake starts at (5, 5)
            this.mockSnake.Setup(m => m.HeadPosition).Returns(new ConsolePoint(5, 0)); // Move snake out of bounds
            this.mockSnake.Setup(m => m.HasRunIntoOwnTail).Returns(false);
            this.mockSnake.Setup(m => m.IsWithin(It.IsAny<ConsoleRectangle>())).Returns(false);
            game.Initialise();

            // Act
            game.Iterate();

            // Assert
            game.GameOver.Should().BeTrue();
        }

        /// <summary>
        /// Tests that when the Iterate method results in the snake running into its own tail,
        /// the game is over.
        /// </summary>
        [Fact]
        public void Iterate_SnakeRunsIntoOwnTail_GameOverIsTrue()
        {
            // Arrange
            var game = this.InstantiateGame();
            this.mockSnakeGameRandomiser.Setup(m => m.GetFoodPosition(It.IsAny<Game>())).Returns(new ConsolePoint(1, 1));
            var segments = new Queue<ConsolePoint>();
            segments.Enqueue(new ConsolePoint(5, 5)); // Snake starts at (5, 5)
            segments.Enqueue(new ConsolePoint(5, 4)); // Add a segment to simulate the snake running into itself
            this.mockSnake.Setup(m => m.HeadPosition).Returns(new ConsolePoint(5, 4)); // Snake runs into its own tail
            this.mockSnake.Setup(m => m.HasRunIntoOwnTail).Returns(true);
            this.mockSnake.Setup(m => m.IsWithin(It.IsAny<ConsoleRectangle>())).Returns(true);
            game.Initialise();

            // Act
            game.Iterate();

            // Assert
            game.GameOver.Should().BeTrue();
        }

        /// <summary>
        /// Tests when the Iterate method does not result in the snake running into its own tail
        /// or moving out of the game surface, the game is not over.
        /// </summary>
        [Fact]
        public void Iterate_SnakeDoesNotMoveForwardAndIsStillWithinGameSurface_GameOverIsFalse()
        {
            // Arrange
            var game = this.InstantiateGame();
            this.mockSnakeGameRandomiser.Setup(m => m.GetFoodPosition(It.IsAny<Game>())).Returns(new ConsolePoint(1, 1));
            var segments = new Queue<ConsolePoint>();
            segments.Enqueue(new ConsolePoint(5, 5)); // Snake starts at (5, 5)
            this.mockSnake.Setup(m => m.HeadPosition).Returns(new ConsolePoint(5, 5)); // Snake does not move
            this.mockSnake.Setup(m => m.HasRunIntoOwnTail).Returns(false);
            this.mockSnake.Setup(m => m.IsWithin(It.IsAny<ConsoleRectangle>())).Returns(true);
            game.Initialise();

            // Act
            game.Iterate();

            // Assert
            game.GameOver.Should().BeFalse();
        }

        /// <summary>
        /// Tests that the Iterate method changes the snake's direction when an arrow key is pressed.
        /// </summary>
        /// <param name="key">The key which was pressed.</param>
        /// <param name="expectedDirection">The direction the snake should attempt to change to.</param>
        [Theory]
        [InlineData(ConsoleKey.UpArrow, Direction.Up)]
        [InlineData(ConsoleKey.DownArrow, Direction.Down)]
        [InlineData(ConsoleKey.LeftArrow, Direction.Left)]
        [InlineData(ConsoleKey.RightArrow, Direction.Right)]
        public void Iterate_ArrowKeyPressed_SnakeChangesDirection(ConsoleKey key, Direction expectedDirection)
        {
            // Arrange
            this.mockConsole.Setup(c => c.KeyAvailable).Returns(true);
            this.mockConsole.Setup(c => c.ReadKey(It.IsAny<bool>()))
                .Returns(new ConsoleKeyInfo(' ', key, false, false, false));
            var game = this.InstantiateGame();
            game.Initialise();

            // Act
            game.Iterate();

            // Assert
            this.mockSnake.Verify(s => s.ChangeDirection(expectedDirection), Times.AtLeastOnce);
        }

        /// <summary>
        /// Tests that the Iterate method sets GameOver to true when the 'Q' key is pressed.
        /// </summary>
        [Fact]
        public void Iterate_QKeyPressed_GameOverIsTrue()
        {
            // Arrange
            this.mockConsole.Setup(c => c.KeyAvailable).Returns(true);
            this.mockConsole.Setup(c => c.ReadKey(It.IsAny<bool>()))
                .Returns(new ConsoleKeyInfo('q', ConsoleKey.Q, false, false, false));
            var game = this.InstantiateGame();
            game.Initialise();

            // Act
            game.Iterate();

            // Assert
            game.GameOver.Should().BeTrue();
        }

        /// <summary>
        /// Tests that the TearDown method exits when the space key is pressed, but not
        /// when other keys are pressed.
        /// </summary>
        [Fact]
        public void TearDown_ExitsWhenSpaceIsPressed()
        {
            // Arrange
            var game = this.InstantiateGame();
            this.mockConsole.SetupSequence(
                c => c.ReadKey(It.IsAny<bool>()))
                .Returns(new ConsoleKeyInfo('a', ConsoleKey.A, false, false, false)) // First key is not space
                .Returns(new ConsoleKeyInfo(' ', ConsoleKey.Spacebar, false, false, false));

            // Act
            game.TearDown();

            // Assert
            this.mockConsole.Verify(c => c.ReadKey(It.IsAny<bool>()), Times.Exactly(2));
        }

        private Game InstantiateGame()
        {
            var game = new Game(
                this.mockConsole.Object,
                this.mockStatusPainter.Object,
                this.mockSnakeGamePainter.Object,
                this.mockSnake.Object,
                this.mockSnakeGameRandomiser.Object);
            return game;
        }
    }
}
