// <copyright file="IColumnRenderer.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.Pov
{
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.FullScreen;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Interface for rendering a single column of a player's point of view in a maze game.
    /// </summary>
    public interface IColumnRenderer
    {
        /// <summary>
        /// Writes a single column of <see cref="ConsoleGems.FullScreen.ConsolePixel"/>s
        /// to the screen buffer.
        /// </summary>
        /// <param name="painter">
        /// The painter which will write the column to the screen buffer.
        /// </param>
        /// <param name="screenX">Horizontal coordinate of the column.</param>
        /// <param name="wallIsPerpendicular">
        /// True if the wall in this section is perpendicular to the player.
        /// </param>
        /// <param name="playerFacingDirection">The direction the player is facing.</param>
        public void RenderColumn(IMazePainterPov painter, int screenX, bool wallIsPerpendicular, Direction playerFacingDirection);

        /// <summary>
        /// Writes a single column of ceiling characters to the screen buffer.
        /// </summary>
        /// <param name="painter">
        /// The painter which will write the column to the screen buffer.
        /// </param>
        /// <param name="screenX">Horizontal coordinate of the column.</param>
        public void RenderCeilingColumn(IMazePainterPov painter, int screenX);

        /// <summary>
        /// Writes a single column of ceiling characters to the screen buffer.
        /// </summary>
        /// <param name="painter">
        /// The painter which will write the column to the screen buffer.
        /// </param>
        /// <param name="screenX">Horizontal coordinate of the column.</param>
        public void RenderFloorColumn(IMazePainterPov painter, int screenX);

        /// <summary>
        /// Writes a single column of <see cref="ConsolePixel"/>s to the screen buffer, representing a wall
        /// which is perpendicular to the direction that the player is facing.
        /// </summary>
        /// <param name="painter">
        /// The painter which will write the column to the screen buffer.
        /// </param>
        /// <param name="screenX">Horizonal coordinate of the column to render.</param>
        /// <param name="sectionIndent">Vertical indent where the wall meets the ceiling / wall.</param>
        /// <param name="wallChar">
        /// The character to represent the wall - this could be a solid block for a regular
        /// wall or a heart to represent the exit.
        /// </param>
        /// <param name="wallColour">
        /// The colour of the wall, represented as a <see cref="ConsoleOutputType"/>.
        /// </param>
        public void RenderPerpendicularWallColumn(
            IMazePainterPov painter,
            int screenX,
            int sectionIndent,
            char wallChar,
            ConsoleOutputType wallColour);
    }
}
