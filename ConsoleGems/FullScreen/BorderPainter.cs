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
        public void PaintBorderIfRequired()
        {
            if (this.borderHasBeenPainted || this.Painter is null)
            {
                return;
            }

            if (this.Painter.HasBorder)
            {
                // Top border
                console.CursorLeft = this.Painter.Origin.X;
                console.CursorTop = this.Painter.Origin.Y;
                console.Write("╭", ConsoleOutputType.MenuBody);
                console.Write(new string('─', this.Painter.InnerSize.Width), ConsoleOutputType.MenuBody);
                console.WriteLine("╮", ConsoleOutputType.MenuBody);

                // Left and right borders
                for (var y = this.Painter.Origin.Y + 1; y <= this.Painter.Origin.Y + this.Painter.InnerSize.Height; y++)
                {
                    console.CursorTop = y;
                    console.CursorLeft = this.Painter.Origin.X;
                    console.Write("│", ConsoleOutputType.MenuBody);
                    console.CursorLeft = this.Painter.Origin.X + this.Painter.InnerSize.Width + 1;
                    console.WriteLine("│", ConsoleOutputType.MenuBody);
                }

                // Bottom border
                console.CursorLeft = this.Painter.Origin.X;
                console.CursorTop = this.Painter.Origin.Y + this.Painter.InnerSize.Height + 1;
                console.Write("╰", ConsoleOutputType.MenuBody);
                console.Write(new string('─', this.Painter.InnerSize.Width), ConsoleOutputType.MenuBody);
                console.WriteLine("╯", ConsoleOutputType.MenuBody);

                this.borderHasBeenPainted = true;
            }
        }

        /// <inheritdoc/>
        public void Reset()
        {
            this.borderHasBeenPainted = false;
        }
    }
}
