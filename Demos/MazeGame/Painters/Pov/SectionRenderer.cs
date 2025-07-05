// <copyright file="SectionRenderer.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.Pov
{
    using Sde.ConsoleGems.Consoles;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Renders a section of the maze from the player's point of view.
    /// </summary>
    public class SectionRenderer(IColumnRenderer columnRenderer)
        : ISectionRenderer
    {
        private readonly int screenWidth = MazePainterPovConstants.PainterInnerWidth;
        private readonly int[] sectionWidths = MazePainterPovConstants.SectionWidths;
        private readonly int[] sectionIndents = MazePainterPovConstants.SectionIndents;

        /// <inheritdoc/>
        public void RenderSectionAllWall(
            IMazePainterPov painter,
            int sectionNumber,
            LeftOrRight leftOrRight,
            int forwardDistance,
            char wallChar,
            Direction playerFacingDirection)
        {
            var sectionIndent = this.sectionIndents[sectionNumber];
            var floorCeilingIndent = this.sectionIndents[forwardDistance - 1];
            var wallColour = playerFacingDirection switch
            {
                Direction.North => MazePainterPovConstants.NorthColour,
                Direction.South => MazePainterPovConstants.SouthColour,
                Direction.East => MazePainterPovConstants.EastColour,
                Direction.West => MazePainterPovConstants.WestColour,
                _ => ConsoleOutputType.Default,
            };
            for (var columnIndent = sectionIndent; columnIndent < this.sectionWidths[sectionNumber] + sectionIndent; columnIndent++)
            {
                var screenX = leftOrRight == LeftOrRight.Left ? columnIndent : this.screenWidth - columnIndent - 1;
                columnRenderer.RenderCeilingColumn(painter, screenX);
                columnRenderer.RenderFloorColumn(painter, screenX);
                columnRenderer.RenderPerpendicularWallColumn(painter, screenX, floorCeilingIndent, wallChar, wallColour);
            }
        }

        /// <inheritdoc/>
        public void RenderSectionTooFar(IMazePainterPov painter, LeftOrRight leftOrRight)
        {
            var screenX = leftOrRight == LeftOrRight.Left ? 11 : 12;
            columnRenderer.RenderCeilingColumn(painter, screenX);
            columnRenderer.RenderFloorColumn(painter, screenX);
            painter.WriteToScreenBuffer(screenX, 11, ' ', ConsoleOutputType.Default);
            painter.WriteToScreenBuffer(screenX, 12, ' ', ConsoleOutputType.Default);
        }

        /// <inheritdoc/>
        public void RenderSection(IMazePainterPov painter, int sectionNumber, ForwardView forwardView, Direction playerFacingDirection)
        {
            if (sectionNumber < forwardView.LeftRow.Count)
            {
                var wallToTheLeft = forwardView.LeftRow[sectionNumber] == MazePointType.Wall;
                this.RenderSection(painter, sectionNumber, LeftOrRight.Left, wallToTheLeft, playerFacingDirection);
            }

            if (sectionNumber < forwardView.RightRow.Count)
            {
                var wallToTheRight = forwardView.RightRow[sectionNumber] == MazePointType.Wall;
                this.RenderSection(painter, sectionNumber, LeftOrRight.Right, wallToTheRight, playerFacingDirection);
            }
        }

        private void RenderSection(
            IMazePainterPov painter,
            int sectionNumber,
            LeftOrRight leftOrRight,
            bool wallToTheSide,
            Direction playerFacingDirection)
        {
            var sectionIndent = this.sectionIndents[sectionNumber];
            for (var columnIndent = sectionIndent; columnIndent < this.sectionWidths[sectionNumber] + sectionIndent; columnIndent++)
            {
                var screenX = leftOrRight == LeftOrRight.Left ? columnIndent : this.screenWidth - columnIndent - 1;
                if (wallToTheSide)
                {
                    columnRenderer.RenderColumn(painter, screenX, wallIsPerpendicular: false, playerFacingDirection);
                }
                else
                {
                    columnRenderer.RenderColumn(painter, screenX, wallIsPerpendicular: true, playerFacingDirection);
                }
            }
        }
    }
}
