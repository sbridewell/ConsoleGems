// <copyright file="MazePainterPov.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.Pov
{
    using System.Diagnostics.CodeAnalysis;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.FullScreen;
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Provides a flattened perspective player's point of view of a maze.
    /// </summary>
    public class MazePainterPov : Painter, IMazePainterPov
    {
        private readonly int[] sectionWidths = MazePainterPovConstants.SectionWidths;
        private readonly ISectionRenderer sectionRenderer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazePainterPov"/> class.
        /// </summary>
        /// <param name="console">IConsole implementation.</param>
        /// <param name="borderPainter">Paints a border around the painter.</param>
        /// <param name="sectionRenderer">Renders sections of the view.</param>
        public MazePainterPov(
            IConsole console,
            IBorderPainter borderPainter,
            ISectionRenderer sectionRenderer)
            : base(console, borderPainter)
        {
            this.sectionRenderer = sectionRenderer;
            this.InnerSize = new ConsoleSize(
                MazePainterPovConstants.PainterInnerWidth,
                MazePainterPovConstants.PainterInnerHeight);
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public void Render(Maze maze, Player player)
        {
            // TODO: can we refactor to remove this method?
            var forwardView = new ForwardView(maze, player, this.sectionWidths.Length - 2); // TODO: why -2?
            this.RenderForwardView(forwardView, player.FacingDirection);
            this.Paint();
        }

        /// <inheritdoc/>
        public void RenderForwardView(ForwardView forwardView, Direction playerFacingDirection)
        {
            this.ClearScreenBuffer();
            var i = 0;
            for (i = 0; i < forwardView.VisibleDistance; i++)
            {
                this.sectionRenderer.RenderSection(this, i, forwardView, playerFacingDirection);
            }

            for (var section = i - 1; section < this.sectionWidths.Length - 1; section++)
            {
                switch (forwardView.MiddleRow[i - 1])
                {
                    case MazePointType.Wall:
                        this.sectionRenderer.RenderSectionAllWall(
                            this,
                            section,
                            LeftOrRight.Left,
                            forwardView.VisibleDistance,
                            '▓',
                            playerFacingDirection);
                        this.sectionRenderer.RenderSectionAllWall(
                            this,
                            section,
                            LeftOrRight.Right,
                            forwardView.VisibleDistance,
                            '▓',
                            playerFacingDirection);
                        break;
                    case MazePointType.Exit:
                        this.sectionRenderer.RenderSectionAllWall(
                            this,
                            section,
                            LeftOrRight.Left,
                            forwardView.VisibleDistance,
                            '♥',
                            playerFacingDirection);
                        this.sectionRenderer.RenderSectionAllWall(
                            this,
                            section,
                            LeftOrRight.Right,
                            forwardView.VisibleDistance,
                            '♥',
                            playerFacingDirection);
                        break;
                    default:
                        this.sectionRenderer.RenderSectionTooFar(this, LeftOrRight.Left);
                        this.sectionRenderer.RenderSectionTooFar(this, LeftOrRight.Right);
                        break;
                }
            }
        }
    }
}
