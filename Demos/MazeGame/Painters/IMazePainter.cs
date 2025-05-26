// <copyright file="IMazePainter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters
{
    using Sde.ConsoleGems.FullScreen;
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.Models;

    // TODO: contents of console window are mangled when window is moved from one screen to another
    // TODO: implement IPainter

    /// <summary>
    /// Interface for painting a maze to the console window.
    /// </summary>
    public interface IMazePainter : IPainter
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
