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
        /// Gets or sets the status of the game.
        /// </summary>
        public MazeGameStatus Status { get; set; } = MazeGameStatus.NotStarted;
    }
}
