// <copyright file="Game.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.SnakeGame
{
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Painters;
    using Sde.ConsoleGems.Text;

    /// <summary>
    /// Represents a single instance of the snake game.
    /// </summary>
    public class Game(
        IConsole console,
        IStatusPainter statusPainter,
        ISnakeGamePainter snakeGamePainter,
        ISnake snake,
        ISnakeGameRandomiser randomiser)
        : IGame
    {
        private ConsoleRectangle playingSurface;
        private ConsolePoint foodPosition;

        /// <summary>
        /// Gets the width of the playing surface.
        /// </summary>
        public static int PlayingSurfaceWidth => 30;

        /// <summary>
        /// Gets the height of the playing surface.
        /// </summary>
        public static int PlayingSurfaceHeight => 15;

        /// <summary>
        /// Gets the snake controlled by the player.
        /// </summary>
        public ISnake Snake { get; private set; } = snake;

        /// <summary>
        /// Gets a value indicating whether the game is over.
        /// </summary>
        public bool GameOver { get; private set; }

        /// <summary>
        /// Gets the player's score.
        /// </summary>
        public int Score { get; private set; }

        /// <inheritdoc/>
        public void Initialise()
        {
            console.Clear();
            this.GameOver = false;
            console.CursorVisible = false;
            this.Snake.Initialise(new ConsolePoint(
                Game.PlayingSurfaceWidth / 2,
                Game.PlayingSurfaceHeight / 2));
            this.playingSurface = new ConsoleRectangle(
                new ConsolePoint(0, 0),
                new ConsoleSize(Game.PlayingSurfaceWidth, Game.PlayingSurfaceHeight));
            statusPainter.Reset();
            statusPainter.InnerSize = new ConsoleSize(console.WindowWidth - 2, 1);
            statusPainter.HasBorder = true;
            snakeGamePainter.Reset();
            snakeGamePainter.Origin = new ConsolePoint(0, 3);
            snakeGamePainter.InnerSize = new ConsoleSize(Game.PlayingSurfaceWidth, Game.PlayingSurfaceHeight);
            snakeGamePainter.HasBorder = true;
            this.Score = 0;

            // TODO: randomise the initial direction
            this.Snake.ChangeDirection(Direction.Up);
            this.AddFood();
        }

        /// <inheritdoc/>
        public void Iterate()
        {
            statusPainter.Paint(
                $"Use arrow keys to control the snake, Q to quit. Score: {this.Score}",
                ConsoleOutputType.Default);
            this.GameOver = this.CheckForAndActOnUserInput();
            this.Snake.MoveForward();
            if (this.Snake.HeadPosition == this.foodPosition)
            {
                this.AddFood();
            }
            else
            {
                snakeGamePainter.WriteToScreenBuffer(this.Snake.TailPosition, ' ', ConsoleOutputType.Default);
                this.Snake.TrimTail(); // Remove the tail segment if food not eaten
            }

            if (!this.Snake.IsWithin(this.playingSurface) ||
                this.Snake.HasRunIntoOwnTail)
            {
                this.GameOver = true;
            }
            else
            {
                this.Score += this.Snake.Length;
                snakeGamePainter.WriteToScreenBuffer(this.Snake.HeadPosition, 'O', ConsoleOutputType.Default);
                snakeGamePainter.Paint();
                Thread.Sleep(100); // Control the game speed
            }
        }

        /// <inheritdoc/>
        public void TearDown()
        {
            statusPainter.Paint($"Game over! You scored {this.Score}. Press space to exit.", ConsoleOutputType.Error);
            while (console.ReadKey().KeyChar != ' ')
            {
                // Ensure we don't exit prematurely on a different keypress
            }

            console.Clear();
            console.CursorVisible = true;
        }

        private void AddFood()
        {
            this.foodPosition = randomiser.GetFoodPosition(this);
            snakeGamePainter.WriteToScreenBuffer(this.foodPosition, '♥', ConsoleOutputType.Green);
        }

        private bool CheckForAndActOnUserInput()
        {
            var gameOver = false;
            if (console.KeyAvailable)
            {
                var keyInfo = console.ReadKey(intercept: true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        this.Snake.ChangeDirection(Direction.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        this.Snake.ChangeDirection(Direction.Down);
                        break;
                    case ConsoleKey.LeftArrow:
                        this.Snake.ChangeDirection(Direction.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        this.Snake.ChangeDirection(Direction.Right);
                        break;
                    case ConsoleKey.Q:
                        gameOver = true;
                        break;
                }
            }

            return gameOver;
        }
    }
}
