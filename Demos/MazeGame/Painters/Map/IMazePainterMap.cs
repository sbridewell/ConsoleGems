// <copyright file="IMazePainterMap.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.Map
{
    using Sde.ConsoleGems.FullScreen;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Interface for painting the map view of a maze to the console window.
    /// </summary>
    public interface IMazePainterMap : IPainter
    {
        /// <summary>
        /// Paints the supplied maze to the console window.
        /// </summary>
        /// <param name="maze">The maze to paint.</param>
        /// <param name="player">
        /// Represents the position and facing direction of the player.
        /// </param>
        public void Paint(Maze maze, Player player);

        /// <summary>
        /// Erases the player from the maze.
        /// </summary>
        /// <param name="player">The player.</param>
        public void ErasePlayer(Player player);

        /// <summary>
        /// Paints the player onto the maze.
        /// </summary>
        /// <param name="player">The player.</param>
        public void PaintPlayer(Player player);
    }
}
