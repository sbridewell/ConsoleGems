// <copyright file="IMazeGameRandomiser.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame
{
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Interface for generating random values for the maze game.
    /// </summary>
    public interface IMazeGameRandomiser
    {
        /// <summary>
        /// Gets a random compass direction.
        /// </summary>
        /// <returns>A random compass direction.</returns>
        public Direction GetDirection();

        /// <summary>
        /// Gets a random position within a maze, which isn't occupied by a wall.
        /// </summary>
        /// <param name="maze">The maze.</param>
        /// <returns>A random position in the maze.</returns>
        public ConsolePoint GetPosition(Maze maze);
    }
}
