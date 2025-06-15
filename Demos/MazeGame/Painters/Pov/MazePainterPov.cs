// <copyright file="MazePainterPov.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.Pov
{
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.FullScreen;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Provides a flattened perspective player's point of view of a maze.
    /// </summary>
    public class MazePainterPov(
        IConsole console,
        IBorderPainter borderPainter,
        ISectionRenderer sectionRenderer)
        : Painter(console, borderPainter), IMazePainterPov
    {
        private readonly int[] sectionWidths = MazePainterPovConstants.SectionWidths;

        /// <inheritdoc/>
        public void Render(Maze maze, Player player)
        {
            var forwardView = new ForwardView(maze, player, this.sectionWidths.Length - 2); // TODO: why -2?
            this.RenderForwardView(forwardView);
            this.Paint();
        }

        /// <inheritdoc/>
        public void RenderForwardView(ForwardView forwardView)
        {
            this.ClearScreenBuffer();
            var i = 0;
            for (i = 0; i < forwardView.VisibleDistance; i++)
            {
                sectionRenderer.RenderSection(this, i, forwardView);
            }

            for (var section = i - 1; section < this.sectionWidths.Length - 1; section++)
            {
                if (forwardView.MiddleRow[i - 1] == MazePointType.Wall)
                {
                    sectionRenderer.RenderSectionAllWall(this, section, LeftOrRight.Left, forwardView.VisibleDistance);
                    sectionRenderer.RenderSectionAllWall(this, section, LeftOrRight.Right, forwardView.VisibleDistance);
                }
                else
                {
                    sectionRenderer.RenderSectionTooFar(this, LeftOrRight.Left);
                    sectionRenderer.RenderSectionTooFar(this, LeftOrRight.Right);
                }
            }
        }
    }
}
