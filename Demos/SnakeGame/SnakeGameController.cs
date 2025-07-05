// <copyright file="SnakeGameController.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.SnakeGame
{
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Text;

    /// <summary>
    /// The controlling module for the snake game.
    /// </summary>
    public class SnakeGameController(IConsole console)
        : ISnakeGameController
    {
        /// <inheritdoc/>
        public void Play()
        {
            var snake = new Snake();
            var gameOver = false;

            // TODO: start off in the middle of the playing surface
            snake.Segments.Enqueue(new ConsolePoint(10, 10));

            // TODO: randomise the initial direction
            snake.CurrentDirection = Direction.Up;

            // TODO: paint the border around the playing surface
            while (!gameOver)
            {
                // TODO: paint the snake
                // TODO: add food to the playing surface at a random position
                // TODO: read the key input from the user in a non-blocking way
                // TODO: change the snake's direction if the user has pressed a key
                // TODO: move the snake in the current direction by pushing a new segment onto the front of the queue
                // TODO: if the snake has eaten food, increase its length and add a new food item to the playing surface
                // TODO: if the snake has collided with itself or the border, set gameOver to true

                // This is just a placeholder to allow the loop to exit for unit testing purposes.
                console.WriteLine("Iteration 1");
                gameOver = true; // Remove this line when implementing the game logic.
            }

            // TODO: StatusPainter (reuse from MazeGame?)
            console.WriteLine($"Game Over! Your snake was {snake.Length} segments long.", ConsoleOutputType.Default);
        }
    }
}
