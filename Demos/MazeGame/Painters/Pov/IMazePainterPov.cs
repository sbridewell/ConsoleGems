// <copyright file="IMazePainterPov.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.Pov
{
    using Sde.ConsoleGems.FullScreen;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Interface for a class which writes a representation of a maze to the console,
    /// from the player's point of view.
    /// </summary>
    public interface IMazePainterPov : IPainter
    {
        /// <summary>
        /// Writes the maze and player to the console.
        /// </summary>
        /// <param name="maze">The maze.</param>
        /// <param name="player">The player.</param>
        public void Render(Maze maze, Player player);

        /// <summary>
        /// Writes the supplied forward view to the screen buffer.
        /// </summary>
        /// <param name="forwardView">View of the maze from the player's point of view.</param>
        void RenderForwardView(ForwardView forwardView);
    }
}
