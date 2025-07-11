// <copyright file="IGame.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.SnakeGame
{
    /// <summary>
    /// Interface for a single instance of the snake game.
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Gets a value indicating whether the game is over.
        /// </summary>
        bool GameOver { get; }

        /// <summary>
        /// Gets the snake controlled by the player.
        /// </summary>
        ISnake Snake { get; }

        /// <summary>
        /// Initialises the game.
        /// </summary>
        void Initialise();

        /// <summary>
        /// Runs a single iteration of the game loop, updating the snake's position and checking for user input.
        /// </summary>
        void Iterate();

        /// <summary>
        /// Tidies things up once the game is over.
        /// </summary>
        void TearDown();
    }
}
