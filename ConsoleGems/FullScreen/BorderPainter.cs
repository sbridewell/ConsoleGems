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
                console.CursorLeft = this.Painter.Origin.X;
                console.CursorTop = this.Painter.Origin.Y;
                console.Write("╭", ConsoleOutputType.MenuBody);
                console.Write(new string('─', this.Painter.InnerSize.Width), ConsoleOutputType.MenuBody);
                console.Write("╮", ConsoleOutputType.MenuBody);
                console.CursorTop++;
            }
        }

        /// <inheritdoc/>
        public void PaintSideBorderIfRequired(bool isLeftBorder)
        {
            if (this.borderHasBeenPainted || this.Painter is null)
            {
                return;
            }

            if (this.Painter.HasBorder)
            {
                if (isLeftBorder)
                {
                    console.CursorLeft = this.Painter.Origin.X;
                }
                else
                {
                    console.CursorLeft = this.Painter.Origin.X + this.Painter.InnerSize.Width + 1;
                }

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
                console.CursorTop = this.Painter.Origin.Y + this.Painter.InnerSize.Height + 1;
                console.Write("╰", ConsoleOutputType.MenuBody);
                console.Write(new string('─', this.Painter.InnerSize.Width), ConsoleOutputType.MenuBody);
                console.Write("╯", ConsoleOutputType.MenuBody);
            }

            this.borderHasBeenPainted = true;
        }
    }
}
