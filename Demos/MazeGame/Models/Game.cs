// <copyright file="Game.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Models
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Encapsulates the maze, the player and other entities which participate in the game.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Game(
        Maze maze,
        Player player)
    {
        /// <summary>
        /// Gets the maze that the game is being played in.
        /// </summary>
        public Player Player => player;

        /// <summary>
        /// Gets the maze for this game.
        /// </summary>
        public Maze Maze => maze;

        /// <summary>
        /// Gets or sets a value indicating whether the player has won the game.
        /// </summary>
        public bool PlayerHasWon { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the game should continue.
        /// // TODO: change this to something like QuitRequested?
        /// </summary>
        public bool ContinuePlaying { get; set; } = true;
    }
}
