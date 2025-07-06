// <copyright file="SnakeGameController.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.SnakeGame
{
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Painters;
    using Sde.ConsoleGems.Text;

    /// <summary>
    /// The controlling module for the snake game.
    /// </summary>
    public class SnakeGameController(IConsole console, ISnakeGamePainter snakeGamePainter, IStatusPainter statusPainter)
        : ISnakeGameController
    {
        private readonly Random random = new ();
        private readonly Snake snake = new ();
        private readonly int playingSurfaceWidth = 30;
        private readonly int playingSurfaceHeight = 15;
        //private bool gameOver = false;
        private ConsolePoint foodPosition;

        /// <inheritdoc/>
        public void Play()
        {
            console.Clear();
            var gameOver = false;
            console.CursorVisible = false;
            this.snake.Initialise(new ConsolePoint(
                this.playingSurfaceWidth / 2,
                this.playingSurfaceHeight / 2));
            var playingSurface = new ConsoleRectangle(
                new ConsolePoint(0, 0),
                new ConsoleSize(this.playingSurfaceWidth, this.playingSurfaceHeight));
            statusPainter.Reset();
            statusPainter.InnerSize = new ConsoleSize(console.WindowWidth - 2, 1);
            statusPainter.HasBorder = true;
            snakeGamePainter.Reset();
            snakeGamePainter.Origin = new ConsolePoint(0, 3);
            snakeGamePainter.InnerSize = new ConsoleSize(this.playingSurfaceWidth, this.playingSurfaceHeight);
            snakeGamePainter.HasBorder = true;
            var score = 0;

            // TODO: randomise the initial direction
            this.snake.CurrentDirection = Direction.Up;
            this.AddFood();

            while (!gameOver)
            {
                statusPainter.Paint($"Use arrow keys to control the snake, Q to quit. Score: {score}", ConsoleOutputType.Default);
                gameOver = this.CheckForAndActOnUserInput();
                this.snake.MoveForward();
                if (this.snake.Position == this.foodPosition)
                {
                    this.AddFood();
                }
                else
                {
                    snakeGamePainter.WriteToScreenBuffer(this.snake.Segments.First(), ' ', ConsoleOutputType.Default);
                    this.snake.Segments.Dequeue(); // Remove the tail segment if food not eaten
                }

                if (!this.snake.IsWithin(playingSurface) ||
                    this.snake.HasRunIntoOwnTail)
                {
                    gameOver = true;
                }
                else
                {
                    score += this.snake.Length;
                    snakeGamePainter.WriteToScreenBuffer(this.snake.Position, 'O', ConsoleOutputType.Default);
                    snakeGamePainter.Paint();
                    Thread.Sleep(100); // Control the game speed
                }
            }

            statusPainter.Paint($"Game over! You scored {score}. Press space to exit.", ConsoleOutputType.Error);
            while (console.ReadKey().KeyChar != ' ')
            {
                // Ensure we don't exit prematurely on a different keypress
            }

            console.Clear();
            console.CursorVisible = true;
        }

        private void AddFood()
        {
            do
            {
                this.foodPosition = this.GetCandidateFoodPosition();
            }
            while (this.snake.OccupiesPoint(this.foodPosition));
            snakeGamePainter.WriteToScreenBuffer(this.foodPosition, '♥', ConsoleOutputType.Green);
        }

        private ConsolePoint GetCandidateFoodPosition()
        {
            return new ConsolePoint(
                this.random.Next(1, this.playingSurfaceWidth - 1),
                this.random.Next(1, this.playingSurfaceHeight - 1));
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
                        this.snake.ChangeDirection(Direction.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        this.snake.ChangeDirection(Direction.Down);
                        break;
                    case ConsoleKey.LeftArrow:
                        this.snake.ChangeDirection(Direction.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        this.snake.ChangeDirection(Direction.Right);
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
