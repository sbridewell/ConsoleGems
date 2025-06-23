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

        /// <summary>
        /// Turns the player to the left.
        /// </summary>
        void TurnPlayerLeft();

        /// <summary>
        /// Turns the player to the right.
        /// </summary>
        void TurnPlayerRight();

        /// <summary>
        /// Attempts to move the player forward.
        /// The player can only move forward if there isn't a wall in the way.
        /// </summary>
        void TryToMovePlayerForward();

        /// <summary>
        /// Quits the game.
        /// </summary>
        void Quit();
    }
}
