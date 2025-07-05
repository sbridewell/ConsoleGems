// <copyright file="MazeGameStatus.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Models
{
    /// <summary>
    /// Enumeration of possible states of the maze game.
    /// </summary>
    public enum MazeGameStatus
    {
        /// <summary>
        /// The game has not yet started.
        /// </summary>
        NotStarted,

        /// <summary>
        /// The game is currently in progress.
        /// </summary>
        InProgress,

        /// <summary>
        /// The player has won the game.
        /// </summary>
        Won,

        /// <summary>
        /// The player has lost the game (including by quitting).
        /// </summary>
        Lost,
    }
}
