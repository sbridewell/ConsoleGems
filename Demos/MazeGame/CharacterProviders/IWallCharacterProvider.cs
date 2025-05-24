// <copyright file="IWallCharacterProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.CharacterProviders
{
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Interface for determining the character to use for a wall
    /// in a two-dimensional representation of a maze.
    /// </summary>
    public interface IWallCharacterProvider
    {
        /// <summary>
        /// Gets the character to use for a wall at the specified position.
        /// </summary>
        /// <param name="maze">The maze being painted.</param>
        /// <param name="position">The position of the wall within the maze.</param>
        /// <returns>A character to represent the wall.</returns>
        public char GetWallChar(Maze maze, ConsolePoint position);
    }
}
