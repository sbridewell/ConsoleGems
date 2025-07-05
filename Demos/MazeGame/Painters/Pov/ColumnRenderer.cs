// <copyright file="ColumnRenderer.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.Pov
{
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.FullScreen;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Class for rendering a single column of the player's point of view in a maze game.
    /// </summary>
    public class ColumnRenderer : IColumnRenderer
    {
        private readonly int screenWidth = MazePainterPovConstants.PainterInnerWidth;
        private readonly int screenHeight = MazePainterPovConstants.PainterInnerHeight;
        private readonly char ceilingChar = MazePainterPovConstants.CeilingChar;
        private readonly char floorChar = MazePainterPovConstants.FloorChar;
        private readonly char perpendicularWallChar = MazePainterPovConstants.PerpendicularWallChar;
        private readonly char parallelWallChar = MazePainterPovConstants.ParallelWallChar;

        /// <inheritdoc/>
        public void RenderColumn(IMazePainterPov painter, int screenX, bool wallIsPerpendicular, Direction playerFacingDirection)
        {
            this.RenderCeilingColumn(painter, screenX);
            this.RenderFloorColumn(painter, screenX);
            if (wallIsPerpendicular)
            {
                var wallColour = playerFacingDirection switch
                {
                    Direction.North => MazePainterPovConstants.NorthColour,
                    Direction.South => MazePainterPovConstants.SouthColour,
                    Direction.East => MazePainterPovConstants.EastColour,
                    Direction.West => MazePainterPovConstants.WestColour,
                    _ => ConsoleOutputType.Default,
                };
                this.RenderPerpendicularWallColumn(painter, screenX, wallColour);
            }
            else
            {
                ConsoleOutputType wallColour;
                if (screenX < this.screenWidth / 2)
                {
                    // It's a wall to the left
                    wallColour = playerFacingDirection switch
                    {
                        Direction.North => MazePainterPovConstants.WestColour,
                        Direction.South => MazePainterPovConstants.EastColour,
                        Direction.East => MazePainterPovConstants.NorthColour,
                        Direction.West => MazePainterPovConstants.SouthColour,
                        _ => ConsoleOutputType.Default,
                    };
                }
                else
                {
                    // It's a wall to the right
                    wallColour = playerFacingDirection switch
                    {
                        Direction.North => MazePainterPovConstants.EastColour,
                        Direction.South => MazePainterPovConstants.WestColour,
                        Direction.East => MazePainterPovConstants.SouthColour,
                        Direction.West => MazePainterPovConstants.NorthColour,
                        _ => ConsoleOutputType.Default,
                    };
                }

                this.RenderParallelWallColumn(painter, screenX, wallColour);
            }
        }

        /// <inheritdoc/>
        public void RenderCeilingColumn(IMazePainterPov painter, int screenX)
        {
            for (var screenY = 0; screenY < (this.screenHeight / 2) - 1; screenY++)
            {
                painter.WriteToScreenBuffer(screenX, screenY, this.ceilingChar, ConsoleOutputType.Default);
            }
        }

        /// <inheritdoc/>
        public void RenderFloorColumn(IMazePainterPov painter, int screenX)
        {
            for (var screenY = this.screenHeight - 1; screenY > this.screenHeight / 2; screenY--)
            {
                painter.WriteToScreenBuffer(screenX, screenY, this.floorChar, ConsoleOutputType.Default);
            }
        }

        /// <inheritdoc/>
        public void RenderPerpendicularWallColumn(
            IMazePainterPov painter,
            int screenX,
            int sectionIndent,
            char wallChar,
            ConsoleOutputType wallColour)
        {
            var topY = sectionIndent;
            var bottomY = this.screenHeight - topY;
            for (var screenY = topY; screenY < bottomY; screenY++)
            {
                //painter.WriteToScreenBuffer(screenX, screenY, this.perpendicularWallChar, wallColour);
                painter.WriteToScreenBuffer(screenX, screenY, wallChar, wallColour);
            }
        }

        /// <summary>
        /// Writes a single column of <see cref="ConsolePixel"/>s to the screen buffer, representing a wall
        /// which runs perpendicular to the direction that the player is facing.
        /// </summary>
        /// <param name="painter">
        /// The painter which will write the column to the screen buffer.
        /// </param>
        /// <param name="screenX">Horizontal co-ordinate of the column.</param>
        /// <param name="wallColour">
        /// The colour of the wall, represented as a <see cref="ConsoleOutputType"/>.
        /// </param>
        private void RenderPerpendicularWallColumn(IMazePainterPov painter, int screenX, ConsoleOutputType wallColour)
        {
            var nextSectionIndent = screenX switch
            {
                0 => 1,
                1 => 5,
                2 => 5,
                3 => 5,
                4 => 5,
                5 => 8,
                6 => 8,
                7 => 8,
                8 => 10,
                9 => 10,
                10 => 11,
                11 => 12,
                12 => 12,
                13 => 11,
                14 => 10,
                15 => 10,
                16 => 8,
                17 => 8,
                18 => 8,
                19 => 5,
                20 => 5,
                21 => 5,
                22 => 5,
                23 => 1,
                _ => 0, // TODO: make this relationship an int[] private field?
            };
            for (var screenY = nextSectionIndent; screenY < this.screenHeight - nextSectionIndent; screenY++)
            {
                painter.WriteToScreenBuffer(screenX, screenY, this.perpendicularWallChar, wallColour);
            }
        }

        /// <summary>
        /// Writes a single column of <see cref="ConsolePixel"/>s to the screen buffer, representing a wall
        /// which runs parallel to the direction that the player is facing.
        /// </summary>
        /// <param name="painter">
        /// The painter which will write the column to the screen buffer.
        /// </param>
        /// <param name="screenX">Horizontal co-ordinate of the column.</param>
        /// <param name="wallColour">
        /// The colour of the wall, represented as a <see cref="ConsoleOutputType"/>.
        /// </param>
        private void RenderParallelWallColumn(IMazePainterPov painter, int screenX, ConsoleOutputType wallColour)
        {
            var columnIndent = screenX < this.screenWidth / 2 ? screenX : this.screenWidth - screenX - 1;
            for (var screenY = columnIndent; screenY < this.screenHeight - columnIndent; screenY++)
            {
                painter.WriteToScreenBuffer(screenX, screenY, this.parallelWallChar, wallColour);
            }
        }
    }
}
