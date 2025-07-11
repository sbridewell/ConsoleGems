// <copyright file="ISnakeGameRandomiser.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.SnakeGame
{
    using Sde.ConsoleGems.Text;

    /// <summary>
    /// Interface for generating random values for the snake game, such as food positions.
    /// </summary>
    public interface ISnakeGameRandomiser
    {
        /// <summary>
        /// Gets a random position to add some food to the game, which is not already occupied by the snake.
        /// </summary>
        /// <param name="game">The snake game instance.</param>
        /// <returns>The position for the food.</returns>
        ConsolePoint GetFoodPosition(IGame game);
    }
}
