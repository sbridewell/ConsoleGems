// <copyright file="IGameController.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame
{
    /// <summary>
    /// Interface for the game controller that manages the game flow and interactions.
    /// </summary>
    public interface IGameController
    {
        /// <summary>
        /// Starts the game by loading a maze from the specified file and allowing the
        /// player to navigate through it.
        /// </summary>
        /// <param name="mazeFile">
        /// Path to a text file representing the maze.
        /// </param>
        void Play(string mazeFile);
    }
}
