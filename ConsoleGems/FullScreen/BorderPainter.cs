// <copyright file="BorderPainter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.FullScreen
{
    /// <summary>
    /// Manages the painting of borders around an IPainter.
    /// </summary>
    public class BorderPainter(IConsole console)
        : IBorderPainter
    {
        private bool borderHasBeenPainted;

        /// <inheritdoc/>
        public IPainter? Painter { get; set; }

        /// <inheritdoc/>
        public void PaintTopBorderIfRequired()
        {
            if (this.borderHasBeenPainted || this.Painter is null)
            {
                return;
            }

            if (this.Painter.HasBorder)
            {
                console.Write("╭", ConsoleOutputType.MenuBody);
                console.Write(new string('─', this.Painter.InnerSize.Width), ConsoleOutputType.MenuBody);
                console.Write("╮", ConsoleOutputType.MenuBody);
                console.CursorTop++;
            }
        }

        /// <inheritdoc/>
        public void PaintSideBorderIfRequired()
        {
            if (this.borderHasBeenPainted || this.Painter is null)
            {
                return;
            }

            if (this.Painter.HasBorder)
            {
                console.Write("│", ConsoleOutputType.MenuBody);
            }
        }

        /// <inheritdoc/>
        public void PaintBottomBorderIfRequired()
        {
            if (this.borderHasBeenPainted || this.Painter is null)
            {
                return;
            }

            if (this.Painter.HasBorder)
            {
                console.CursorLeft = this.Painter.Origin.X;
                console.Write("╰", ConsoleOutputType.MenuBody);
                console.Write(new string('─', this.Painter.InnerSize.Width), ConsoleOutputType.MenuBody);
                console.Write("╯", ConsoleOutputType.MenuBody);
            }

            this.borderHasBeenPainted = true;
        }
    }
}
