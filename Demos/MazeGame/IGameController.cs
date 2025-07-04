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
        /// Initialises the game by loading a maze from the specified file.
        /// </summary>
        /// <param name="options">
        /// Options controlling the behaviour of the game.
        /// </param>
        void Initialise(MazeGameOptions options);

        /// <summary>
        /// Starts the game by loading a maze from the specified file and allowing the
        /// player to navigate through it.
        /// </summary>
        void Play();

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
