// <copyright file="ISectionRenderer.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.Pov
{
    using Sde.ConsoleGems.Consoles;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Interface for rendering a section of the player's view of a maze.
    /// </summary>
    public interface ISectionRenderer
    {
        /// <summary>
        /// Renders a section, both on the left and right of the screen.
        /// </summary>
        /// <param name="painter">
        /// The painter which will write the section to the screen buffer.
        /// </param>
        /// <param name="sectionNumber">The zero-based section number.</param>
        /// <param name="forwardView">Represents the part of the maze that the player can see.</param>
        public void RenderSection(IMazePainterPov painter, int sectionNumber, ForwardView forwardView, Direction playerFacingDirection);

        /// <summary>
        /// Renders a section of the maze which contains only a perpendicular wall.
        /// </summary>
        /// <param name="painter">
        /// The painter which will write the section to the screen buffer.
        /// </param>
        /// <param name="sectionNumber">Zero-based section number.</param>
        /// <param name="leftOrRight">Whether the section is in the left or right half of the screen.</param>
        /// <param name="forwardDistance">How far away from the player the wall is.</param>
        public void RenderSectionAllWall(
            IMazePainterPov painter,
            int sectionNumber,
            LeftOrRight leftOrRight,
            int forwardDistance,
            Direction playerFacingDirection);

        /// <summary>
        /// Renders a section of the maze which is too far away to see, i.e. there is no wall in that section.
        /// </summary>
        /// <param name="painter">
        /// The painter which will write the section to the screen buffer.
        /// </param>
        /// <param name="leftOrRight">Whether the section is in the left or right half of the screen.</param>
        public void RenderSectionTooFar(IMazePainterPov painter, LeftOrRight leftOrRight);
    }
}
